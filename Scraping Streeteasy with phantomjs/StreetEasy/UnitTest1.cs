using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Text;

namespace SeleniumSample
{
    [TestClass]
    public class SeleniumTests
    {
        [TestMethod]
        public void TheBingSearchTest()
        {
            TestContext.BeginTimer("BingSearchTest_Navigate");
            _driver.Navigate().GoToUrl("http://www.bing.com/");
            TestContext.EndTimer("BingSearchTest_Navigate");

            TestContext.BeginTimer("BingSearchTest_SearchBHarry");
            _driver.FindElement(By.Id("sb_form_q")).SendKeys("Brian harry blog");
            _driver.FindElement(By.Id("sb_form_go")).Click();
            TestContext.EndTimer("BingSearchTest_SearchBHarry");

            var elementText = _driver.FindElement(By.XPath("//ol[@id='b_results']/li/h2/a"));
            Assert.IsTrue(elementText.Text.Equals("Brian Harry's blog - Site Home - MSDN Blogs"), "Verified title of the blog page");
        }

        public TestContext TestContext { get; set; }

        #region Additional test attributes

        [TestInitialize]
        public void SetupTestSuite()
        {
            Console.WriteLine("Test init called: {0}");
            _driver = new PhantomJSDriver();
        }

        [TestCleanup]
        public void CleanupTestSuite()
        {
            _driver.Quit();
        }
        #endregion
        private IWebDriver _driver;
    }
}