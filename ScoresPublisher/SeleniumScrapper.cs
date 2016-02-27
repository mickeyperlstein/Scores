using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoresPublisher
{
    public class SeleniumScrapper : IScrapingProvider
    {
        public SeleniumScrapper()
        {
           
        }
        public void Scrape(string url)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                string xpath = "//*[@id='partidos']";
                var t = driver.FindElementByXPath(xpath);

                html = t.GetAttribute("innerHTML");
            }
        }

        public string html { get; set; }
    }
}
