using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize webdriver and start firefox,
            //open Sensorberg login page, and log in
            //IWebDriver driver = new FirefoxDriver();
            // if FF is not supported for selenium webdriver, use chromedriver,
            // there need to be specified path to the chromewebdriver

            IWebDriver driver;
            try
            {
                driver = new FirefoxDriver();
            }
            finally
            {
                driver = new ChromeDriver(@"c:\selenium\web_automation");
            }


            driver.Url = "https://manage.sensorberg.com/#/signin";

            //enter login information
            //
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            var name = driver.FindElement(By.Name("email"));
            name.Clear();
            name.SendKeys("cingiz27@seznam.cz");

            var password = driver.FindElement(By.Name("password"));
            password.Clear();
            password.SendKeys("Password1");
            password.SendKeys(Keys.Return);

            //-----add application
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            //get to the page with Applications and add new entry
            driver.FindElement(By.XPath("//a[@href='#/applications']")).Click();
            driver.FindElement(By.XPath("//a[@href='#/applications/add']")).Click();

            var appName = driver.FindElement(By.Name("name"));
            appName.Clear();
            appName.SendKeys("testapp1");

            var description = driver.FindElement(By.Name("description"));
            description.Clear();
            description.SendKeys("add text with description");

            var radio = driver.FindElements(By.CssSelector(".platform-option"));
            radio[2].Click();

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            System.Threading.Thread.Sleep(2000);


            //
            // get to the page with Beacons and add new beacon
            //
            driver.FindElement(By.XPath("//a[@href='#/beacon/list']")).Click();
            driver.FindElement(By.XPath("//a[@href='#/beacon/add']")).Click();

            appName = driver.FindElement(By.Name("name"));
            appName.Clear();
            appName.SendKeys("testBeacon1");


            description = driver.FindElement(By.Name("description"));
            description.Clear();
            description.SendKeys("add text with description  ");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Element  m = new Element() ;

            //delete one application 
            m.delete_element(driver, "#/applications");
            
            //delete beacon
            m.delete_element(driver, "#/beacon/list");

            driver.Close();

        }


        class Element
        {

            /// <summary>
            /// Method for deleting one random line from the App list or Beacon list 
            /// </summary>
            /// <param name="driver"></param>
            /// <param name="element">xpath to the element whitch should be deleted from the main app (beacon/application)</param>

           public  void delete_element(IWebDriver driver, string element)
            {
                Random rnd = new Random();
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(50));
                System.Threading.Thread.Sleep(2000);

                driver.FindElement(By.XPath("//a[@href='" + element + "']")).Click();

                var lines = driver.FindElements(By.CssSelector(".column.column-select"));
                lines[rnd.Next(1, lines.Count())].Click();

                var deleteBeacon = driver.FindElement(By.CssSelector(".btn.btn-danger"));
                deleteBeacon.Click();
                driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            }
        }

    }
}



