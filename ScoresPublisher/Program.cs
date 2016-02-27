using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoresPublisher
{
    class Program
    {
        static void Main(string[] args)
        {

            var sc = new Scraper(new SeleniumScrapper());
            sc.Scrape("http://futbolme.com/");
            var list = sc.Games;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string queueName = "hello";
                DeclareQueu(queueName, channel);


                foreach (var item in list)
                {

                    string output = JsonConvert.SerializeObject(item);
                    string message = output;
                    pushMessage(channel, message, queueName);
                }
            }


        }

        private static void DeclareQueu(string queueName, IModel channel)
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

           
        }

        private static void pushMessage(IModel channel, string message, string queueName)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }  
    }
}
