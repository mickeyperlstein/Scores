using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSportsScores.DB
{
    class GameComparer : IComparer<Game>
    {
        public int Compare(Game x, Game y)
        {
            if (x.SportType == y.SportType
                &&
                x.CompetitionName == y.CompetitionName
                &&
                x.LastUpdatedOn.Value.AddHours(2) < y.LastUpdatedOn
                ) return 0;
            else
                return 1;


        }
    }
}
