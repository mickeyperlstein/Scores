using DataLayer;
using RabbitSportsScores.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    class Converter
    {
        internal static Game GameDBO_To_EntityGame(GameDBO dbo)
        {
            var game = new Game
            {
                CompetitionName = dbo.CompetitionName,
                LastUpdatedOn = dbo.LastUpdatedOn,
                MatchStart = dbo.Start,
                SportType = dbo.Sport,
                Team1 = dbo.Team1,
                Team2 = dbo.Team2

            };
            return game;
        }
    }
}
