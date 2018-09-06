using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {

        public static string Popula_UserProfile(Message message)
        {
            var id = message.Id;

            var prof = GetProfile(id);

            prof.Visitas += 1;

            SetProfile(id, prof);

            return $"{message.User} disse '{message.Text} e mandou {prof.Visitas} '";
        }
        
        public static string Reply(Message message)
        {

            Popula_UserProfile(message);

            var client = new MongoClient("mongodb://localhost:27017");


            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");
            
            var doc = new BsonDocument()
            {
                {"id",message.Id },
                {"texto", message.Text },
                {"app","teste2" }
                
            };         

            col.InsertOne(doc);
           
            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {        


            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<UserProfile>("Profile01");


            var filtro = Builders<UserProfile>.Filter.Eq("id",id);

            var profile = col.Find(filtro).FirstOrDefault(); 

            if (profile == null)
            {
                return new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };

            }


            return profile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<UserProfile>("Profile01");

            var filtro = Builders<UserProfile>.Filter.Eq("id", id);

            col.ReplaceOne(filtro, profile);

        }
    }
}