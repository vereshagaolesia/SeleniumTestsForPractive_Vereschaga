using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace SeleniumTestsForPractice1;

public class SeleniumTestsForPractice
{
    public void Authorization()

    {
        [Test]
        //  зайти в Хром (с помощью вебдрайвера)
        var driver = new ChromeDriver();
        
        //  перейти по урлу  
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        //  dвести логин и пароль
        //  нажать кнопку "Войти"
        //  проверяем, что находимся на нужной странице
    }
    
}