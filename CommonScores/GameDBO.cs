using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class GameDBO
    {   
        public int Id { get; set; }
        public DateTime Start { get; set; }

        public string CompetitionName { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public string Sport { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}
