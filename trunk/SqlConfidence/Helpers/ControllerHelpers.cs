using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqlConfidence.Helpers
{
    public class ControllerHelpers
    {
        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string TranslateQueryException(String Message)
        {
            if (Message.Contains("No column name was specified"))
            {
                return "You must specify an alias for each column.  e.g. select count(*) 'Count', rather than just select count(*)";
            }
            else if (Message.Contains("must have an equal number of expressions"))
            {
                return "The number of columns in your query do not match the answer.";
            }
            else
            {
                //TODO: report this
                return "There was an error - " + Message;
            }
        }
    }
}