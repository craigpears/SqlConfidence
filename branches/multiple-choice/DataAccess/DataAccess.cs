using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Common;
using Common.Enums;

namespace DA
{
    public class DataAccess
    {
        protected SqlConnection _conn;

        public DataAccess()
        {
            _conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString);
        }

        public List<ExerciseModel> ListExercises(int UserId)
        {
            List<ExerciseModel> exercises = new List<ExerciseModel>();
            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand command = new SqlCommand("select * from exercises order by ex_name asc", _conn);
            SqlDataReader reader = command.ExecuteReader();
            List<int> exerciseIds = new List<int>();
            while (reader.Read())
            {
                exerciseIds.Add((int)reader["EX_ID"]);
            }
            reader.Close();
            foreach (int id in exerciseIds)
            {
                ExerciseModel exercise = GetExercise(UserId, id);
                exercises.Add(exercise);
            }
            exercises = exercises.OrderBy(x => x.Name).ToList();
            _conn.Close();
            return exercises;
        }

        public ExerciseModel GetExercise(int UserId, int ExerciseId)
        {
            SqlCommand command;
            SqlDataReader reader;

            if (_conn.State != ConnectionState.Open) _conn.Open();
            command = new SqlCommand("select * from exercises where ex_id = @ExerciseId", _conn);
            command.Parameters.AddWithValue("ExerciseId", ExerciseId);
            reader = command.ExecuteReader();
            reader.Read();
            ExerciseModel exercise = ParseExerciseFromReader(reader);
            reader.Close();
            exercise.DataSource = GetDataSource(exercise.DataSourceId);
            exercise.Questions = GetQuestions(UserId, ExerciseId);

            _conn.Close();
            return exercise;
        }

        private ExerciseModel ParseExerciseFromReader(SqlDataReader reader)
        {
            ExerciseModel exercise = new ExerciseModel();
            exercise.Summary = (String)reader["EX_SUMMARY"];
            exercise.Name = (String)reader["EX_NAME"];
            exercise.Description = (String)reader["EX_DESCRIPTION"];
            exercise.ExerciseId = (int)reader["EX_ID"];
            exercise.DataSourceId = (int)reader["DS_ID"];
            
            return exercise;
        }

        public DataSourceModel GetDataSource(int DataSourceId)
        {
            DataSourceModel dataSource = new DataSourceModel();
            SqlCommand command;
            SqlDataReader reader;

            if (_conn.State != ConnectionState.Open) _conn.Open();
            command = new SqlCommand("select * from data_sources_tables where ds_id = @DataSourceId", _conn);
            command.Parameters.AddWithValue("DataSourceId", DataSourceId);
            reader = command.ExecuteReader();

            dataSource.Tables = new List<TableModel>();
            while (reader.Read())
            {
                TableModel table = ParseTableFromReader(reader);
                dataSource.Tables.Add(table);
            }

            reader.Close();

            command = new SqlCommand("select * from data_sources where ds_id = @DataSourceId", _conn);
            command.Parameters.AddWithValue("DataSourceId", DataSourceId);
            reader = command.ExecuteReader();
            reader.Read();
            dataSource.Name = (string)reader["DS_NAME"];
            reader.Close();
            _conn.Close();

            return dataSource;
        }

        private TableModel ParseTableFromReader(SqlDataReader reader)
        {
            TableModel table = new TableModel();
            table.TableId = (int)reader["DST_ID"];
            table.DataSourceId = (int)reader["DS_ID"];
            table.TableName = (string)reader["DST_TABLE_NAME"];
            table.Columns = new JavaScriptSerializer().Deserialize<List<ColumnModel>>((string)reader["DST_COLUMNS"]);

            return table;
        }

        public void CreateUser(UserModel user)
        {
            // Validate that the user model has been populated
            if (String.IsNullOrEmpty(user.Email) ||
                String.IsNullOrEmpty(user.FirstName) ||
                String.IsNullOrEmpty(user.LastName) ||
                String.IsNullOrEmpty(user.HashedPassword))
            {
                throw new Exception("Create user - user model not properly populated");
            }

            String query = "insert into users (US_CREATED_BY, US_CREATED_DATE, US_EMAIL_ADDRESS, US_PASSWORD, US_FIRST_NAME, US_LAST_NAME) " +
                "VALUES (@CreatedBy, @CreatedDate, @EmailAddress, @Password, @FirstName, @LastName)";

            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand command = new SqlCommand(query, _conn);
            command.Parameters.AddWithValue("CreatedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            command.Parameters.AddWithValue("CreatedDate", DateTime.Now);
            command.Parameters.AddWithValue("EmailAddress", user.Email);
            command.Parameters.AddWithValue("Password", user.HashedPassword);
            command.Parameters.AddWithValue("FirstName", user.FirstName);
            command.Parameters.AddWithValue("LastName", user.LastName);
            command.ExecuteNonQuery();
            _conn.Close();
        }

        public UserModel GetUser(UserModel user, Boolean checkPassword = true)
        {
            // Check that the user model has been populated
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.HashedPassword))
            {
                throw new Exception("Email or Hashed Password not provided to DA.GetUser");
            }

