using RabbitSportsScores.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitSportsScores
{
    class Converter
    {
        internal static Game GameDBO_To_EntityGame(ScoresPublisher.GameDBO dbo)
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
