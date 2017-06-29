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
using Common.DataSources.Interfaces;
using Common.EntityServices.Exercises.MultipleChoice;

namespace SqlConfidence.Controllers
{
    [AdminRedirect]
    public class AdminController : BaseController
    {
        protected IDataSourceService _dataSourcesService;

        public AdminController()
        {
            _dataSourcesService = IocContainer.Container.GetInstance<IDataSourceService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DataSources()
        //{
        //    List<DataSourceModel> dataSources = _da.ListDataSources();
        //    return View(dataSources);
        //}

        //public ActionResult DataSource(DataSource DataSource)
        //{
        //    SqlConfidenceContext context = new SqlConfidenceContext();
        //    if (!String.IsNullOrEmpty(DataSource.Name))
        //    {
        //        try
        //        {
        //            var original = context.DataSources.Find(DataSource.DataSourceId);
        //            context.Entry(original).CurrentValues.SetValues(DataSource);
        //            original.UpdatedBy = GetActiveUser().Email;
        //            original.UpdatedDate = DateTime.Now;
        //            context.SaveChanges();
        //            ViewBag.SuccessMessage = "Saved Successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            Trace.WriteLine("Error saving exercise - " + ex.Message);
        //            ViewBag.ErrorMessage = "Error saving exercise, please check the logs";
        //        }
        //    }

        //    DataSource = context.DataSources.Single(x => x.DataSourceId == DataSource.DataSourceId);
        //    ISourceDataAccess dataAccess = SourceDataAccessFactory.CreateDataAccess(Common.Enums.SourceDatabaseType.TSQL);
        //    ViewBag.Tables = dataAccess.ListAllTables();
        //    return View(DataSource);
        //}

        //public JsonResult AddDataSourceTable(int DataSourceId, string TableName)
        //{
        //    Object returnData;
        //    try
        //    {
        //        ISourceDataAccess dataAccess = SourceDataAccessFactory.CreateDataAccess(Common.Enums.SourceDatabaseType.TSQL);
        //        TableModel table = dataAccess.ListAllTables().Single(x => x.TableName == TableName);

        //        DataSourceTable dsTable = new DataSourceTable();
        //        dsTable.DataSourceId = DataSourceId;
        //        dsTable.CreatedBy = GetActiveUser().Email;
        //        dsTable.CreatedDate = DateTime.Now;
        //        dsTable.DataSourceTableIdGuid = Guid.NewGuid();
        //        dsTable.TableAlias = table.TableName.Replace("SRC_", "");
        //        dsTable.TableName = table.TableName;
        //        dsTable.Columns = new JavaScriptSerializer().Serialize(table.Columns);

        //        SqlConfidenceContext context = new SqlConfidenceContext();
        //        context.DataSourceTables.Add(dsTable);
        //        context.SaveChanges();

        //        returnData = new { Success = true };
        //        return Json(returnData);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData = new { Success = false, ErrorMessage = "Error adding table, please check the logs" };
        //        Trace.WriteLine(String.Format("Error adding table {0} to {1} - {2}", TableName, DataSourceId, ex.Message));
        //        return Json(returnData);
        //    }
        //}

        //public ActionResult NewDataSource(DataSource DataSource)
        //{
        //    if (!String.IsNullOrEmpty(DataSource.Name))
        //    {
        //        DataSource.CreatedBy = GetActiveUser().Email;
        //        DataSource.CreatedDate = DateTime.Now;
        //        DataSource.DataSourceIdGuid = Guid.NewGuid();

        //        SqlConfidenceContext context = new SqlConfidenceContext();
        //        context.DataSources.Add(DataSource);
        //        context.SaveChanges();

        //        return RedirectToAction("DataSource", new { DataSourceId = DataSource.DataSourceId, SuccessMessage = "Data Source saved Successfully"});
        //    }

        //    return View();
        //}
        
        //public ActionResult Exercise(ExerciseModel exercise)
        //{
        //    if (!String.IsNullOrEmpty(exercise.Name))
        //    {
        //        try
        //        {
        //            _da.SaveExercise(exercise, GetActiveUser());
        //            ViewBag.SuccessMessage = "Saved Successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            Trace.WriteLine("Error saving exercise - " + ex.Message);
        //            ViewBag.ErrorMessage = "Error saving exercise, please check the logs";
        //        }
        //    }
            
        //    exercise = _da.GetExercise(exercise.ExerciseId);

        //    ViewBag.DataSources = _da.ListDataSources();

        //    return View(exercise);
        //}

        //public JsonResult PublishExercise(int ExerciseId)
        //{
        //    Object returnData;
        //    try
        //    {
        //        ExerciseModel exercise = _da.GetExercise(ExerciseId);
        //        if (exercise.Published)
        //        {
        //            returnData = new { Success = false, ErrorMessage = "This exercise is already published"};
        //            return Json(returnData);
        //        }
        //        _da.PublishExercise(ExerciseId, GetActiveUser());
        //        returnData = new { Success = true };
        //        return Json(returnData);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData = new { Success = false, ErrorMessage = "Error publishing exercise, please check the logs" };
        //        Trace.WriteLine(String.Format("Error publishing exercise {0} - {1}", ExerciseId, ex.Message));
        //        return Json(returnData);
        //    }
        //}

        //public JsonResult MoveQuestions(int QuestionOne, int QuestionTwo)
        //{
        //    Object returnData;
        //    try
        //    {
        //        QuestionModel questionOne = _da.GetQuestion(QuestionOne);
        //        QuestionModel questionTwo = _da.GetQuestion(QuestionTwo);
        //        questionOne.Order--;
        //        questionTwo.Order++;
        //        _da.SaveQuestion(questionOne, GetActiveUser(), false);
        //        _da.SaveQuestion(questionTwo, GetActiveUser(), false);

        //        returnData = new { Success = true };
        //        return Json(returnData);
        //    }
        //    catch (Exception ex) 
        //    {
        //        returnData = new { Success = false, ErrorMessage = "Error moving questions, please check the logs" };
        //        Trace.WriteLine(String.Format("Error moving questions {0} and {1} - {2}", QuestionOne, QuestionTwo, ex.Message));
        //        return Json(returnData);
        //    }
        //}

        //public ActionResult QuestionsSublist(int ExerciseId)
        //{
        //    ExerciseModel exercise = _da.GetExercise(ExerciseId);
        //    return PartialView("Questions", exercise);
        //}
        
        //[ValidateInput(false)]
        //public ActionResult Question(ExerciseQuestion question)
        //{
        //    var questionService = IocContainer.Container.GetInstance<IQuestionService>();

        //    if (question.ExerciseQuestionId != 0)
        //    {
        //        question = questionService.Get(question.ExerciseQuestionId, null);
        //    }
        //    string questionJson = SerializerHelper.Serialize(question);

        //    return View((object)questionJson);
        //}
    }
}
