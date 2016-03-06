using System;
using NUnit.Framework;
using ScoresPublisher;

namespace ScrapingSendingTester
{
    [TestFixture]
    public class HtmlScraping
    {
        [Test]
        public void TestLiveScraping()
        {
            var sc = new Scraper(new SeleniumScrapper());
            sc.Scrape("http://www.livesport.co.uk/");
            var list = sc.Games;

        }


        [Test]
        public void TestTextExtrqactor ()
        {
            string html = "<a href='fff'>byebye</babye>";
            string cleantext = Scraper.ExtractText(html);

            Assert.IsTrue("byebye" == cleantext);
       }

       
    }
}
