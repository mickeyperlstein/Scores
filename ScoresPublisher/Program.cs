using CommonScores;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Storage.Rabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScoresPublisher
{
    class Program
    {
        static void Main(string[] args)
        {


            ConsoleUtils.Title("Games Scraper and PERSISTER");
            Console.WriteLine("Scraping initialized"); 
            var sc = new Scraper(new SeleniumScrapper());
            while (true)
            {

                Console.WriteLine("Scraping start");
                sc.Scrape("http://futbolme.com/");
                Console.WriteLine("Done Scraping");
                var list = sc.Games;
                var storage = new RabbitStorageWriter();
                storage.Connect();
                storage.PushedMessage += storage_PushedMessage;
                storage.WriteAll(list);
                Console.WriteLine("Sleeping for 15 min");
                Thread.Sleep(1000 * 60 * 15);
            }

        }

        static void storage_PushedMessage(string arg1, bool? saved)
        {
            Console.WriteLine("Sent : " + arg1);
        }

     

      

 
    }
}
