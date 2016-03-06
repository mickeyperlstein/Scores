using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoresPublisher
{
    public class WebClientScraper : IScrapingProvider
    {
        public WebClientScraper()
        {
            
        }
        public void Scrape(string url)
        {
            var getHtmlWeb = new GZipWebClient();
            var html = getHtmlWeb.DownloadString(url);

            var htmlDocObj = new HtmlDocument();
            htmlDocObj.LoadHtml(html);
            string xpath = "//*[@id='form1']/div[3]/div[3]/div[3]/section/ul";
            xpath = "html/body/div";

            html = htmlDocObj.DocumentNode.InnerHtml;
        }

        public string html { get; set; }
    }
}
