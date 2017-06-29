using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public static class MultipleChoicePage
    {
        public static void NavigateToPage(int exerciseId)
        {
            SqlConfidenceDriver.Driver.Navigate().GoToUrl(Properties.Settings.Default.BaseUrl + "Exercise/MultipleChoice/?ExerciseId=" + exerciseId);
        }

        public static List<String> Options
        {
            get
            {
                var directiveTag = "multiple-choice-option";
                var elements = SqlConfidenceDriver.Driver.FindElementsByClassName(directiveTag);
                var options = new List<String>();
                foreach (var element in elements)
                {
                    options.Add(element.Text);
                }
                return options;
            }
        }
    }
}
