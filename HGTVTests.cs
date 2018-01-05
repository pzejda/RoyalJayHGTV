using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;

namespace PeterZejdaHGTVProject
{
    [TestClass]
    public class HGTVTests
    {
        private string HGTVGiveAwayURL = "http://www.hgtv.com/design/hgtv-dream-home/sweepstakes";
        private string emailURL = "https://www.mail.com/#.7518-bluestripe_v4086232-element1-2";    
        private RemoteWebDriver driver;
        private string emailID = "royalJayTest@mail.com";
        private string emailPW = "royaljay";

        [TestCleanup]
        public void TestCleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }            
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        /// Verifies that user is able to succesfully signup for the HGTV dream home giveaway
        /// 1. navigate to hgtv dream home site
        /// 2. enter an email and click submit
        /// 3. enter form data and click enter 
        /// 4. verify correct message is displayed
        public void VerifyDreamHomeGiveAwaySignup()
        {
            driver = new FirefoxDriver();   // create Selenium driver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // how long driver should wait for an element to become present
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(HGTVGiveAwayURL);
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.Id("ngxFrame91461")));    // this is the id of the Iframe which contains the email textbox on the HGTV page
                driver.FindElementById("xReturningUserEmail").SendKeys(emailID);    // enter email address
                driver.FindElementById("xCheckUser").Click();   // click submit button
                
                // enter form data
                driver.FindElementById("name_Firstname").SendKeys("firstName");
                driver.FindElementById("name_Lastname").SendKeys("lastName");
                driver.FindElementById("address_Address1").SendKeys("123 State Street");
                driver.FindElementById("address_City").SendKeys("Boise");

                var stateDropdown = driver.FindElementByXPath("//div[contains(text(),'State')] ");
                // scroll to item (a better solution might be to scroll to every item (so that it works on all screen resolutions
                // and then do a check to see that it is in a ready and useable state before interacting with the element
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", stateDropdown);

                var genderRadioBtn = driver.FindElementById("gender_1");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", genderRadioBtn);

                driver.FindElementByXPath("//div[contains(text(),'State')]").Click();
                var idaho = driver.FindElementByXPath("//div[contains(text(),'Idaho')] ");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('class', 'option selected active')", idaho);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('data-value', 'ID')", stateDropdown);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerHTML = 'Idaho';", stateDropdown);

                var addressState = driver.FindElementById("address_State");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('value', 'ID')", addressState);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerHTML = 'Idaho';", addressState);
                
                driver.FindElementByXPath("//div[contains(text(),'Idaho')] ").Click();

                driver.FindElementById("address_Zip").SendKeys("83714");
                driver.FindElementById("phone").SendKeys("2081112222");
                driver.FindElementById("date_of_birth_day").SendKeys("12");
                driver.FindElementById("date_of_birth_year").SendKeys("1982");
                
                var monthDropdown = driver.FindElementByXPath("//div[contains(text(),'Month')] ");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('data-value', '03')", monthDropdown);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerHTML = 'March';", monthDropdown);                
                var cableProvider = driver.FindElementByXPath("//div[contains(text(),'Provider')] ");
                
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('data-value', 'Cable_ONE')", cableProvider);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].innerHTML = 'Cable ONE';", cableProvider);
                
                // scroll to button
                var enterButton = driver.FindElementByClassName("xSubmit");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", enterButton);

                enterButton.Click();
            }
            catch (NoSuchElementException e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsTrue(driver.PageSource.Contains("Thank You for Entering!"), "Success message was not found after submitting entry for dream home giveaway.");
        }

        [TestMethod]
        /// verifies user is able to access their mail.com account
        /// 1. navigate to mail.com site
        /// 2. enter credentials and login
        /// 3. verify user is logged in
        public void NavigateToEmailAccount()
        {            
            driver = new FirefoxDriver();   // create Selenium driver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);  // how long driver should wait for an element to become present
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(emailURL);
            try
            {                
                driver.FindElementById("login-button").Click();
                driver.FindElementById("login-email").SendKeys(emailID);
                driver.FindElementById("login-password").SendKeys(emailPW);
                driver.FindElementByClassName("login-submit").Click();
            }
            catch (NoSuchElementException e)
            {
                Assert.Fail(e.Message);
            }
            // confirm email account is logged into
            Assert.IsTrue(driver.PageSource.Contains("Log out"), "The user was not able to login.");
        }

        [TestMethod]
        /// Verifies that registering for the dream home giveaway reminders gives the correct message to the user
        /// 1. navigate to hgtv dream home site
        /// 2. enter an email
        /// 3. click sign up button
        /// 4. verify correct message is displayed
        public void DreamHomeGiveAwayReminderSignup()
        {             
            driver = new FirefoxDriver();   // create Selenium driver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20); // how long driver should wait for an element to become present
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(HGTVGiveAwayURL);
            try
            { 
                driver.FindElementByName("email").SendKeys(emailID);
                driver.FindElementByName("submit").Click();
            }
            catch(NoSuchElementException e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsTrue(driver.PageSource.Contains("You have successfully registered to receive email reminders to enter the giveaway."), "Success message was not found after clicking sign up button.");
        }

        [TestMethod]
        /// Verify that an email was recieved after the user signed up for reminders
        /// 1. Sign up for reminders through the hgtv site
        /// 2. Navigate to email account and confirm that reminder email is there (here we assume the spec is for them to send an email when the user signs up for reminders)
        public void EmailSentAfterDreamHomeReminderRegistration()
        {
            // Sign up for reminders through the hgtv site
            driver = new FirefoxDriver();   // create Selenium driver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);  // how long driver should wait for an element to become present
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(HGTVGiveAwayURL);
            try
            {
                driver.FindElementByName("email").SendKeys(emailID);
                driver.FindElementByName("submit").Click();
                Assert.IsTrue(driver.PageSource.Contains("You have successfully registered to receive email reminders to enter the giveaway."), "Success message was not found after clicking sign up button.");

                //  Navigate to email account and confirm that reminder email is there
                driver.Navigate().GoToUrl(emailURL);
                driver.FindElementById("login-button").Click();
                driver.FindElementById("login-email").SendKeys(emailID);
                driver.FindElementById("login-password").SendKeys(emailPW);
                driver.FindElementByClassName("login-submit").Click();
            }
            catch (NoSuchElementException e)
            {
                Assert.Fail(e.Message);
            }
            // hgtv doesn't send an email when the user signs up for reminders, so this test will fail
            Assert.IsTrue(driver.PageSource.Contains("We will be sending HGTV reminders to your email."), "The user did not recieve an email notifying them of email reminders from HGTV.");            
        }        
    }
}
