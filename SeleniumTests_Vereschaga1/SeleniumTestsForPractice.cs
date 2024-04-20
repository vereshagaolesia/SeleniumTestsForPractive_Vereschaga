using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace SeleniumTests_Vereschaga1;

public class SeleniumTestsForPractice

{ 
    private ChromeDriver driver;
    private WebDriverWait wait;
    
    [SetUp]
        public void SetUp()
    
        {       // создаем опции для запуска браузера
            ChromeOptions options = new ChromeOptions();
                // прописываем опции
            options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
                // создаем драйвер с опциями
            driver = new ChromeDriver(options);
                // прописываем неявное ожидание
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                // прописываем явное ожидание
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                // вызываем метод авторизации
            SignIn();
        }

    public void SignIn()

    {       // объявляем переменные
        string login = "vereshaga.lesika@gmail.com";
        string password = "Lolesik2024!";
        
            // переходим по урл на страницу Контура для входа
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
            // ищем поле для ввода логина
        IWebElement inputLogin = driver.FindElement(By.Id("Username"));
            // вводим логин
        inputLogin.SendKeys(login);
            // ищем поле для ввода пароля
        IWebElement inputPassword = driver.FindElement(By.Name("Password"));
            // вводим пароль
        inputPassword.SendKeys(password);
            // ищем кнопку "Войти"
        IWebElement SignInbutton = driver.FindElement(By.Name("button"));
            // кликаем
        SignInbutton.Click();
            // ждем прогрузку страницы Новости
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='Feed']")));
    }


    [Test]  //  1. Авторизация
    public void Authorization()
    
    {       // ищем заголовок Новости
        string news = driver.FindElement(By.CssSelector("h1[data-tid='Title']")).Text;
            // беглая проверка заголовка Новости
        news.Should().Be("Новости");
            // объявляем переход по урл
        string currentUrl = driver.Url;
            // беглая проверка урл
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }
    
    
    [Test] // 2. Переход в диалоги
    public void ToDialogs()
    {   
            // найти кнопку "Диалоги"
        IWebElement buttonMessages = driver.FindElements(By.CssSelector("a[data-tid='Messages']"))[0];
            // клик на кнопку "Диалоги"
        buttonMessages.Click();
            // проверить заголовок "Диалоги"
        string dialog = driver.FindElement(By.CssSelector("h1[data-tid='Title']")).Text;
            // беглая проверка заголовка "Диалоги"
        dialog.Should().Be("Диалоги");
            // объявляем переход по урл
        string urlDialogs = driver.Url;
            // беглая проверка урл
        urlDialogs.Should().Be("https://staff-testing.testkontur.ru/messages");
        
    }

    [Test] // 3. Переход на личную страницу
    public void ToMyProfile()
    {       // объявляем переменные
        string name = "Олеся Верещага";
        string myID = "0baa364d-2e32-47f8-9e88-7f3ac4233064";
            // ищем аватар, чтобы открыть выпадающее меню
        IWebElement dropDownMenu = driver.FindElement(By.CssSelector("div[data-tid='Avatar']"));
            // кликаем на аватар
        dropDownMenu.Click();
            // ищем кнопку "Мой профиль"
        IWebElement myProfile = driver.FindElement(By.CssSelector("span[data-tid='Profile']"));
            // кликаем по кнопке "Мой профиль"
        myProfile.Click();
            // ищем имя на странице
        string myName = driver.FindElement(By.CssSelector("[data-tid='EmployeeName']")).Text; 
            // беглая проверка имени
        myName.Should().Be(name);
            // объявляем переход по урл
        string urlMyProfile = driver.Url;
            // беглая проверка урл
        urlMyProfile.Should().Be("https://staff-testing.testkontur.ru/profile/"+myID);
    }

    [Test]   // 4. Создание сообщества
     public void CreateCommunity()
    {
            // - переход по урл https://staff-testing.testkontur.ru/communities
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
            // ищем кнопку "Создать" в блоке заголовка
        IWebElement buttonCreate = driver.FindElement(By.CssSelector("section[data-tid='PageHeader']")).
         FindElement(By.TagName("button"));
            // кликаем по кнопке "Создать"
        buttonCreate.Click();
            // - найти и заполнить название 
            // создаем уникальное название сообщества
        var uniqueCommunity =  Guid.NewGuid().ToString("N");
            // ищем поля для воода названия сообщества
        IWebElement nameCommunity = driver.FindElement(By.CssSelector("textarea[placeholder='Название сообщества']"));
            // вводим уникальное название
        nameCommunity.SendKeys(uniqueCommunity);
            // ищем кнопку "Создать"
        IWebElement buttonCreateCommunity = driver.FindElement(By.CssSelector("span[data-tid='CreateButton']"));
            // кликаем по кнопке "Создать"
        buttonCreateCommunity.Click();
            // ищем свое название
        string newCommunity = driver.FindElement(By.XPath($"//*[contains(text(),uniqueCommunity)]")).Text;
            // - бегло проверяем уникальное название сообщества
        newCommunity.Should().Contain(uniqueCommunity);
        
    }
     
    [Test]   // 5. поиск по файлам
    public void SearchFile()
    {
            // объявляем переменную
        string name = "Папка";
            // переходим по урл https://staff-testing.testkontur.ru/files
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
            // ищем лупу (кнопку поиска)
        IWebElement buttonSearch = driver.FindElement(By.CssSelector("button[data-tid='Search']"));
            // кликаем по кнопке поиска
        buttonSearch.Click();
            // ждем прогрузку страницы
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("label[data-tid='Search']")));
            // ищем поле для ввода
        IWebElement fieldSearch = driver.FindElement(By.CssSelector("label[data-tid='Search']"));
            // вводим "Папка"
        fieldSearch.SendKeys(name);
            // ждем прогрузку страницы
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-tid='Folders']")));
            // выбираем первый ответ, где содержится "Папка"
        //string nameFile = driver.FindElement(By.CssSelector("div[data-tid='Folders']")).FindElement(By.XPath($"//*[contains(text(),'Папка')]")).Text;
        string nameFile = driver.FindElement(By.CssSelector("div[data-tid='Folders']")).FindElement(By.CssSelector("li[data-tid='ListItemWrapper']")).Text;
            // - бегло проверяем уникальное название сообщества
        //nameFile.Should().NotBeNull();
        nameFile.Should().NotBeNull();

    }
    

    [Test] // 6. создать папку в Файлы
    public void AddFolder()
    {
        string name = "1NewFolder";
            // переходим по урл на https://staff-testing.testkontur.ru/files
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
            // ждем прогрузку страницы
        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-tid='Title']")));
            // ищем кнопку "Добавить"
        IWebElement buttonAdd = driver.FindElement(By.CssSelector("button[type='button']"));
            // кликаем на кнопку "Добавить"
        buttonAdd.Click();
            // ищем кнопку "Папку"
         IWebElement buttonFolder = driver.FindElement(By.CssSelector("span[data-tid='CreateFolder']"));
            // кликаем на кнопку "Папку"
         buttonFolder.Click();
            // ищем поле для ввода
        IWebElement createFolder = driver.FindElement(By.CssSelector("input[placeholder='Новая папка']"));
            // вводим название папки
        createFolder.SendKeys(name);
            // ищем кнопку "Сохранить"
        IWebElement buttonSave = driver.FindElement(By.CssSelector("span[data-tid='SaveButton']"));
            // кликаем на кнопку "Сохранить"
        buttonSave.Click();
            // ищем папку с своим названием
        IWebElement newFolder = driver.FindElement(By.XPath("//*[contains(text(),name)]"));
            // беглая проверка
        newFolder.Should().NotBeNull();

    }
    
    [TearDown]
    public void TearDown()
    { 
        driver.Close();
        driver.Quit();
    
    }
}