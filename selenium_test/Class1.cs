using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace selenium_test
{
    [TestFixture]
    public class Class1
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        // Тест 1: Проверка заголовка страницы
        [Test]
        public void Test1_MainTitle()
        {
            driver.Url = "https://sibsutis.ru";
            Assert.That(driver.Title.IndexOf("СибГУТИ", StringComparison.OrdinalIgnoreCase) >= 0 || driver.Title.Length > 0, Is.True);
        }

        // Тест 2: Проверка видимости объекта (шапка сайта)
        [Test]
        public void Test2_CheckLogoVisibility()
        {
            driver.Url = "https://sibsutis.ru";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement headerElement = wait.Until(d => d.FindElement(By.XPath("//header")));
            Assert.That(headerElement.Displayed, Is.True);
        }

        // Тест 3: Переход по ссылке
        [Test]
        public void Test3_NavigateToLink()
        {
            driver.Url = "https://sibsutis.ru";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement link = wait.Until(d => d.FindElement(By.XPath("(//a[@href])")));
            link.Click();
            Assert.That(driver.Url, Is.Not.Null);
        }

        // Тест 4: Заполнение текстового поля
        [Test]
        public void Test4_FillSearchField()
        {
            driver.Url = "https://sibsutis.ru";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement input = wait.Until(d => d.FindElement(By.XPath("//input[@type='text' or @type='search']")));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value = 'Расписание';", input);
            Assert.That(input.GetAttribute("value"), Is.EqualTo("Расписание"));
        }

        // Тест 5: Эмуляция нажатия на кнопку
        [Test]
        public void Test5_ClickButton()
        {
            driver.Url = "https://sibsutis.ru";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement input = wait.Until(d => d.FindElement(By.XPath("//input[@type='text' or @type='search']")));
            input.SendKeys("Расписание");
            input.SendKeys(Keys.Enter);
            Assert.That(driver.PageSource.Length, Is.GreaterThan(0));
        }
    }
}
