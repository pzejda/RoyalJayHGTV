using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Interactions;
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
            driver.Quit();
        }

        [TestInitialize]
        public void TestInitialize()
        {

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
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(HGTVGiveAwayURL);
            driver.FindElementByName("email").SendKeys(emailID);
            driver.FindElementByName("submit").Click();
            Assert.IsTrue(driver.PageSource.Contains("You have successfully registered to receive email reminders to enter the giveaway."), "Success message was not found after clicking sign up button.");
        }

        [TestMethod]
        /// Verify that an email was recieved after the user signed up for reminders
        /// 1. Sign up for reminders through the hgtv site
        /// 2. Navigate to email account and confirm that reminder email is there (here we assume the spec is for them to send an email when the user signs up for reminders)
        public void VerifyDreamHomeReminderRegistrationViaEmail()
        {
            // Sign up for reminders through the hgtv site
            driver = new FirefoxDriver();   // create Selenium driver
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(HGTVGiveAwayURL);
            driver.FindElementByName("email").SendKeys(emailID);
            driver.FindElementByName("submit").Click();
            Assert.IsTrue(driver.PageSource.Contains("You have successfully registered to receive email reminders to enter the giveaway."), "Success message was not found after clicking sign up button.");

            //  Navigate to email account and confirm that reminder email is there
            driver.Navigate().GoToUrl(emailURL);
            driver.FindElementById("login-button").Click();
            driver.FindElementById("login-email").SendKeys(emailID);
            driver.FindElementById("login-password").SendKeys(emailPW);
            driver.FindElementByClassName("login-submit").Click();
            // hgtv doesn't send an email when the user signs up for reminders, so this test will fail
            Assert.IsTrue(driver.PageSource.Contains("We will be sending HGTV reminders to your email."), "The user did not recieve an email notifying them of email reminders from HGTV.");            
        }        
    }
}
