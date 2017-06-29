using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqlConfidence.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AskAQuestion(String UserEmail = "", String Question = "")
        {
            String errorMessage = "";
            if (!String.IsNullOrEmpty(UserEmail) || !String.IsNullOrEmpty(Question))
            {
                if (!IsValidEmail(UserEmail))
                {
                    errorMessage = "Please enter a valid Email";
                }
                if (String.IsNullOrEmpty(Question))
                {
                    errorMessage = "Please submit a question";
                }
                try
                {
                    if (errorMessage == "")
                    {
                        Emailer emailer = new Emailer();
                        emailer.SendQuestionEmail(UserEmail, Question);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(String.Format("Error sending question email to {0}, '{1}' - {2}", UserEmail, Question, ex.Message));
                    errorMessage = "An error occurred sending your email, please try again";
                }
            }

            ViewBag.ErrorMessage = errorMessage;
            ViewBag.UserEmail = UserEmail;
            ViewBag.Question = Question;
            return View();
        }

        public ActionResult TermsAndConditions()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
		
		public ActionResult About()
        {
            return View();
        }

        public ActionResult StumbleUpon()
        {
            ExerciseController c = new ExerciseController();
            return c.Exercise(3);
        }
    }
}
