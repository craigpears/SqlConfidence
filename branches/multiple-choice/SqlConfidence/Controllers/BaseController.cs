using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common;
using Common.EntityServices.Users;
using DataAccess.Models;

namespace SqlConfidence.Controllers
{
    public class BaseController : Controller
    {
        protected Common.DataAccess _da;

        public BaseController()
        {
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
                    var user = _da.GetUser(
                        Users.BuildUser(
                            new LoginModel() {
                                PlainPassword = Properties.Settings.Default.AutoLoginPassword,
                                UserData = new UserModel() {
                                    Email = Properties.Settings.Default.AutoLoginEmail
                                }
                            }
                        )
                   );
                   Session["User"] = IocContainer.Container.GetInstance<IUserService>().Get(user.UserId);
                }
                else
                {
                    // Otherwise create a guest user so that we can keep track of their queries (needed for unit test exercises)
                    var userService = IocContainer.Container.GetInstance<IUserService>();
                    var guestUser = userService.CreateGuestUser();
                    Session["User"] = guestUser;
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

        public User GetActiveUser()
        {
            return (User)Session["User"];
        }
        
        public int GetCurrentUserId()
        {
            if (UserIsLoggedIn())
            {
                return GetActiveUser().UserId;
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
