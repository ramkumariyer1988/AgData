using AgData.PageModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgData
{
    public class AgDataBasePage
    {
        public void SetUp(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(AgDataConstants.uri);
            driver.Manage().Window.Maximize();

        }


        public static IWebDriver GetDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            IWebDriver driver = new ChromeDriver(options);
            return driver;                                
        }

        public void Close(IWebDriver driver)
        {
            driver.Quit();

        }     
    }
}
