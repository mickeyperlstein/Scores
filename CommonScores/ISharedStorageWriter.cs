using DataLayer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public interface ISharedStorageWriter : IStorageConnector
    {
        void WriteAll(List<GameDBO> games);
        void WriteOne(GameDBO game);

        event Action<string,bool?> PushedMessage;
        
    }
}
