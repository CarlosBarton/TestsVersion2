using NUnit.Framework;
using OpenQA.Selenium;
using Shared;

namespace EvolutionAutomationTests
{

    [SetUpFixture]
    public class Main
    {

        IWebDriver driver;

        [OneTimeSetUp]
        public void Login()
        {
            CommonLogic commonLogic = new CommonLogic();
            driver = WebDriverSingleton.GetInstance();
            driver.Url = commonLogic.GetAppUrl();          
            driver.Manage().Window.Maximize();

            IWebElement inputUser = driver.FindElement(By.Id("txtUsername"));
            inputUser.SendKeys("soporteit");
            IWebElement inputPassword = driver.FindElement(By.Id("txtPassword"));
            inputPassword.SendKeys("soporteit");
            inputPassword.SendKeys(Keys.Enter);
        }

        [OneTimeTearDown]
        public void Logout()
        {
            IWebElement CerrarSesion = driver.FindElement(By.XPath("//*[@id='topmenu']/a[1]"));
            CerrarSesion.Click();
            driver.Close();
        }

    }
}
