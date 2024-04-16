using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.FedCm;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests_Vereschaga1;

public class SeleniumTestsForPractice

{ 
    public ChromeDriver driver;
    
    
    [Test]
    public void Authorization()
    
    { 
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        //  зайти в Хром (с помощью вебдрайвера)
        driver = new ChromeDriver(options);
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); // неявное ожидание
        
        //  перейти по урлу  
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        // IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        // IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name='Username']")));
        
        //  ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("vereshaga.lesika@gmail.com");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Lolesik2024!");
        
        //  нажать кнопку "Войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
       
        // - проверяем что мы находимся на нужной странице
        var news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
        var currentUrl = driver.Url;
        //Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news",
        //"current url = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/news");

        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
        
        
        
        // Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        //Assert.That(driver.FindElement(By.Name("Новости")));
        //typeof(Assert).Name("Новости");
        
        //  закрываем браузер (процесс драйвера)
        
        //currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");

        // закрываем браузер и убиваем процесс драйвера
        driver.Quit();

    }
    
    [Test]
    
    public void Community()
    {
        Authorization();
       
        // - проверяем что мы находимся на нужной странице
        var news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news",
            "current url = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/news");
        




    [TearDown]
    public void TearDown()
    { 
        driver.Quit();
    
    }
}