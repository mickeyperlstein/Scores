using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoresPublisher
{
    class RabbitMQProvider : ISharedStorageProvider
    {
        public event Action<string,IModel> PushedMessage;
        public virtual void OnPushedMessage(string message, IModel channel)
        {

            if (this.PushedMessage != null)
                this.PushedMessage(message, channel);
        
        
        }
            public RabbitMQProvider()
            {
            this.QueueName = "hello";
            this.HostName = "localhost";
        }
        public  string QueueName { get; private set; }
        public  string HostName { get; private set; }
        public void PushAll(List<GameDBO> games)
        {
            var factory = new ConnectionFactory() { HostName = this.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                DeclareQueu( channel);


                foreach (var item in games)
                {

                    string output = JsonConvert.SerializeObject(item);
                    string message = output;
                    pushSingleMessage(channel, message);
                }
            }
        }

        private  void DeclareQueu( IModel channel)
        {
            channel.QueueDeclare(queue: this.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


        }

        private  void pushSingleMessage(IModel channel, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: this.QueueName ,
                                 basicProperties: null,
                                 body: body);
            
            OnPushedMessage(message, channel);
            
        } 
    }
}
