using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoresPublisher
{
    public interface IScrapingProvider
    {
         void Scrape(string url);
        string html { get; set; }
    }
}
