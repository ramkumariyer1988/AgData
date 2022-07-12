using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgData.PageModel
{
    public class AgDataPageModel:AgDataBasePage
    {

        public AgDataPageModel(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.LinkText, Using = "COMPANY")]
        public IWebElement LocCompany { get; set; }

        [FindsBy(How = How.LinkText, Using = "Careers")]
        public IWebElement LocCareer { get; set; }

        public void NavigateToJobPage(IWebDriver driver)
        {
            LocCompany.Click();
            LocCareer.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
        }

        public void ScrollToJobs(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,550)", "");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);

        }
    }
}
