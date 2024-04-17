﻿using FluentAssertions;
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
    
    [SetUp]
        public void SignIn()
    
        { 
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
            
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
            //  ввести логин и пароль
            IWebElement login = driver.FindElement(By.Id("Username"));
            login.SendKeys("vereshaga.lesika@gmail.com");
            IWebElement password = driver.FindElement(By.Name("Password"));
            password.SendKeys("Lolesik2024!");
            IWebElement buttonSignIn = driver.FindElement(By.Name("button"));
            buttonSignIn.Click();
         }

    [Test]
    public void Authorization()
    
    {
        IWebElement news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
        news.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }
    
    // - проверяем что мы находимся на нужной странице
    
    //Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news",
    //"current url = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/news");
        
    // IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
    // IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name='Username']")));

    
    [Test]  // 2. диалоги
    public void Dialogs()
    {   
        //     - найти "Диалоги"
        IWebElement buttonMessages = driver.FindElements(By.CssSelector("a[data-tid='Messages']"))[0];
        //     - клик на "Диалоги"
        buttonMessages.Click();
        //     - проверить "диалоги" и урл
        IWebElement dialog = driver.FindElement(By.CssSelector("h1[data-tid='Title']"));
        dialog.Should().NotBeNull();
        string urlDialogs = driver.Url;
        urlDialogs.Should().Be("https://staff-testing.testkontur.ru/messages");
        
    }

    [Test] // 3. личная страница
           // (нужно редактировать тесты, если используются другие логин и пароль)
    public void MyProfile()
    {
        IWebElement dropDownMenu = driver.FindElement(By.CssSelector("div[data-tid='Avatar']"));
        dropDownMenu.Click();
        IWebElement myProfile = driver.FindElement(By.CssSelector("span[data-tid='Profile']"));
        myProfile.Click();
        string myName = driver.FindElement(By.CssSelector("[data-tid='EmployeeName']")).Text; 
        myName.Should().Be("Олеся Верещага");
        string urlMyProfile = driver.Url;
        urlMyProfile.Should().Be("https://staff-testing.testkontur.ru/profile/0baa364d-2e32-47f8-9e88-7f3ac4233064");
    }

    [Test] // 4. поиск по имени (закрыть поиск)
    public void fg()
    {
    }
    // - найти поле ввода имени
    // - ввести ""
    // - проверить
    
    [Test] // 6. создать папку в Файлы (hard)
    public void AddFolder()
    {
        // - переход по урл на https://staff-testing.testkontur.ru/files
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
        // - клик "добавить"
        IWebElement buttonAdd = driver.FindElements(By.CssSelector("span[data-tid='DropdownButton']"))[1];
        buttonAdd.Click();
        // - клик "папку" (скрыто)
         IWebElement buttonFolder = driver.FindElement(By.CssSelector("span[data-tid='CreateFolder']"));
         buttonFolder.Click();
        // - ввести название папки 
        IWebElement newFolder = driver.FindElement(By.CssSelector("label[data-tid='Input']"));
        newFolder.SendKeys("1NewFolder");
        // - найти активную кнопку и клик "сохранить"
        IWebElement buttonSave = driver.FindElement(typeof(By("button")); // доделать!
        buttonSave.Click();
        // - проверить название папки на странице
        
    }
    
    
    
    [TearDown]
    public void TearDown()
    { 
        driver.Quit();
    
    }
}