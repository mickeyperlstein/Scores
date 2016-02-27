using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitSportsScores.DB
{
     public partial class ScoresDBEntities
    {

         public bool IsGamePersisted( Game y)
         {

             return this.Games.Where(x =>
                   x.SportType == y.SportType
                   &&
                   x.CompetitionName == y.CompetitionName
                   &&
                   x.LastUpdatedOn.Value.AddHours(2) < y.LastUpdatedOn)
                   .Any();


         }
    }
}
