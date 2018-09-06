using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        private readonly IMongoDatabase _db;
        private readonly MongoClient _client;
        public SimpleBotUser()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("dbNet13");
        }
        public static string Reply(Message message)
        {
            var id = message.Id;
            var profile = GetProfile(id);
            if(profile==null)
                SetProfile(id, profile);

            profile.Visitas += 1;
            SetProfile(id, profile);


            //var client = new MongoClient("mongodb://localhost:27017");

            //var doc = new BsonDocument()
            //{
            //    { "id", message.Id},
            //    { "texto", message.Text},
            //    { "app", "TesteApp"}
            //};

            //var db = client.GetDatabase("dbNet13");
            //var col = db.GetCollection<BsonDocument>("user");
            //col.InsertOne(doc);
            
            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("dbNet13");


            var filtro = Builders<UserProfile>.Filter.Eq("Id", id);
            var col = db.GetCollection<UserProfile>("UserProfile");
            var res = col.Find(filtro).ToList().FirstOrDefault();

            return res;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var doc = new BsonDocument();
            try
            {
                doc = new BsonDocument()
            {
                { "UserProfile", new BsonDocument
                    {
                        { "Id", id },
                        { "Visitas", 1 }
                    }
                }
            };
            }catch(Exception erro)
            {
                string msg = erro.Message;
            }


            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("dbNet13");
            var col = db.GetCollection<BsonDocument>("UserProfile");
            col.InsertOne(doc);
        }
    }
}