using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common.Models;
using Common.Enums;
using Common.QueryBuilder;
using System.Text.RegularExpressions;
using DataAccess.Models;
using Common.EntityServices.Exercises.Base;

namespace Common
{
    public class Database
    {
        protected ISourceDataAccess _sourceDA;
        protected DataAccess _da;

        public Database(DataAccess da)
        {
            _sourceDA = SourceDataAccessFactory.CreateDataAccess(SourceDatabaseType.TSQL);
            _da = new DataAccess();
        }

        public DataTable ExecuteUserQuery(int QuestionId, string Query, User User)
        {

            ValidateUserQuery(Query);

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
            {             
                try
                {
                    conn.Open();
                    var exerciseService = IocContainer.Container.GetInstance<IExerciseService>();
                    MultipleChoiceExercise exercise = (MultipleChoiceExercise)exerciseService.GetByQuestion(QuestionId, User);
                    var question = exercise.Questions.Single(x => x.ExerciseQuestionId == QuestionId);

                    // Start a transaction first
                    _sourceDA.BeginUserTransaction(conn);
                    IQueryBuilder queryBuilder = QueryBuilderFactory.CreateQueryBuilder(SourceDatabaseType.TSQL);

                    // Do any commands that the user has already ran on this data source (Execute Query type only, not submitted answers)
                    if (User.UserId != -1)
                    {
                        List<UserActionModel> queries = _da.ListQuestionQueries(question.ExerciseQuestionId, User.UserId);

                        foreach (UserActionModel query in queries)
                        {
                            try
                            {
                                String userQuery = queryBuilder.BuildQuery(query.Description, exercise, question);
                                _sourceDA.ExecuteUserQuery(userQuery, conn);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    // Do the commands
                    String builtQuery = queryBuilder.BuildQuery(Query, exercise, question);

                    DataTable results = _sourceDA.GetDataTable(builtQuery, conn, 100);

                    return results;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    // Roll back the transaction
                    _sourceDA.RollBackUserTransaction(conn);
                }
            }
        }
        
        public Boolean CheckAnswer(int QuestionId, string Query, User User, out DataDifferencesModel Differences, string AnswerQuery = "", string SqlToRunFirst = "")
        {
            ValidateUserQuery(Query);

            var exerciseService = IocContainer.Container.GetInstance<IExerciseService>();
            MultipleChoiceExercise exercise = (MultipleChoiceExercise)exerciseService.GetByQuestion(QuestionId, User);
            var question = exercise.Questions.Single(x => x.ExerciseQuestionId == QuestionId);

            IQueryBuilder queryBuilder = QueryBuilderFactory.CreateQueryBuilder(SourceDatabaseType.TSQL);

            if (String.IsNullOrEmpty(AnswerQuery)) AnswerQuery = question.AnswerTemplate;

            String userQuery = queryBuilder.BuildQuery(Query, exercise, question);
            String answerQuery = queryBuilder.BuildQuery(AnswerQuery, exercise, question);

            using(SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseDataSourcesConnectionString))
            {
                try
                {
                    conn.Open();
                    _sourceDA.BeginUserTransaction(conn);

                    // Do any commands that the user has already ran on this data source (Execute Query type only, not submitted answers)
                    if (User.UserId != -1)
                    {
                        List<UserActionModel> queries = _da.ListQuestionQueries(question.ExerciseQuestionId, User.UserId);

                        foreach (UserActionModel query in queries)
                        {
                            try
                            {
                                String previouslyRunUserQuery = queryBuilder.BuildQuery(query.Description, exercise, question);
                                _sourceDA.ExecuteUserQuery(previouslyRunUserQuery, conn);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    
                    DataTable userQueryData = _sourceDA.GetDataTable(userQuery, conn);
                    _sourceDA.RollBackUserTransaction(conn);

                    _sourceDA.BeginUserTransaction(conn);
                    if(!string.IsNullOrEmpty(SqlToRunFirst))
                    {
                        String queryToRun = queryBuilder.BuildQuery(SqlToRunFirst, exercise, question);
                        _sourceDA.ExecuteUserQuery(queryToRun, conn);
                    }
                    
                    DataTable answerQueryData = _sourceDA.GetDataTable(answerQuery, conn);
                    _sourceDA.RollBackUserTransaction(conn);
                    // Stop at the first difference if we are being called from the unit test runner
                    var stopAtFirstDifference = !String.IsNullOrEmpty(SqlToRunFirst);
                    Differences = new DifferenceEngine(@StopAtFirstDifference: stopAtFirstDifference).GetDifferences(userQueryData, answerQueryData);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error checking answer - " + ex.Message);
                }
                finally
                {
                    _sourceDA.RollBackUserTransaction(conn);
                }
                
            }
            
            // The answer is correct if there were no differences
            return Differences.Differences.Count == 0;
        }

        private void ValidateUserQuery(String Query)
        {
            Query = Query.ToLower();
            if (Query.Contains("transaction")) throw new Exception("User queries must not contain transactions");
            if (Query.Contains("drop")) throw new Exception("User queries must not contain drop");
            // Allow delete in a word, but not the command itself
            if (Regex.IsMatch(Query, @"\bdelete\b", RegexOptions.IgnoreCase)) throw new Exception("User queries must not contain delete");
            if (Query.Contains("rollback")) throw new Exception("User queries must not contain transactions");
            if (Query.Contains("commit")) throw new Exception("User queries must not contain transactions");
            if (Query.Contains("bulk insert")) throw new Exception("User queries must not contain bulk inserts");
            if (Query.Contains("cursor")) throw new Exception("User queries must not contain bulk cursors");
            if (Query.Contains("truncate")) throw new Exception("User queries must not contain truncate commands");
            if (Query.Contains("alter") && Query.Contains("table")) throw new Exception("User queries must not alter tables");
            if (Query.Contains("create") && Query.Contains("table")) throw new Exception("User queries must not create tables");
            if (Query.Contains("grant") || Query.Contains("revoke")) throw new Exception("User queries must not alter user permissions");
            if (Regex.IsMatch(Query, @"\buse\b", RegexOptions.IgnoreCase)) throw new Exception("User queries must not contain use statements");
            if (Query.Contains("information_schema")) throw new Exception("User queries must not access any information schema objects");
            if (Query.Contains("sys")) throw new Exception("User queries must not access any system tables or views");
            if (Query.Contains("constraint")) throw new Exception("User queries must not include constraints");
            if (Query.Contains("tempdb")) throw new Exception("User queries must not use tempdb");
            if (Query.Contains("spt_values")) throw new Exception("User queries must not use spt_values");
            if (Query.Contains("exec")) throw new Exception("User queries must not use exec");
            if (Query.Contains("while")) throw new Exception("User queries must not use while");
            if (Query.Contains("wait")) throw new Exception("User queries must not use wait");
            if (Query.Contains("delay")) throw new Exception("User queries must not use delay");
            if (Query.Contains("maxrecursion")) throw new Exception("User queries must not use maxrecursion");

            var strippedQuery = Regex.Replace(Query, "[^a-z0-9% ._]", string.Empty);
            String[] parts = strippedQuery.Split(' ');

            if (parts.Where(x => x.Contains("join")).Count() > 5) throw new Exception("User queries must not contain large numbers of joins");
            if (parts.Where(x => x.Contains("union")).Count() > 5) throw new Exception("User queries must not contain large numbers of unions");
        }

    }
}
