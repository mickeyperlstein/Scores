using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Rabbit
{
    public abstract class RabbitMQConnector : IDisposable, IStorageConnector
    {
        private ConnectionFactory factory;
        protected IConnection connection;
        protected IModel channel;
        public string Exchange { get; protected  set; }
        public string QueueName { get; protected set; }
        public string HostName { get; protected set; }
        public RabbitMQConnector()
        {
            this.QueueName = "hello";
            this.HostName = "localhost";
            this.Exchange = "";
        }
        protected void DeclareQueu()
        {
            channel.QueueDeclare(queue: this.QueueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


        }



        public virtual void Dispose()
        {
            if (connection != null)
                connection.Dispose();

            if (channel != null)
                channel.Dispose();


        }

        public virtual void Connect()
        {
            if (IsConnected)
                return;

            this.factory = new ConnectionFactory() { HostName = this.HostName };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            DeclareQueu();
        }


        public bool IsConnected
        {
            get
            {
                {
                    return (connection != null && connection.IsOpen);
                }
            }
        }




    }
}
