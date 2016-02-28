using DataLayer;

using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Rabbit
{
    class RabbitStorageWriter : RabbitMQConnector,  ISharedStorageWriter, IDisposable
    {
        public event Action<string,bool?> PushedMessage;
        private log4net.ILog log;
   
        protected virtual void OnPushedMessage(string message)
        {
            this.log.Debug(message);
            if (this.PushedMessage != null)
                this.PushedMessage(message,null);
        
        
        }
            public RabbitStorageWriter():base()
            {
                log4net.Config.XmlConfigurator.Configure();
                this.log = log4net.LogManager.GetLogger(this.GetType());
           
            }
       
        public void WriteAll(List<GameDBO> games)
        {
                foreach (var item in games)
                {

                    WriteOne(item);
                }
            
        }

        public void WriteOne(GameDBO item)
        {
           

            string output = JsonConvert.SerializeObject(item);
            string message = output;
            WriteOneMessage(message);
        }

       

        protected virtual void WriteOneMessage( string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: this.Exchange,
                                 routingKey: this.QueueName ,
                                 basicProperties: null,
                                 body: body);
            
            OnPushedMessage(message);
            
        }






       
    }
}
