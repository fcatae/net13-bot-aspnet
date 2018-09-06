using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        
        public static string Reply(Message message)
        {
            var id = message.Id;
            var profile = GetProfile(id);
            profile.Visitas += 1;

            SetProfile(id, profile);

            //var client = new MongoClient("mongodb://localhost:27017");

            //var doc = new BsonDocument
            //{
            //    { "id", message.Id},
            //    { "texto", message.Text},
            //    { "app", "teste"}
            //};

            //var db = client.GetDatabase("db01");
            //var col = db.GetCollection<BsonDocument>("tabela01");

            //col.InsertOne(doc);

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");

            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            var res = col.Find(filtro).ToList();

            if (res.Count == 0)
            {
                return new UserProfile
                {
                    Id = id,
                    Visitas = 0
                };

            }
            else {
                //retornar o documento;
                return null;
            }
            
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("userProfile");

            var filtro = Builders<BsonDocument>.Filter.Gt("_id", 1);


        }
    }
}