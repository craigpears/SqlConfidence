using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class SqlConfidenceDriver
    {
        private static ChromeDriver _driver;
        public static ChromeDriver Driver
        {
            get
            {
                if (_driver == null) _driver = new ChromeDriver();
                return _driver;
            }
        }
    }
}
