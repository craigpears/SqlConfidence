using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common;

namespace SqlConfidence.Controllers
{
    public class BaseController : Controller
    {
        protected Common.DataAccess _da;

        public BaseController()
        {
            IocContainer.Init();
            _da = new Common.DataAccess();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["User"] == null)
            {
                if(!String.IsNullOrEmpty(Properties.Settings.Default.AutoLoginEmail))
                {
                    // Auto login for local development
                    Session["User"] = _da.GetUser(
                        Users.BuildUser(
                            new LoginModel() {
                                PlainPassword = Properties.Settings.Default.AutoLoginPassword,
                                UserData = new UserModel() {
                                    Email = Properties.Settings.Default.AutoLoginEmail
                                }
                            }
                        )
                   );
                }
                else
                {
                    // Otherwise create a guest user so that we can keep track of their queries (needed for unit test exercises)
                    var highestId = _da.GetHighestUserId();
                    var guestUser = new UserModel()
                    {
                        Email = string.Format("guest_user{0}@sqlconfidence.com", highestId + 1),
                        FirstName = "Guest",
                        HashedPassword = "###",
                        IsAdmin = false,
                        LastName = "User",
                        IsGuest = true
                    };

                    _da.CreateUser(guestUser);
                    Session["User"] = _da.GetUser(guestUser, false);
                }               
            }
            ViewBag.User = Session["User"];
            ViewBag.SuccessMessage = Request.QueryString["SuccessMessage"] ?? Request.Form["SuccessMessage"];
        }

        public Boolean UserIsLoggedIn()
        {
            if (Session == null) return false;
            return Session["User"] != null;
        }

        public UserModel GetCurrentUser()
        {
            return (UserModel)Session["User"];
        }

        public int GetCurrentUserId()
        {
            if (UserIsLoggedIn())
            {
                return GetCurrentUser().UserId;
            }
            else
            {
                return -1;
            }
        }

        public bool IsValidEmail(string email)
        {
            Regex r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            return !string.IsNullOrEmpty(email) && r.IsMatch(email);
        }
    }
}
