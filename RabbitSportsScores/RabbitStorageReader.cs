using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitSportsScores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Rabbit
{
    class RabbitStorageReader : RabbitMQConnector, ISharedStorageReader
    {
        private EventingBasicConsumer consumer;
        public event Action<string> RecievedMessage;
       
        protected virtual void OnRecievedMessage(string json)
        {
 	        if (this.RecievedMessage!=null)
                this.RecievedMessage(json);
        }

                
        public void StartRecieve()
        {

            this.consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var json = Encoding.UTF8.GetString(body);

                OnRecievedMessage(json);

            };
        }
    }
}
