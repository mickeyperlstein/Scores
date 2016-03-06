using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public interface  IStorageConnector
    {
        void Connect();
        bool IsConnected {get;}
    }
}
