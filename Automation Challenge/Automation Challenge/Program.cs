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

            // Select Long Term Surface Parking
            IWebElement ParkingLotSelector = driver.FindElement(By.Id("ParkingLot"));
            var SelectParkingLot = new SelectElement(ParkingLotSelector);
            SelectParkingLot.SelectByValue("Long-Surface");

            // Entry Date
            IWebElement EntryDate = driver.FindElement(By.Id("StartingDate"));
            EntryDate.Clear();
            EntryDate.SendKeys("4/5/20");

            // Entry Time
            IWebElement EntryTime = driver.FindElement(By.Id("StartingTime"));
            EntryTime.Clear();
            EntryTime.SendKeys("12:00");

            //Leaving Date
            IWebElement LeavingDate = driver.FindElement(By.Id("LeavingDate"));
            LeavingDate.Clear();
            LeavingDate.SendKeys("4/6/20");

            //Leaving Time
            IWebElement LeavingTime = driver.FindElement(By.Id("LeavingTime"));
            LeavingTime.Clear();
            LeavingTime.SendKeys("5:01");

            //Click Summit
            IWebElement ClickSummit = driver.FindElement(By.Name("Submit"));
            ClickSummit.Click();


            /*
             Assert
            $2.00 per hour
            $10.00 daily maximum
            $60.00 per week (7th day free)

            Every day at the time of departure just at 5:01 a.m. to 5:59 a.m. and 
            also from 6:01a.m. to 6:59 a.m. an additional $2 is charged, 
            when the maximum that can be charged per day is $10. 
            The bug is only for the am time frame and not for the pm. 
             
             */

            IWebElement ActualResults = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/span[1]/b"));
            var GivenResults = ActualResults.Text;
            var ExpectedResults = "$ 20.00";
            Assert.AreEqual(GivenResults, ExpectedResults);
           
        }


        [Test]
        public void InitializeDataEntry()
        {
            // I am testing if we can enter random inforamtion and see what happen
            IWebElement ParkingLotSelector = driver.FindElement(By.Id("ParkingLot"));
            var LotSelector = new SelectElement(ParkingLotSelector);
            LotSelector.SelectByValue("Valet");

            // Entry Date
            IWebElement EntryDate = driver.FindElement(By.Id("StartingDate"));
            EntryDate.Clear();
            EntryDate.SendKeys("adfsdfsdf");

            //Entry Time
            IWebElement EntryTime = driver.FindElement(By.Id("StartingTime"));
            EntryTime.Clear();
            EntryTime.SendKeys("adfsdfsdf");

            //Leaving Date
            IWebElement LeavingDate = driver.FindElement(By.Id("LeavingDate"));
            LeavingDate.Clear();
            LeavingDate.SendKeys("adfsdfsdf");

            //Leaving Time
            IWebElement LeavingTime = driver.FindElement(By.Id("LeavingTime"));
            LeavingTime.Clear();
            LeavingTime.SendKeys("adfsdfsdf");

            //Click Summit
            IWebElement ClickSummit = driver.FindElement(By.Name("Submit"));
            ClickSummit.Click();

            /*
             Assert

            Ramdon information should display and error message and not a value.
             */

            IWebElement ActualResults = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr[4]/td[2]/span[1]/b"));
            var GivenResults = ActualResults.Text;
            var ExpectedResults = "ERROR! Your Leaving Date Or Time Is Before Your Starting Date or Time";
            Assert.AreEqual(GivenResults, ExpectedResults);

        }
    }
}
