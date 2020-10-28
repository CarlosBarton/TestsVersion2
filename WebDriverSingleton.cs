using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EvolutionAutomationTests
{
    public sealed class WebDriverSingleton
    {
        private static IWebDriver instance = null;
        private WebDriverSingleton() { }

        public static IWebDriver GetInstance() { 
            if (instance == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("no-sandbox");
                instance = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, System.TimeSpan.FromMinutes(3));
                instance.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
            }
            return instance;
        }

    }
}
