using AgData.PageModel;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;

namespace AgData
{
    public class AgDataTestCases:AgDataBasePage
    {
        private ILog Log;
        protected static IWebDriver driver = GetDriver();
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log = LogManager.GetLogger(GetType());
        }

        //Instantiate the driver,open Chrome browser and Open the URL 
        [SetUp]
        public void StartTest()
        {
            Log.Info("Test Started");
            SetUp(driver);

        }

        //Test the Functionality of Job Search
        [Test]
        public void ClickOnManagerJob()
        {
            Log.Info("Job Page Tests Started");
            AgDataPageModel apm = new AgDataPageModel(driver);
            apm.NavigateToJobPage(driver);
            apm.ScrollToJobs(driver);
            //Since it is in a frame , we have to switch to this frame
            driver.SwitchTo().Frame("HBIFRAME");

            List<IWebElement> list = new List<IWebElement>(driver.FindElements(By.XPath("//span[@class='job']//a")));
            for (int i = 0; i < list.Count-1; i++)
            {
                if (list[i].Text.Contains("Manager"))
                {
                    list[i].Click();
                    break;
                }
            }

        }

        //Close the Browser
        [TearDown]
        public void EndTest()
        {
            Log.Info("Test Ended");
            Close(driver);
        }

    }
}

        
