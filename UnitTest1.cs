using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
namespace DRMusicUITest
{
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webDrivers";

       
        string url = "https://drmusic20260417123557-fmfkb9ffd7hxgpeh.swedencentral-01.azurewebsites.net/ ";

        [Fact]
        public void GetAllTest()
        {
            //IWebDriver driver = new ChromeDriver(DriverDirectory);
            //IWebDriver driver = new FirefoxDriver(DriverDirectory);
            IWebDriver driver = new EdgeDriver(DriverDirectory);

            driver.Navigate().GoToUrl(url);

            Assert.Equal("DRMusic", driver.Title);

        

            //IWebElement button = driver.FindElement(By.Id("getAllButton"));
            //button.Click();

            //IWebElement message = driver.FindElement(By.Id("recordlist"));
            //Assert.Equal("abc ", message.Text);

            driver.Dispose();

        }
    }
}