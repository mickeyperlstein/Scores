using CommonScores;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitSportsScores.DB;
using ScoresPublisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSportsScores
{
    class Program
    {
        static void Main(string[] args)
        {


            ConsoleUtils.Title("Listener and persister");

            // Recieve rec = new Recieve("localhost","hello");

            using (var db = new ScoresDBEntities())
            {
                //var game =
                //    new Game
                //{
                //    CompetitionName = "name1",
                //    MatchStart = DateTime.Now,

                //    LastUpdatedOn = DateTime.UtcNow
                //    ,
                //    Team1 = "team1",
                //    Team2 = "team2",
                //    SportType = "soccer"
                  
                //};

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    string queueName = "hello";
                    DeclareQueu(queueName, channel);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var json = Encoding.UTF8.GetString(body);


                        var dbo = JsonConvert.DeserializeObject<GameDBO>(json);
                        
                        var g = Converter.GameDBO_To_EntityGame(dbo);
                        
                        if (!db.IsGamePersisted(g))
                        {
                                                 
                            db.Games.Add(g);
                            
                            Console.WriteLine(" [x] Received {0}", json);
                            db.SaveChanges();
                            Console.WriteLine("New Id = {0}", g.Id);
                        }

                        
                        
                        
                    };

                    channel.BasicConsume(queue: queueName,
                                         noAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();


                }

               
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
    }
}
