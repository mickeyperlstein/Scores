using CommonScores;
using log4net.Config;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitSportsScores.DB;
using Storage;
using Storage.Entity;
using Storage.Rabbit;
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

            EntityStorageWriter entityWriter = new EntityStorageWriter();
            entityWriter.Connect();
            entityWriter.PushedMessage += (msg, saved) =>
            {
                Console.WriteLine(" [x] Received {0}", msg);
             
                if (saved.HasValue && saved.Value==false)
                    Console.WriteLine("DISCARDED DATA");
            
            };

            ISharedStorageReader storageReader = new RabbitStorageReader();
            storageReader.Connect();

            storageReader.RecievedMessage += (json) =>
            {
                entityWriter.WriteOne(json);
            };
          
            

           
            }

        }

       
   

       
    
}
