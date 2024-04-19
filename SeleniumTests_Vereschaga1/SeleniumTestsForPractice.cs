using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Text;
namespace SeleniumTests_Vereschaga1;

public class SeleniumTestsForPractice

{ 
    private ChromeDriver driver;
    private WebDriverWait wait;
    
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='Feed']")));
         }

    [Test]  //  1. Авторизация
    public void CheckNews()
    
    {
        IWebElement news = driver.FindElement(By.CssSelector("[data-tid='Feed']"));
        news.Should().NotBeNull();
        string currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }
    
    
    [Test]  // 2. Переход в диалоги (ГОТОВО)
    public void Dialogs()
    {   
        //     - найти "Диалоги"
        IWebElement buttonMessages = driver.FindElements(By.CssSelector("a[data-tid='Messages']"))[0];
        //     - клик на "Диалоги"
        buttonMessages.Click();
        //     - проверить заголовок "Диалоги"
        string dialog = driver.FindElement(By.CssSelector("h1[data-tid='Title']")).Text;
        dialog.Should().Be("Диалоги");
        //     - проверитьи урл
        string urlDialogs = driver.Url;
        urlDialogs.Should().Be("https://staff-testing.testkontur.ru/messages");
        
    }

    [Test] // 3. личная страница   (ГОТОВО)
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

    [Test]   // 4. Создать сообщество
     public void CreateCommunity()
    {
     // - переход по урл на https://staff-testing.testkontur.ru/communities
     driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
     // - найти и кликнуть на кнопку "Создать"
     IWebElement buttonCreate = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']")).FindElement(By.TagName("button"));
     buttonCreate.Click();
     // - найти и заполнить название 
     
     var uniqueCommunity =  Guid.NewGuid().ToString("N");
     Console.WriteLine(uniqueCommunity);
     //nameCommunity.SendKeys("1newCommunity");
     IWebElement nameCommunity = driver.FindElement(By.CssSelector("textarea[placeholder='Название сообщества']"));
     Console.WriteLine("нашел поле");
     nameCommunity.SendKeys(uniqueCommunity);
     // - найти и кликнуть на кнопку создать
     IWebElement buttonCreateCommunity = driver.FindElement(By.CssSelector("span[data-tid='CreateButton']"));
     buttonCreateCommunity.Click();
     // - найти и кликнуть на ссылку со своим названием
     IWebElement newCommunity = driver.FindElement(By.XPath($"//*[contains(text(),uniqueCommunity)]"));
     // - проверить uniqueCommunity
     newCommunity.Should().NotBeNull();
     



    }
     
     
    [Test]   // 5. поиск по файлам
    public void SearchFile()
    {
        // - перейти по урл https://staff-testing.testkontur.ru/files
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
        // - найти лупу button data-tid="Search"
        IWebElement buttonSearch = driver.FindElement(By.CssSelector("button[data-tid='Search']"));
        // - клик
        buttonSearch.Click();
        // ждем прогрузку страницы
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("label[data-tid='Search']")));
        // - найти поле для поиска
        IWebElement fieldSearch = driver.FindElement(By.CssSelector("label[data-tid='Search']"));
        // - записать значение "Папка"
        fieldSearch.SendKeys("Папка");
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='Feed']")));
        // - выбираем первый ответ, где содержится "Папка" (contain)
        IWebElement nameFile = driver.FindElement(By.CssSelector("div[data-tid='Folders']")).FindElement(By.XPath($"//*[contains(text(),fieldSearch)]"));
        nameFile.Should().NotBeNull();
        


    }
    

    [Test] // 6. создать папку в Файлы
    public void AddFolder()
    {
        // - переход по урл на https://staff-testing.testkontur.ru/files
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
        // - найти и кликнуть "добавить"
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-tid='Title']")));
        IWebElement checkPage = driver.FindElement(By.CssSelector("div[data-tid='Title']"));
        checkPage.Should().NotBeNull();
        IWebElement buttonAdd = driver.FindElement(By.CssSelector("button[type='button']"));
        buttonAdd.Click();
        // - найти и кликнуть "папку"
         IWebElement buttonFolder = driver.FindElement(By.CssSelector("span[data-tid='CreateFolder']"));
         buttonFolder.Click();
        // - ввести название папки
        IWebElement createFolder = driver.FindElement(By.CssSelector("input[placeholder='Новая папка']"));
        createFolder.SendKeys("1NewFolder");
        // - найти активную кнопку и клик "сохранить"
        IWebElement buttonSave = driver.FindElement(By.CssSelector("span[data-tid='SaveButton']"));
        buttonSave.Click();
        // - проверить название папки на странице
        IWebElement newFolder = driver.FindElement(By.XPath("//*[contains(text(),'1NewFolder')]"));
        
        //string dialog = driver.FindElement(By.CssSelector("h1[data-tid='Title']")).Text;
        //dialog.Should().Be("Диалоги");
        
    }
    
    //Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news",
    //"current url = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/news");
        
    // IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
    // IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name='Username']")));

    // IWebElement getToNewCommunity =
    //     driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section/span/a"));
    // getToNewCommunity.Click();
    // string checkName = driver.FindElement(By.CssSelector("div[data-tid='Title']")).Text;
    // checkName.Should().Be(name);
    
    [TearDown]
    public void TearDown()
    { 
        driver.Quit();
    
    }
}