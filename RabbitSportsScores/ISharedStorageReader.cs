using RabbitMQ.Client;
using RabbitSportsScores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public interface ISharedStorageReader : IStorageConnector
    {
        event Action<string> RecievedMessage;
        void StartRecieve();
    }
}