            String query = "select * from users where us_email_address = @EmailAddress";

            if (checkPassword) query += " and us_password = @Password";

            try
            {
                if (_conn.State != ConnectionState.Open) _conn.Open();
                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.AddWithValue("EmailAddress", user.Email);
                command.Parameters.AddWithValue("Password", user.HashedPassword);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                user.UserId = (int)reader["US_ID"];
                user.FirstName = (String)reader["US_FIRST_NAME"];
                user.LastName = (String)reader["US_LAST_NAME"];
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error getting user {0} - {1}", user.Email, ex.Message));
            }
            return user;
        }

        public List<QuestionModel> GetQuestions(int UserId, int ExerciseId)
        {
            List<QuestionModel> questions = new List<QuestionModel>();

            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand command = new SqlCommand("select * from vwExercisesQuestionsUsers where ex_id = @EX_ID and US_ID = @US_ID order by exq_order asc", _conn);
            command.Parameters.AddWithValue("EX_ID", ExerciseId);
            command.Parameters.AddWithValue("US_ID", UserId);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                QuestionModel question = ParseQuestionFromReader(reader);
                questions.Add(question);
            }
            reader.Close();
            _conn.Close();

            return questions;
        }

        public QuestionModel GetQuestion(int UserId, int QuestionId)
        {
            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand command = new SqlCommand("select * from vwExercisesQuestionsUsers where exq_id = @EXQ_ID and US_ID = @US_ID", _conn);
            command.Parameters.AddWithValue("EXQ_ID", QuestionId);
            command.Parameters.AddWithValue("US_ID", UserId);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            QuestionModel question = ParseQuestionFromReader(reader);
            reader.Close();
            _conn.Close();

            Object test = new object();

            return question;
        }

        private QuestionModel ParseQuestionFromReader(SqlDataReader reader)
        {
            QuestionModel question = new QuestionModel();
            question.ExerciseId = (int)reader["EX_ID"];
            question.QuestionId = (int)reader["EXQ_ID"];
            question.DataSourceId = (int)reader["DS_ID"];
            question.Instructions = (string)reader["EXQ_INSTRUCTIONS_TEMPLATE"];
            question.AnswerTemplate = (string)reader["EXQ_ANSWERS_TEMPLATE"];
            question.Order = (int)reader["EXQ_ORDER"];
            question.Description = (string)reader["EXQ_DESCRIPTION"];
            question.Hint = reader["EXQ_HINT"] == DBNull.Value ? "No hint available" : (string)reader["EXQ_HINT"];
            question.Answered = (Boolean)reader["Answered"];
            object sqlDateTime = reader["AnsweredDate"];
            question.AnsweredDate = (sqlDateTime == System.DBNull.Value)
                                    ? (DateTime?)null
                                    : Convert.ToDateTime(sqlDateTime);
            return question;
        }

        public DataTable ExecuteUserQuery(int QuestionId, string Query, int UserId)
        {
            SqlConnection dataSourcesConnection = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString);
            try
            {
                dataSourcesConnection.Open();
                QuestionModel question = GetQuestion(UserId, QuestionId);
                ExerciseModel exercise = GetExercise(UserId, question.ExerciseId);
                             
                // Start a transaction first
                SqlCommand command = new SqlCommand("BEGIN TRANSACTION", dataSourcesConnection);
                command.ExecuteNonQuery();

                // Do any commands that the user has already ran on this data source (Execute Query type only, not submitted answers)
                command = new SqlCommand("SELECT usa_description FROM USERS_ACTIONS WHERE EXQ_ID = @QuestionId AND USAT_ID = 0 AND ISNULL(USA_RESET_DATE, GETDATE()) >= GETDATE() ", _conn);
                command.Parameters.AddWithValue("QuestionId", question.QuestionId);
                _conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                // Execute each command in the history
                while (reader.Read())
                {
                    String query = (String)reader["usa_description"];
                    command = QueryBuilder.BuildCommand(query, exercise, question, dataSourcesConnection);
                    command.ExecuteNonQuery();
                }

                // Do the commands
                command = QueryBuilder.BuildCommand(Query, exercise, question, dataSourcesConnection);

                reader = command.ExecuteReader();
                DataTable results = new DataTable();
                results.Load(reader);


                return results;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // Roll back the transaction
                SqlCommand command = new SqlCommand("ROLLBACK TRANSACTION", dataSourcesConnection);
                command.ExecuteNonQuery();
                dataSourcesConnection.Close();
                _conn.Close();
            }
        }

        public List<String> ListExerciseCategories()
        {
            List<String> exerciseCategories = new List<String>();

            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand command = new SqlCommand("select ec_name from exercise_categories", _conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                exerciseCategories.Add((String)reader["ec_name"]);
            }
            _conn.Close();

            return exerciseCategories;
        }

        public Boolean CheckAnswer(int QuestionId, string Query, int UserId, out List<AnswerDifferenceModel> Differences)
        {
            SqlConnection dataSourcesConnection = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString);
            dataSourcesConnection.Open();            
            QuestionModel question = GetQuestion(UserId, QuestionId);
            ExerciseModel exercise = GetExercise(UserId, question.ExerciseId);

            SqlCommand command = QueryBuilder.BuildDifferenceQuery(
                QueryBuilder.BuildCommand(Query, exercise, question, dataSourcesConnection),
                QueryBuilder.BuildCommand(question.AnswerTemplate, exercise, question, dataSourcesConnection),
                dataSourcesConnection
            );

            DataTable diffDataTable = new DataTable();
            SqlDataReader reader = command.ExecuteReader();
            diffDataTable.Load(reader);
            Differences = Converters.DataTableToAnswerDifferences(diffDataTable);

            dataSourcesConnection.Close();

            // The answer is correct if there were no differences
            return diffDataTable.Rows.Count == 0;
        }

        public void LogUserQuery(int UserId, string Query, int QuestionId, UsersActionsTypes QueryType)
        {
            _conn.Open();
            SqlCommand command = new SqlCommand("insert into USERS_ACTIONS (US_ID, USA_DESCRIPTION, EXQ_ID, USAT_ID, USA_CREATED_DATE) VALUES (@UserId, @Query, @QuestionId, @ActionTypeId, GETDATE())", _conn);
            command.Parameters.AddWithValue("UserId", UserId);
            command.Parameters.AddWithValue("Query", Query);
            command.Parameters.AddWithValue("QuestionId", QuestionId);
            command.Parameters.AddWithValue("ActionTypeId", (int)QueryType);
            command.ExecuteNonQuery();
            _conn.Close();
        }

        public void LogQuestionAnswered(int UserId, int QuestionId)
        {           
            try
            {
                _conn.Open();
                // Check if this question has already been answered
                SqlCommand command = new SqlCommand("select cast(case when exists(select * from exercises_questions_answered where us_id = @US_ID and exq_id = @EXQ_ID) then 1 else 0 end as bit) 'exists'", _conn);
                command.Parameters.AddWithValue("US_ID", UserId);
                command.Parameters.AddWithValue("EXQ_ID", QuestionId);

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                Boolean exists = (Boolean)reader["exists"];
                reader.Close();

                if (!exists)
                {
                    command = new SqlCommand("insert into EXERCISES_QUESTIONS_ANSWERED (US_ID, EXQA_CREATED_DATE, EXQ_ID) VALUES (@UserId, GETDATE(), @QuestionId)", _conn);
                    command.Parameters.AddWithValue("UserId", UserId);
                    command.Parameters.AddWithValue("QuestionId", QuestionId);
                    command.ExecuteNonQuery();
                }
                
            }
            catch (Exception)
            {                
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void CreatePasswordResetToken(UserModel user, Guid token)
        {
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand("insert into USERS_RESET_TOKENS (UST_TOKEN, UST_CREATED_DATE, UST_EXPIRY_DATE, UST_STATUS_ID) values (@Token, getdate(), @ExpiryDate, 0)", _conn);
                command.Parameters.AddWithValue("Token", token);
                command.Parameters.AddWithValue("ExpiryDate", DateTime.Now.AddHours(Properties.Settings.Default.ResetTokenLifetimeHours));
                command.ExecuteNonQuery();        
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
            

        }
    }
}
