using DataLayer;
using log4net.Config;
using Newtonsoft.Json;
using RabbitSportsScores.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage.Entity
{
    class EntityStorageWriter : ISharedStorageWriter, IDisposable
    {
        public EntityStorageWriter()
        {
            XmlConfigurator.Configure();
            this.log = log4net.LogManager.GetLogger(this.GetType());
            
        }
        public virtual void OnPushedMessage(string message, bool? lastpushPersisted)
        {
            if (this.PushedMessage!=null)
                this.PushedMessage(message,lastpushPersisted);
        }
        public void WriteAll(List<GameDBO> games)
        {
            foreach (var item in games)
            {
                WriteOne(item);
            }
        }

        public event Action<string,bool?> PushedMessage;
        private ScoresDBEntities db;

         public void WriteOne(string  jsonDbo)
        {
var dbo = JsonConvert.DeserializeObject<GameDBO>(jsonDbo);
WriteOne(dbo);
        }
        public void WriteOne(GameDBO dbo, string json)
         {
             var entityGame = Converter.GameDBO_To_EntityGame(dbo);
             this.LastPushPersisted = true;

             if (!IsGamePersisted(entityGame))
             {

                 db.Games.Add(entityGame);
                 this.LastPushPersisted = true;
                 log.DebugFormat(" [x] Received {0}", json);
                 db.SaveChanges();
                 log.DebugFormat("New Id = {0}", entityGame.Id);
             }
             else
             {
                 log.DebugFormat("***old news\n{0}", json);
                 this.LastPushPersisted = false;
             }
            

             OnPushedMessage(json, LastPushPersisted);
         }
        public void WriteOne(GameDBO dbo)
        {
            var json = JsonConvert.SerializeObject(dbo);
            WriteOne(dbo, json);
            
        }


        public void Connect()
        {
            this.db = new ScoresDBEntities();
           
        }


        
        private log4net.ILog log;

        protected bool IsGamePersisted(Game y)
        {
            var time = y.LastUpdatedOn.Value.AddHours(-2);
            if (log.IsDebugEnabled)
            {
                var existingMatches = db.Games.Where(x =>
                  x.SportType == y.SportType
                  &&
                  x.CompetitionName == y.CompetitionName
                  &&
                  x.LastUpdatedOn.HasValue &&
                  x.LastUpdatedOn.Value > time)
                  .ToList();

                //log all here
                return existingMatches.Any();
            }
            else
                return db.Games.Where(x =>
                      x.SportType == y.SportType
                      &&
                      x.CompetitionName == y.CompetitionName
                      &&
                      x.LastUpdatedOn.HasValue &&
                      x.LastUpdatedOn.Value > time)
                      .Any();


        }
        public bool IsConnected
        {
            get
            {
                return (this.db != null &&
                    (this.db.Database.Connection.State ==
                    (System.Data.ConnectionState.Executing |
                    System.Data.ConnectionState.Connecting |
                    System.Data.ConnectionState.Fetching |
                    System.Data.ConnectionState.Open)));

            }
        }

        public void Dispose()
        {
            if (this.db != null)
                this.db.Dispose();
        }

        public bool LastPushPersisted { get; set; }
    }
}
