﻿using NUnit.Framework;
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

        }
    }
}
