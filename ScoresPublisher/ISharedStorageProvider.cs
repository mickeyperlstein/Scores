using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoresPublisher
{
    public interface ISharedStorageProvider
    {
        void PushAll(List<GameDBO> games);
    }
}
