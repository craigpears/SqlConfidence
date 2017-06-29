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
using System.Diagnostics;
using Common.Exceptions;
using DataAccess.Models;

namespace Common
{
    public class DataAccess
    {
        public DataAccess()
        {
        }

        #region ListMethods
            public List<ExerciseModel> ListExercises()
            {
                List<ExerciseModel> exercises = new List<ExerciseModel>();
                List<int> exerciseIds = new List<int>();
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select ex_id from exercises where ex_deleted = 0 order by ex_order asc", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exerciseIds.Add((int)reader["EX_ID"]);
                        }
                        reader.Close();
                    }
                }
                foreach (int id in exerciseIds)
                {
                    ExerciseModel exercise = GetExercise(id);
                    exercises.Add(exercise);
                }
                return exercises;
            }

            public List<ExerciseModel> ListExercises(int UserId)
            {
                List<ExerciseModel> exercises = new List<ExerciseModel>();
                List<int> exerciseIds = new List<int>();
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select ex_id from exercises where ex_deleted = 0 and ex_published = 1", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            exerciseIds.Add((int)reader["EX_ID"]);
                        }
                        reader.Close();
                    }
                }
                foreach (int id in exerciseIds)
                {
                    ExerciseModel exercise = GetExercise(UserId, id);
                    exercises.Add(exercise);
                }

                return exercises;
            }

            public List<DataSourceModel> ListDataSources()
            {
                List<DataSourceModel> dataSources = new List<DataSourceModel>();
                List<int> dataSourceIds = new List<int>();

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT DS_ID FROM DATA_SOURCES", conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dataSourceIds.Add((int)reader["DS_ID"]);
                            }
                        }
                    }
                }
                foreach (int ds_id in dataSourceIds)
                {
                    try
                    {
                        DataSourceModel dataSource = GetDataSource(ds_id);
                        dataSources.Add(dataSource);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(String.Format("Error loading data source {0} - {1}", ds_id, ex.Message));
                    }
                }

                return dataSources;
            }



            public List<QuestionModel> ListQuestions(int UserId, int ExerciseId)
            {
                List<QuestionModel> questions = new List<QuestionModel>();

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from vwExercisesQuestionsUsers where ex_id = @EX_ID and US_ID = @US_ID order by exq_order asc", conn))
                    {
                        command.Parameters.AddWithValue("EX_ID", ExerciseId);
                        command.Parameters.AddWithValue("US_ID", UserId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuestionModel question = ParseQuestionFromReader(reader);
                                questions.Add(question);
                            }
                            reader.Close();
                        }
                    }
                }

                return questions;
            }

            public List<QuestionModel> ListQuestions(int ExerciseId)
            {
                List<QuestionModel> questions = new List<QuestionModel>();
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from EXERCISES_QUESTIONS where exq_deleted = 0 and ex_id = @EX_ID order by exq_order asc", conn))
                    {
                        command.Parameters.AddWithValue("EX_ID", ExerciseId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuestionModel question = ParseQuestionFromReader(reader);
                                questions.Add(question);
                            }
                        }
                    }
                }
                return questions;
            }

            public List<UserActionModel> ListQuestionQueries(int QuestionId, int UserId)
            {
                try
                {
                    List<UserActionModel> queries = new List<UserActionModel>();
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("SELECT usa_description FROM UserActions WHERE EXQ_ID = @QuestionId AND USAT_ID = 0 AND ISNULL(USA_RESET_DATE, GETDATE()) >= GETDATE() AND US_ID = @UserId AND usa_description LIKE '%update%'", conn))
                        {
                            command.Parameters.AddWithValue("QuestionId", QuestionId);
                            command.Parameters.AddWithValue("UserId", UserId);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Execute each command in the history
                                while (reader.Read())
                                {
                                    UserActionModel query = new UserActionModel();
                                    query.Description = (String)reader["usa_description"];
                                    queries.Add(query);
                                }
                            }
                        }
                    }

                    return queries;
                }
                catch (Exception)
                {
                    throw;
                }

            }
        #endregion
        #region GetMethods
            public ExerciseModel GetExercise(int UserId, int ExerciseId)
            {
                ExerciseModel exercise;
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from exercises where ex_id = @ExerciseId", conn))
                    {
                        command.Parameters.AddWithValue("ExerciseId", ExerciseId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            exercise = ParseExerciseFromReader(reader);
                        }
                    }
                }
                exercise.DataSource = GetDataSource(exercise.DataSourceId);
                exercise.Questions = ListQuestions(UserId, ExerciseId);

                return exercise;
            }

            public ExerciseModel GetExercise(int ExerciseId)
            {
                ExerciseModel exercise;
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from exercises where ex_id = @ExerciseId", conn))
                    {
                        command.Parameters.AddWithValue("ExerciseId", ExerciseId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            exercise = ParseExerciseFromReader(reader);
                        }
                    }
                }
                exercise.DataSource = GetDataSource(exercise.DataSourceId);
                exercise.Questions = ListQuestions(ExerciseId);

                return exercise;
            }

            public DataSourceModel GetDataSource(int DataSourceId)
            {
                DataSourceModel dataSource = new DataSourceModel();

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from data_sources_tables where ds_id = @DataSourceId", conn))
                    {
                        command.Parameters.AddWithValue("DataSourceId", DataSourceId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataSource.Tables = new List<TableModel>();
                            while (reader.Read())
                            {
                                TableModel table = ParseTableFromReader(reader);
                                dataSource.Tables.Add(table);
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand("select * from data_sources where ds_id = @DataSourceId", conn))
                    {
                        command.Parameters.AddWithValue("DataSourceId", DataSourceId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            dataSource.Name = (string)reader["DS_NAME"];
                            dataSource.DataSourceId = (int)reader["DS_ID"];
                            dataSource.DataSourceIdGuid = (Guid)reader["DS_ID_GUID"];
                        }
                    }
                }

                return dataSource;
            }

            public int GetHighestUserId()
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT MAX (US_ID) as 'HighestId' FROM USERS", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        int highestId = (int)reader["HighestId"];
                        return highestId;
                    }
                }
            }

            public UserModel GetUser(UserModel user, Boolean checkPassword = true)
            {
                // Check that the user model has been populated
                if (String.IsNullOrEmpty(user.Email) || (String.IsNullOrEmpty(user.HashedPassword) && checkPassword))
                {
                    throw new Exception("Email or Hashed Password not provided to DA.GetUser");
                }

                String query = "select * from users where Email = @EmailAddress";

                if (checkPassword) query += " and Password = @Password";

                try
                {
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("EmailAddress", user.Email);
                            command.Parameters.AddWithValue("Password", user.HashedPassword);
                            SqlDataReader reader = command.ExecuteReader();
                            reader.Read();
                            user.UserId = (int)reader["UserId"];
                            user.FirstName = (String)reader["FirstName"];
                            user.LastName = (String)reader["LastName"];
                            user.IsAdmin = DAHelpers.BooleanFromReader(reader, "IsAdmin");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Error getting user {0} - {1}", user.Email, ex.Message));
                }
                return user;
            }

            public void ResetQuestion(int UserId, int QuestionId)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand("DELETE FROM UserActions WHERE US_ID = @UserId AND EXQ_ID = @QuestionId", conn))
                        {
                            command.Parameters.AddWithValue("UserId", UserId);
                            command.Parameters.AddWithValue("QuestionId", QuestionId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Error resetting data - {0}", ex.Message));
                }
            }

            public QuestionModel GetQuestion(int UserId, int QuestionId)
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from vwExercisesQuestionsUsers where exq_id = @EXQ_ID and US_ID = @US_ID", conn))
                    {
                        command.Parameters.AddWithValue("EXQ_ID", QuestionId);
                        command.Parameters.AddWithValue("US_ID", UserId);
                        QuestionModel question;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            question = ParseQuestionFromReader(reader);
                        }
                        
                        return question;
                    }
                }
            }

            public QuestionModel GetQuestion(int QuestionId)
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select * from exercises_questions where exq_id = @EXQ_ID", conn))
                    {
                        command.Parameters.AddWithValue("EXQ_ID", QuestionId);
                        QuestionModel question;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            question = ParseQuestionFromReader(reader);
                        }

                        return question;
                    }
                }
            }
        #endregion
        #region ParseMethods

        private TableModel ParseTableFromReader(SqlDataReader reader)
        {
            TableModel table = new TableModel();
            table.TableId = (int)reader["DST_ID"];
            table.DataSourceId = (int)reader["DS_ID"];
            table.TableName = (string)reader["DST_TABLE_NAME"];
            table.Alias = (string)reader["DST_TABLE_ALIAS"];
            table.DataSourceTableIdGuid = (Guid)reader["DST_ID_GUID"];
            table.Columns = new JavaScriptSerializer().Deserialize<List<ColumnModel>>((string)reader["DST_COLUMNS"]);

            return table;
        }
        
        private ExerciseModel ParseExerciseFromReader(SqlDataReader reader)
        {
            ExerciseModel exercise = new ExerciseModel();
            exercise.Summary = (String)reader["EX_SUMMARY"];
            exercise.Name = (String)reader["EX_NAME"];
            exercise.Description = (String)reader["EX_DESCRIPTION"];
            exercise.ExerciseId = (int)reader["EX_ID"];
            exercise.DataSourceId = (int)reader["DS_ID"];
            exercise.Published = DAHelpers.BooleanFromReader(reader, "EX_PUBLISHED");
            exercise.PublishedDate = DAHelpers.DateTimeFromReader(reader, "EX_PUBLISHED_DATE");
            exercise.SectionName = (String)reader["EX_SECTION_NAME"];
            exercise.Order = (int)reader["EX_ORDER"];
            exercise.ShowQueryBuilder = DAHelpers.BooleanFromReader(reader, "EX_SHOW_QUERY_BUILDER");
            exercise.ExerciseIdGuid = (Guid)reader["EX_ID_GUID"];
            exercise.CreatedDate = DAHelpers.DateTimeFromReader(reader, "EX_CREATED_DATE");
            exercise.CreatedBy = DAHelpers.StringFromReader(reader, "EX_CREATED_BY");
            exercise.UpdatedDate = DAHelpers.NullableDateTimeFromReader(reader, "EX_UPDATED_DATE");
            exercise.UpdatedBy = DAHelpers.NullableStringFromReader(reader, "EX_UPDATED_BY");

            return exercise;
        }

        private QuestionModel ParseQuestionFromReader(SqlDataReader reader)
        {
            QuestionModel question = new QuestionModel();
            question.ExerciseId = (int)reader["EX_ID"];
            question.QuestionId = (int)reader["EXQ_ID"];
            question.Instructions = (string)reader["EXQ_INSTRUCTIONS_TEMPLATE"];
            question.AnswerTemplate = (string)reader["EXQ_ANSWERS_TEMPLATE"];
            question.Order = (int)reader["EXQ_ORDER"];
            question.Description = (string)reader["EXQ_DESCRIPTION"];
            question.Hint = reader["EXQ_HINT"] == DBNull.Value ? "No hint available" : (string)reader["EXQ_HINT"];
            if (DAHelpers.HasColumn(reader, "Answered")) question.Answered = (Boolean)reader["Answered"];
            if (DAHelpers.HasColumn(reader, "AnsweredDate"))
            {
                object sqlDateTime = reader["AnsweredDate"];
                question.AnsweredDate = (sqlDateTime == System.DBNull.Value)
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(sqlDateTime);
            }
            question.StartingSql = DAHelpers.StringFromReader(reader, "EXQ_STARTING_SQL");
            question.ExerciseQuestionIdGuid = (Guid)reader["EXQ_ID_GUID"];
            question.CreatedDate = DAHelpers.DateTimeFromReader(reader, "EXQ_CREATED_DATE");
            question.CreatedBy = DAHelpers.StringFromReader(reader, "EXQ_CREATED_BY");
            question.UpdatedDate = DAHelpers.NullableDateTimeFromReader(reader, "EXQ_UPDATED_DATE");
            question.UpdatedBy = DAHelpers.NullableStringFromReader(reader, "EXQ_UPDATED_BY");
            question.ExerciseQuestionType = (int)reader["EXQ_TYPE"];
            return question;
        }

        #endregion

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

            UserModel existingUser = null;
            try
            {
                existingUser = GetUser(user);
            }
            catch (Exception) { }

            if (existingUser != null && !user.IsGuest) throw new UserExistsException(String.Format("Create user - {0} already exists", user.Email), user.Email);

            String query = "insert into users (US_CREATED_BY, US_CREATED_DATE, US_EMAIL_ADDRESS, US_PASSWORD, US_FIRST_NAME, US_LAST_NAME) " +
                "VALUES (@CreatedBy, @CreatedDate, @EmailAddress, @Password, @FirstName, @LastName)";
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("CreatedBy", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    command.Parameters.AddWithValue("CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("EmailAddress", user.Email);
                    command.Parameters.AddWithValue("Password", user.HashedPassword);
                    command.Parameters.AddWithValue("FirstName", user.FirstName);
                    command.Parameters.AddWithValue("LastName", user.LastName);
                    command.ExecuteNonQuery();
                }
            }
        }

        

        public void LogUserQuery(int UserId, string Query, int QuestionId, UsersActionsTypes QueryType)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("insert into UserActions (US_ID, USA_DESCRIPTION, EXQ_ID, USAT_ID, USA_CREATED_DATE) VALUES (@UserId, @Query, @QuestionId, @ActionTypeId, GETDATE())", conn))
                {
                    command.Parameters.AddWithValue("UserId", UserId);
                    command.Parameters.AddWithValue("Query", Query);
                    command.Parameters.AddWithValue("QuestionId", QuestionId);
                    command.Parameters.AddWithValue("ActionTypeId", (int)QueryType);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreatePasswordResetToken(UserModel user, Guid token)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("insert into USERS_RESET_TOKENS (UST_TOKEN, UST_CREATED_DATE, UST_EXPIRY_DATE, UST_STATUS_ID) values (@Token, getdate(), @ExpiryDate, 0)", conn))
                    {
                        command.Parameters.AddWithValue("Token", token);
                        command.Parameters.AddWithValue("ExpiryDate", DateTime.Now.AddHours(Properties.Settings.Default.ResetTokenLifetimeHours));
                        command.ExecuteNonQuery();
                    }
                }                    
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetDataTable(string Query)
        {
            DataTable dt = new DataTable(); 
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(Query, conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }
            }
            
            return dt;
        }

        public void SaveExercise(ExerciseModel exercise, User user)
        {
            // Is this a new exercise?
            if (exercise.ExerciseId == 0)
            {
                String query = "INSERT INTO EXERCISES";
                query += " (DS_ID, EX_NAME, EX_SUMMARY, EX_DESCRIPTION, EX_CREATED_DATE, EX_CREATED_BY, EX_DELETED, EX_SECTION_NAME, EX_ORDER, EX_SHOW_QUERY_BUILDER)";
                query += " VALUES";
                query += " (@DS_ID, @EX_NAME, @EX_SUMMARY, @EX_DESCRIPTION, GETDATE(), @CREATED_BY, 0, @EX_SECTION_NAME, isnull((SELECT MAX(EX_ORDER) + 1 FROM EXERCISES WHERE EX_SECTION_NAME = @EX_SECTION_NAME), 0), @EX_SHOW_QUERY_BUILDER)";
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("DS_ID", exercise.DataSourceId);
                        command.Parameters.AddWithValue("EX_NAME", exercise.Name);
                        command.Parameters.AddWithValue("EX_SUMMARY", exercise.Summary);
                        command.Parameters.AddWithValue("EX_DESCRIPTION", exercise.Description);
                        command.Parameters.AddWithValue("CREATED_BY", user.Email);
                        command.Parameters.AddWithValue("EX_SECTION_NAME", exercise.SectionName);
                        command.Parameters.AddWithValue("EX_SHOW_QUERY_BUILDER", exercise.ShowQueryBuilder);                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                String query = "UPDATE EXERCISES";
                query += " SET DS_ID = @DS_ID,";
                query += " EX_NAME = @EX_NAME,";
                query += " EX_SUMMARY = @EX_SUMMARY,";
                query += " EX_DESCRIPTION = @EX_DESCRIPTION,";
                query += " EX_UPDATED_DATE = GETDATE(),";
                query += " EX_UPDATED_BY = @UPDATED_BY,";
                query += " EX_SECTION_NAME = @EX_SECTION_NAME,";
                query += " EX_SHOW_QUERY_BUILDER = @EX_SHOW_QUERY_BUILDER,";
                query += " EX_ORDER = @EX_ORDER";
                query += " WHERE EX_ID = @EX_ID";
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("EX_ID", exercise.ExerciseId);
                        command.Parameters.AddWithValue("DS_ID", exercise.DataSourceId);
                        command.Parameters.AddWithValue("EX_NAME", exercise.Name);
                        command.Parameters.AddWithValue("EX_SUMMARY", exercise.Summary);
                        command.Parameters.AddWithValue("EX_DESCRIPTION", exercise.Description ?? "");
                        command.Parameters.AddWithValue("UPDATED_BY", user.Email);
                        command.Parameters.AddWithValue("EX_SECTION_NAME", exercise.SectionName);
                        command.Parameters.AddWithValue("EX_ORDER", exercise.Order);
                        command.Parameters.AddWithValue("EX_SHOW_QUERY_BUILDER", exercise.ShowQueryBuilder);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SaveQuestion(QuestionModel question, User user, Boolean LogUpdatedBy = true)
        {
            // Is this a new exercise?
            if (question.QuestionId == 0)
            {
                String query = "INSERT INTO EXERCISES_QUESTIONS";
                query += " (EX_ID, EXQ_INSTRUCTIONS_TEMPLATE, EXQ_ANSWERS_TEMPLATE, EXQ_DESCRIPTION, EXQ_CREATED_BY, EXQ_CREATED_DATE, EXQ_DELETED, EXQ_HINT, EXQ_ORDER, EXQ_STARTING_SQL)";
                query += " VALUES";
                query += " (@EX_ID, @EXQ_INSTRUCTIONS_TEMPLATE, @EXQ_ANSWERS_TEMPLATE, @EXQ_DESCRIPTION, @EXQ_CREATED_BY, GETDATE(), 0, @EXQ_HINT, isnull((SELECT MAX(EXQ_ORDER) + 1 FROM EXERCISES_QUESTIONS WHERE EX_ID = @EX_ID), 0), @EXQ_STARTING_SQL)";
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("EX_ID", question.ExerciseId);
                        command.Parameters.AddWithValue("EXQ_INSTRUCTIONS_TEMPLATE", question.Instructions);
                        command.Parameters.AddWithValue("EXQ_ANSWERS_TEMPLATE", question.AnswerTemplate);
                        command.Parameters.AddWithValue("EXQ_DESCRIPTION", question.Description);
                        command.Parameters.AddWithValue("EXQ_CREATED_BY", user.Email);
                        command.Parameters.AddWithValue("EXQ_HINT", question.Hint);
                        command.Parameters.AddWithValue("EXQ_STARTING_SQL", question.StartingSql);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                String query = "UPDATE EXERCISES_QUESTIONS";
                query += " SET EXQ_INSTRUCTIONS_TEMPLATE = @EXQ_INSTRUCTIONS_TEMPLATE,";
                query += " EXQ_ANSWERS_TEMPLATE = @EXQ_ANSWERS_TEMPLATE,";
                query += " EXQ_DESCRIPTION = @EXQ_DESCRIPTION,";
                query += " EXQ_HINT = @EXQ_HINT,";
                query += " EXQ_ORDER = @EXQ_ORDER,";
                if (LogUpdatedBy)
                {
                    query += " EXQ_UPDATED_DATE = GETDATE(),";
                    query += " EXQ_UPDATED_BY = @UPDATED_BY,";
                }
                query += " EXQ_STARTING_SQL = @EXQ_STARTING_SQL";
                query += " WHERE EXQ_ID = @EXQ_ID";
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("EXQ_INSTRUCTIONS_TEMPLATE", question.Instructions);
                        command.Parameters.AddWithValue("EXQ_ANSWERS_TEMPLATE", question.AnswerTemplate);
                        command.Parameters.AddWithValue("EXQ_DESCRIPTION", question.Description);
                        command.Parameters.AddWithValue("EXQ_HINT", question.Hint);
                        command.Parameters.AddWithValue("EXQ_ORDER", question.Order);
                        command.Parameters.AddWithValue("EXQ_ID", question.QuestionId);
                        if (LogUpdatedBy) command.Parameters.AddWithValue("UPDATED_BY", user.Email);
                        command.Parameters.AddWithValue("EXQ_STARTING_SQL", question.StartingSql);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void PublishExercise(int ExerciseId, User user)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("UPDATE EXERCISES SET EX_PUBLISHED = 1, EX_PUBLISHED_DATE = GETDATE(), EX_PUBLISHED_BY = @USER WHERE EX_ID = @EX_ID", conn))
                {
                    command.Parameters.AddWithValue("EX_ID", ExerciseId);
                    command.Parameters.AddWithValue("USER", user.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void SaveDataSource(DataSourceModel DataSource, UserModel User)
        {
            // Is this a new data source?
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString))
            {
                conn.Open();
                if (DataSource.DataSourceId == 0)
                {
                    String query = "INSERT INTO DATA_SOURCES";
                    query += " (DS_NAME, DS_CREATED_DATE, DS_CREATED_BY, DS_DELETED)";
                    query += " VALUES";
                    query += " (@DS_NAME, GETDATE(), @DS_CREATED_BY, 0)";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("DS_NAME", DataSource.Name);
                        command.Parameters.AddWithValue("DS_CREATED_BY", User.Email);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new NotImplementedException();

                    String query = "UPDATE DATA_SOURCES";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("DS_NAME", DataSource.Name);
                        command.Parameters.AddWithValue("DS_CREATED_BY", User.Email);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
