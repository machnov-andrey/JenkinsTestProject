using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace JenkinsTestProject
{
    public class Tests
    {
        private WebDriver _driver;

        [OneTimeSetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.None;
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var xpath = "//textarea";

            _driver.Navigate().GoToUrl("https://www.google.com/");

            Thread.Sleep(3000);

            new WebDriverWait(_driver, TimeSpan.FromSeconds(15))
                .Until(driver => driver.FindElements(By.XPath(xpath)).Count > 0);
            
            _driver.FindElement(By.XPath(xpath)).SendKeys(Environment.GetEnvironmentVariable("GoogleSearch", 
                EnvironmentVariableTarget.Machine));

            (_driver as ITakesScreenshot).GetScreenshot().SaveAsFile("test.png", ScreenshotImageFormat.Png);

            Thread.Sleep(3000);

            new Actions(_driver).SendKeys(Keys.Enter).Build().Perform();

            Thread.Sleep(3000);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}