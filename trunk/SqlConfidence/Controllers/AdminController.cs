using Common.Models;
using SqlConfidence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Common;
using DataAccess.Models;
using System.Web.Script.Serialization;
using Common.Questions.Interfaces;

namespace SqlConfidence.Controllers
{
    [AdminRedirect]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataSources()
        {
            List<DataSourceModel> dataSources = _da.ListDataSources();
            return View(dataSources);
        }

        public ActionResult DataSource(DataSource DataSource)
        {
            SqlConfidenceContext context = new SqlConfidenceContext();
            if (!String.IsNullOrEmpty(DataSource.Name))
            {
                try
                {
                    var original = context.DataSources.Find(DataSource.DataSourceId);
                    context.Entry(original).CurrentValues.SetValues(DataSource);
                    original.UpdatedBy = GetCurrentUser().Email;
                    original.UpdatedDate = DateTime.Now;
                    context.SaveChanges();
                    ViewBag.SuccessMessage = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error saving exercise - " + ex.Message);
                    ViewBag.ErrorMessage = "Error saving exercise, please check the logs";
                }
            }

            DataSource = context.DataSources.Single(x => x.DataSourceId == DataSource.DataSourceId);
            ISourceDataAccess dataAccess = SourceDataAccessFactory.CreateDataAccess(Common.Enums.SourceDatabaseType.TSQL);
            ViewBag.Tables = dataAccess.ListAllTables();
            return View(DataSource);
        }

        public JsonResult AddDataSourceTable(int DataSourceId, string TableName)
        {
            Object returnData;
            try
            {
                ISourceDataAccess dataAccess = SourceDataAccessFactory.CreateDataAccess(Common.Enums.SourceDatabaseType.TSQL);
                TableModel table = dataAccess.ListAllTables().Single(x => x.TableName == TableName);

                DataSourceTable dsTable = new DataSourceTable();
                dsTable.DataSourceId = DataSourceId;
                dsTable.CreatedBy = GetCurrentUser().Email;
                dsTable.CreatedDate = DateTime.Now;
                dsTable.DataSourceTableIdGuid = Guid.NewGuid();
                dsTable.TableAlias = table.TableName.Replace("SRC_", "");
                dsTable.TableName = table.TableName;
                dsTable.Columns = new JavaScriptSerializer().Serialize(table.Columns);

                SqlConfidenceContext context = new SqlConfidenceContext();
                context.DataSourceTables.Add(dsTable);
                context.SaveChanges();

                returnData = new { Success = true };
                return Json(returnData);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error adding table, please check the logs" };
                Trace.WriteLine(String.Format("Error adding table {0} to {1} - {2}", TableName, DataSourceId, ex.Message));
                return Json(returnData);
            }
        }

        public ActionResult NewDataSource(DataSource DataSource)
        {
            if (!String.IsNullOrEmpty(DataSource.Name))
            {
                DataSource.CreatedBy = GetCurrentUser().Email;
                DataSource.CreatedDate = DateTime.Now;
                DataSource.DataSourceIdGuid = Guid.NewGuid();

                SqlConfidenceContext context = new SqlConfidenceContext();
                context.DataSources.Add(DataSource);
                context.SaveChanges();

                return RedirectToAction("DataSource", new { DataSourceId = DataSource.DataSourceId, SuccessMessage = "Data Source saved Successfully"});
            }

            return View();
        }

        public ActionResult Exercises()
        {
            List<ExerciseModel> exercises = _da.ListExercises();
            return View(exercises);
        }

        public ActionResult Exercise(ExerciseModel exercise)
        {
            if (!String.IsNullOrEmpty(exercise.Name))
            {
                try
                {
                    _da.SaveExercise(exercise, GetCurrentUser());
                    ViewBag.SuccessMessage = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error saving exercise - " + ex.Message);
                    ViewBag.ErrorMessage = "Error saving exercise, please check the logs";
                }
            }
            
            exercise = _da.GetExercise(exercise.ExerciseId);

            ViewBag.DataSources = _da.ListDataSources();

            return View(exercise);
        }

        public ActionResult NewExercise(ExerciseModel exercise)
        {
            if (exercise.Name != null)
            {
                _da.SaveExercise(exercise, GetCurrentUser());
                return RedirectToAction("Exercises");
            }

            ViewBag.DataSources = _da.ListDataSources();
            return View();
        }

        public JsonResult PublishExercise(int ExerciseId)
        {
            Object returnData;
            try
            {
                ExerciseModel exercise = _da.GetExercise(ExerciseId);
                if (exercise.Published)
                {
                    returnData = new { Success = false, ErrorMessage = "This exercise is already published"};
                    return Json(returnData);
                }
                _da.PublishExercise(ExerciseId, GetCurrentUser());
                returnData = new { Success = true };
                return Json(returnData);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error publishing exercise, please check the logs" };
                Trace.WriteLine(String.Format("Error publishing exercise {0} - {1}", ExerciseId, ex.Message));
                return Json(returnData);
            }
        }

        public JsonResult MoveQuestions(int QuestionOne, int QuestionTwo)
        {
            Object returnData;
            try
            {
                QuestionModel questionOne = _da.GetQuestion(QuestionOne);
                QuestionModel questionTwo = _da.GetQuestion(QuestionTwo);
                questionOne.Order--;
                questionTwo.Order++;
                _da.SaveQuestion(questionOne, GetCurrentUser(), false);
                _da.SaveQuestion(questionTwo, GetCurrentUser(), false);

                returnData = new { Success = true };
                return Json(returnData);
            }
            catch (Exception ex) 
            {
                returnData = new { Success = false, ErrorMessage = "Error moving questions, please check the logs" };
                Trace.WriteLine(String.Format("Error moving questions {0} and {1} - {2}", QuestionOne, QuestionTwo, ex.Message));
                return Json(returnData);
            }
        }

        public ActionResult QuestionsSublist(int ExerciseId)
        {
            ExerciseModel exercise = _da.GetExercise(ExerciseId);
            return PartialView("Questions", exercise);
        }
        
        [ValidateInput(false)]
        public ActionResult Question(ExerciseQuestion question)
        {
            var questionService = IocContainer.Container.GetInstance<IQuestionService>();

            if (question.ExerciseQuestionId != 0)
            {
                question = questionService.Get(question.ExerciseQuestionId, null);
            }
            string questionJson = SerializerHelper.Serialize(question);

            return View((object)questionJson);
        }

        [ValidateInput(false)]
        public JsonResult SaveQuestion(ExerciseQuestion question)
        {
            Object returnData;
            try
            {
                var service = IocContainer.Container.GetInstance<IQuestionService>();
                service.Save(question, GetCurrentUser());

                returnData = new { Success = true, Data = SerializerHelper.Serialize(question) };
                return Json(returnData);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error saving question - " + ex.Message };
                Trace.WriteLine(String.Format("Error saving question - {0}", ex.Message));
                return Json(returnData);
            }
        }
    }
}
