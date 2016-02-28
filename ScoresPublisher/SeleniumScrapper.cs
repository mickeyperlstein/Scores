
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
            log4net.Config.XmlConfigurator.Configure();
            this.log = log4net.LogManager.GetLogger(this.GetType());

        }
        public void Scrape(string url)
        {
            using (var driver = new ChromeDriver())
            {
                log.DebugFormat("beginning navigation to {0}",url);
                driver.Navigate().GoToUrl(url);
                log.Debug("Navigation done, starting to read html and run js scripts");

                string xpath = "//*[@id='partidos']";
                var t = driver.FindElementByXPath(xpath);

                html = t.GetAttribute("innerHTML");
            }
            log.Debug("chrome handle destroyed");
        }

        public string html { get; set; }

        private log4net.ILog log;
    }
}
