using Common;
using Common.BusinessLogicServices;
using Common.EntityServices.Exercises.Base;
using Common.EntityServices.Exercises.MultipleChoice;
using Common.EntityServices.Users;
using Common.Enums;
using Common.Models;
using DataAccess.Models;
using SqlConfidence.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SqlConfidence.Controllers
{
    public class ExerciseController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("ListExercises");
        }

        public ActionResult ListExercises()
        {
            var service = IocContainer.Container.GetInstance<IExerciseService>();
            List<Exercise> exercises = service.GetAll(GetActiveUser());
            return View(exercises);
        }

        public ActionResult Exercise(int ExerciseId)
        {
            var service = IocContainer.Container.GetInstance<IExerciseService>();
            var exercise = service.Get(ExerciseId, GetActiveUser());
            String exerciseJson = SerializerHelper.Serialize(exercise);
            return View((object)exerciseJson);
        }

        public ActionResult ExerciseNew(int ExerciseId)
        {
            var service = IocContainer.Container.GetInstance<IExerciseService>();
            var exercise = service.Get(ExerciseId, GetActiveUser());
            String exerciseJson = SerializerHelper.Serialize(exercise);
            return View((object)exerciseJson);
        }

        public ActionResult MultipleChoice(int ExerciseId)
        {
            var service = IocContainer.Container.GetInstance<IMultipleChoiceExerciseService>();
            var exercise = service.Get(ExerciseId, GetActiveUser());
            String exerciseJson = SerializerHelper.Serialize(exercise);
            return View((object)exerciseJson);
        }

        public String ExecuteUserQueryJson(int QuestionId, String Query)
        {
            _da.LogUserQuery(GetCurrentUserId(), Query, QuestionId, UsersActionsTypes.ExecuteQuery);
            return this.ExecuteQueryJson(QuestionId, Query, GetCurrentUserId());
        }

        public String ExecuteQueryJson(int QuestionId, String Query, int UserId)
        {
            try
            {
                var db = new Database(_da);
                var userService = IocContainer.Container.GetInstance<IUserService>();
                var results = db.ExecuteUserQuery(QuestionId, Query, userService.Get(UserId));
                var dtModel = DataTableModelHelper.DataTableToModel(results);
                return new JavaScriptSerializer().Serialize(dtModel);
            }
            catch (Exception ex)
            {
                return ControllerHelpers.TranslateQueryException(ex.Message);
            }
        }

        public String GetAnswerPreview(int QuestionId)
        {
            Database db = new Database(_da);
            QuestionModel question = _da.GetQuestion(QuestionId);
            DataTable results = db.ExecuteUserQuery(QuestionId, question.AnswerTemplate, new User() { UserId = -1 });
            return ControllerHelpers.RenderViewToString(this.ControllerContext, "ExecuteUserQuery", results);
        }

        public String GetAnswer(int QuestionId)
        {
            try
            {
                QuestionModel question = _da.GetQuestion(GetCurrentUserId(), QuestionId);
                return question.AnswerTemplate;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String GetHint(int QuestionId)
        {
            try
            {                
                QuestionModel question = _da.GetQuestion(GetCurrentUserId(), QuestionId);
                return question.Hint;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public JsonResult CheckAnswer(int QuestionId, String Query)
        {
            DataDifferencesModel differences;
            try
            {
                if (Query.Contains("Answer") && QuestionId != 41) throw new Exception("You can't use the answer table in your answer");

                Database db = new Database(_da);
                Boolean correct = db.CheckAnswer(QuestionId, Query, GetActiveUser(), out differences);

                var exerciseService = IocContainer.Container.GetInstance<IExerciseService>();
                
                _da.LogUserQuery(GetCurrentUserId(), Query, QuestionId, UsersActionsTypes.CheckAnswer);
                if (correct) exerciseService.MarkQuestionAsAnswered(QuestionId, GetActiveUser());
                
                String differencesHTML = ControllerHelpers.RenderViewToString(this.ControllerContext, "AnswerDifferences", differences);
                return Json(new { correctAnswer = correct, differences = differencesHTML });
            }
            catch (Exception ex)
            {
                return Json(new { error = ControllerHelpers.TranslateQueryException(ex.Message) });
            }
        }

        public JsonResult LogCorrectAnswer(int QuestionId)
        {
            Object returnData;
            try
            {
                var exerciseService = IocContainer.Container.GetInstance<IExerciseService>();
                exerciseService.MarkQuestionAsAnswered(QuestionId, GetActiveUser());
                returnData = new { Success = true };
                return Json(returnData);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error logging correct answer - " + ex.Message };
                Trace.WriteLine(String.Format("Error logging correct answer - {0}", ex.Message));
                return Json(returnData);
            }
        }

        public JsonResult RunUnitTest(int QuestionId, Guid UnitTestId)
        {
            Object returnData;
            try
            {
                var service = IocContainer.Container.GetInstance<IExerciseService>();
                var exercise = service.GetByQuestion(QuestionId, null);
                var question = exercise.ExerciseQuestions.Single(x => x.ExerciseQuestionId == QuestionId);
                var test = question.ExerciseQuestionUnitTests.Single(x => x.ExerciseQuestionUnitTestId == UnitTestId);
                var testRunner = new UnitTestRunner();
                var passed = testRunner.RunUnitTest(test, GetActiveUser());
                returnData = new { Success = true, Passed = passed };
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error running unit test - " + ex.Message };
                Trace.WriteLine(String.Format("Error running unit test - {0}", ex.Message));
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Reset(int QuestionId)
        {
            Object returnData;
            try
            {
                _da.ResetQuestion(GetCurrentUserId(), QuestionId);
                returnData = new { Success = true};
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                returnData = new { Success = false, ErrorMessage = "Error resetting data - " + ex.Message };
                Trace.WriteLine(String.Format("Error resetting data - {0}", ex.Message));
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetQuestionsList(int ExerciseId)
        {
            try
            {
                List<QuestionModel> questions = _da.ListQuestions(GetCurrentUserId(), ExerciseId);
                return PartialView("Exercise_Partial_QuestionsList", questions);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error getting questions list - " + ex.Message);
                throw new Exception("There was an error loading the questions list");
            }
        }

        public ActionResult Exercise_Partial_DatabaseSchema()
        {
            return PartialView("Exercise_Partial_DatabaseSchema");
        }
    }
}
