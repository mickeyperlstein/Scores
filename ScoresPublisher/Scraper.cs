using DataLayer;
using log4net.Config;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScoresPublisher
{
    public class Scraper
    {
        private IScrapingProvider scProvider;
        protected  log4net.ILog log;
        public Scraper(IScrapingProvider provider)
        {
            this.Games = new List<GameDBO>();

            this.scProvider = provider;

            XmlConfigurator.Configure();
            this.log = log4net.LogManager.GetLogger(this.GetType());


        }
        public List<GameDBO> Games { get; set; }

        public void Scrape(string url)
        {
            log.Debug("starting html read");
            scProvider.Scrape(url);
            string html = scProvider.html;
            log.Debug("html complete");
           


            LocateRows(html);
        }

        public virtual void LocateRows(string html)
        {
            //var match = Regex.Match(html, @">(\s+?)<");
            //if (match.Groups.Count > 1)
            //{
            //    var v = match.Groups[1].Value;
            //}

            var opt1 = html
                .Split(new[] {"tname"}, StringSplitOptions.RemoveEmptyEntries)
                .Where(x=>!x.StartsWith(" hidden-xs"))
                .Select(x=> x.Replace(" visible-xs",""))
                .Select(i => i.Replace("\t", "")
                           .Replace("\n", "")
                            .Replace("\r", "")
                            .Replace('\"','\'')
                    )     
                .Distinct()
                
                .ToList();
            

                         var t = opt1
                   .Select(x => x
                        .Split(new[] { 
                            "ntreduce", 
                            "hora", 
                                //"timeresult",
                            "background-color:white"}, StringSplitOptions.RemoveEmptyEntries)
                   )
                   

                   .Select(e=>
                                          
                            e
                            .Select(z =>  ExtractText(z.Trim()))
                            .Where(u=> u.Length>1)
                            

                        .ToList()
                   )
                   .ToList();
               var gg = t
                   .Where(i=> i.Count > 1)
                   
                   .Select(xx=> ParseGameData(xx))
                   .ToList();

               this.Games = gg.SelectMany(x => x).ToList();

           
        }

        private List<GameDBO> ParseGameData(List<string> partiallyParsed)
        {

            string competition = partiallyParsed[0];
            int current = 1;

            List<GameDBO> listOut = new List<GameDBO>();
            

            while (current < partiallyParsed.Count)
            {

                string time_text = partiallyParsed[current++];
                    //string minuto_jornada = partiallyParsed[current++];
                    //if (!minuto_jornada.Contains("Minuto")
                    //   && !minuto_jornada.Contains("Descanso")
                    //    && !minuto_jornada.Contains("Jornada")  )

                    //{
                    //    current--;
                    //}
                string team1 = partiallyParsed[current++];
                current++;
                string team2 = partiallyParsed[current++];
                current++;
                DateTime date = DateTime.Today;
                if (DateTime.TryParse(time_text, out date))
                {

                var retGame = new GameDBO
                {
                    Start = date,
                    CompetitionName = competition,
                    Team1 = team1,
                    Team2 = team2,
                    Sport = "soccer",
                    LastUpdatedOn = DateTime.UtcNow
                };
                    listOut.Add(retGame);
                   
                } else
                     LogError("date parse failed for time: " + time_text );
            }
            
            
            
            return listOut;
        }

        private void LogError(string p)
        {
            log.Error(p);
        }

        public static string ExtractText(string html)
        {
            //var match = Regex.Match(html, @">(\s+?)<");
            //if (match.Groups.Count > 1)
            //{
            //    var v = match.Groups[1].Value;
            //}
            int start = html.IndexOf(">");
            int end = html.IndexOf("<", start + 1);
            string cleantext = html.Substring(start + 1, end - start - 1);
            return cleantext;
        }
    }
}