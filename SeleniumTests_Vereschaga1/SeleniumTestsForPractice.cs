using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.FedCm;

namespace SeleniumTests_Vereschaga1;

public class SeleniumTestsForPractice
{
    [Test]
    public void Authorization()
    
    { 
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox",
             "--start-maximized", "--disable-extensions");
        
        //  зайти в Хром (с помощью вебдрайвера)
        var driver = new ChromeDriver(options);
        
        //  перейти по урлу  
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
       Thread.Sleep(3000);
        
        //  ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("vereshaga.lesika@gmail.com");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Lolesik2024!");
        Thread.Sleep(3000);
        
        //  нажать кнопку "Войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(3000);
        
        //  проверяем, что находимся на нужной странице
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        //  закрываем браузер (процесс драйвера)
        driver.Quit();
    }
    
}