using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Automation_Challenge
{
    class Program
    {
        // Reference for Chrome browser
        IWebDriver driver = new ChromeDriver();

        [SetUp]
        public void Initialize()
        {
            // Got the website tested
            driver.Navigate().GoToUrl("http://www.shino.de/parkcalc/");
        }


        [Test]
        public void ExecuteTest()
        {
            //Make browser Full Screen
            driver.Manage().Window.Maximize();

            //Select Parking Lot
            IWebElement ParkingLotSelector = driver.FindElement(By.Id("ParkingLot"));
            var SelectParkingLot = new SelectElement(ParkingLotSelector);
            SelectParkingLot.SelectByValue("Valet");

            // Entry Date
            IWebElement EntryDate = driver.FindElement(By.Id("StartingDate"));
            EntryDate.Clear();
            EntryDate.SendKeys("4/5/20");

            // Entry Time
            IWebElement EntryTime = driver.FindElement(By.Id("StartingTime"));
            EntryTime.Clear();
            EntryTime.SendKeys("12:00");

            // Leaving Date
            IWebElement LeavingDate = driver.FindElement(By.Id("LeavingDate"));
            LeavingDate.Clear();
            LeavingDate.SendKeys("4/6/2020");

            //Leaving Time
            IWebElement LeavingTime = driver.FindElement(By.Id("LeavingTime"));
            LeavingTime.Clear();
            LeavingTime.SendKeys("4:00");

            //CLick Calcualte
            IWebElement ClickSummit = driver.FindElement(By.Name("Submit"));
            ClickSummit.Click();

            /* 
             Assert

            Valet Parking parameters
             $18 per day
             $12 for five hours or less
            
            We are looking for 1 day and 4 hours, the result should be $30. The result provided is $36.

             */
            IWebElement ActualResults = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/span[1]/b"));
            var GivenResults = ActualResults.Text;
            var ExpectedResults = "$ 30.00";
            Assert.AreEqual(GivenResults, ExpectedResults);

        }

        [Test]
        public void InitializeShortTerm()
        {

            // Select Short Term Parking
            IWebElement ParkingLotSelector = driver.FindElement(By.Id("ParkingLot"));
            var SelectParkingLot = new SelectElement(ParkingLotSelector);
            SelectParkingLot.SelectByValue("Short");

            //Entry Date
            IWebElement EntryDate = driver.FindElement(By.Id("StartingDate"));
            EntryDate.Clear();
            EntryDate.SendKeys("4/5/2020");

            //Entry Time
            IWebElement EntryTime = driver.FindElement(By.Id("StartingDate"));
            EntryTime.Clear();
            EntryTime.SendKeys("12:00");

            //Leaving Date
            IWebElement LeavingDate = driver.FindElement(By.Id("LeavingDate"));
            LeavingDate.Clear();
            LeavingDate.SendKeys("4:00");

            //Leaving Time
            IWebElement leavingTime = driver.FindElement(By.Id("LeavingTime"));
            leavingTime.Clear();
            leavingTime.SendKeys("4:00");

            /*
             Assert

            Short-Term (hourly) Parking
            $2.00 first hour; $1.00 each additional 1/2 hour
            $24.00 daily maximum

            We are looking for 1 day and 4 hours, the result should be $32. The result is the same. 
             
             */

            IWebElement ActualResults = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/span[1]/b"));
            var GivenResults = ActualResults.Text;
            var ExpectedResults = "$ 32.00";
            Assert.AreEqual(ExpectedResults, ActualResults);
           


        }
    }
}
