using CommonScores;
using Newtonsoft.Json;
using RabbitMQ.Client;
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

            while (true)
            {

                var sc = new Scraper(new SeleniumScrapper());
                sc.Scrape("http://futbolme.com/");
                var list = sc.Games;
                var storage = new RabbitMQProvider();
                storage.PushedMessage += storage_PushedMessage;
                storage.PushAll(list);
                Console.WriteLine("Sleeping for 3 second");
                Thread.Sleep(3000);
            }

        }

        static void storage_PushedMessage(string arg1, IModel arg2)
        {
            Console.WriteLine("Sent : " + arg1);
        }

     

      

 
    }
}
