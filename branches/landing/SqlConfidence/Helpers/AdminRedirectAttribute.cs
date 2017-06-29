using Common.Models;
using SqlConfidence.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SqlConfidence.Helpers
{
    public class AdminRedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            MethodInfo method = typeof(BaseController).GetMethod("UserIsLoggedIn");
            Boolean userLoggedIn = (Boolean)method.Invoke(filterContext.Controller, null);

            if (!userLoggedIn)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Login"
                }));
                return;
            }

            method = typeof(BaseController).GetMethod("GetCurrentUser");
            UserModel user = (UserModel)method.Invoke(filterContext.Controller, null);
            if (!user.IsAdmin) throw new Exception("User must be an administrator to access the admin console - " + user.Name);
        }
    }
}