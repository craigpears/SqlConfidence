using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Models;
using System.Diagnostics;
using Common.Exceptions;
using Common.EntityServices.Users;

namespace SqlConfidence.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Register(LoginModel login)
        {
            // Check if any details were submitted
            if (login.UserData == null)
            {
                return View();
            }
            else
            {
                UserModel user = Users.BuildUser(login);
                try
                {
                    _da.CreateUser(user);
                }
                catch (UserExistsException ex)
                {
                    Trace.WriteLine(String.Format("Error creating user {0} - {1}", user.Email, ex.Message));
                    ViewBag.ErrorMessage = String.Format("{0} already exists.", ex.UserEmail);
                    return View();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(String.Format("Error creating user {0} - {1}", user.Email, ex.Message));
                    ViewBag.ErrorMessage = "There was an error creating the user.  If this problem persists, please contact support";
                    return View();
                }
                
                // Log in the user
                Session["User"] = _da.GetUser(user);
                return RedirectToAction("Index", "Home");
            }            
        }

        public ActionResult Login(LoginModel login)
        {
            if (login.UserData != null)
            {
                // Process the login
                try
                {
                    UserModel user = _da.GetUser(Users.BuildUser(login));
                    var userService = IocContainer.Container.GetInstance<IUserService>();
                    Session["User"] = userService.Get(user.UserId);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    ViewBag.ErrorMessage = "Incorrect username or password.";
                    return View(login);
                }                
            }

            // Present the login page
            return View(new LoginModel());
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login");
        }
    }
}
