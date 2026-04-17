using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace DRMusicUITest
{
    public class UnitTest1
    {
        // Original URL (preserved)
        string url = "https://drmusic20260417123557-fmfkb9ffd7hxgpeh.swedencentral-01.azurewebsites.net/ ";

        [Fact]
        public void GetAllTest()    
        {
            //IWebDriver driver = new ChromeDriver(DriverDirectory);
            //IWebDriver driver = new FirefoxDriver(DriverDirectory);
            IWebDriver driver = new EdgeDriver();

            try
            {
                driver.Navigate().GoToUrl(url);

                Assert.Equal("DRMusic", driver.Title);

                // Click the "Get all records" button and wait for either records or "No Records" message
                var getAllButton = driver.FindElement(By.Id("getAllButton"));
                getAllButton.Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                bool uiUpdated = wait.Until(d =>
                    d.FindElements(By.CssSelector("#recordlist li")).Count > 0
                    || d.FindElements(By.XPath("//*[normalize-space(text())='No Records']")).Count > 0);

                Assert.True(uiUpdated, "Expected either record list items or 'No Records' message to appear.");
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void FilterUiElementsExistTest()
        {
            IWebDriver driver = new EdgeDriver();

            try
            {
                driver.Navigate().GoToUrl(url);

                // Verify the filter input and button are present
                var titleInput = driver.FindElement(By.CssSelector("input[placeholder='Title']"));
                var filterButton = driver.FindElements(By.XPath("//button[normalize-space(text())='Filter']")).FirstOrDefault();

                Assert.True(titleInput.Displayed, "Filter title input should be displayed.");
                Assert.NotNull(filterButton);
                Assert.True(filterButton.Displayed, "Filter button should be displayed.");
            }
            finally
            {
                driver.Quit();
            }
        }

        [Fact]
        public void LoginUiElementsExistTest()
        {
            IWebDriver driver = new EdgeDriver();

            try
            {
                driver.Navigate().GoToUrl(url);

                // Verify login inputs and login button exist
                var username = driver.FindElement(By.CssSelector("input[placeholder='Username']"));
                var password = driver.FindElement(By.CssSelector("input[placeholder='Password']"));
                var loginButton = driver.FindElements(By.XPath("//button[normalize-space(text())='Login']")).FirstOrDefault();

                Assert.True(username.Displayed, "Username input should be displayed.");
                Assert.True(password.Displayed, "Password input should be displayed.");
                Assert.NotNull(loginButton);
                Assert.True(loginButton.Displayed, "Login button should be displayed.");
            }
            finally
            {
                driver.Quit();
            }
        }
        [Fact]
        public void LoginTest()
        {
            IWebDriver driver = new EdgeDriver();

            try
            {
                driver.Navigate().GoToUrl(url);
                Assert.Equal("DRMusic", driver.Title);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                // wait then find inputs
                var username = wait.Until(d => d.FindElement(By.CssSelector("input[placeholder='Username']")));
                var password = driver.FindElement(By.CssSelector("input[placeholder='Password']"));

                // clear any existing text then type
                username.Clear();
                username.SendKeys("admin");     
                password.Clear();
                password.SendKeys("1234");  

                var loginButton = driver.FindElements(By.XPath("//button[normalize-space(text())='Login']")).FirstOrDefault();
                Assert.NotNull(loginButton);
                loginButton.Click();

                // optional: wait for logged-in UI to appear
                bool loggedIn = wait.Until(d => d.FindElements(By.XPath("//*[contains(normalize-space(.),'Logged in as')]")).Count > 0);
                Assert.True(loggedIn);
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}