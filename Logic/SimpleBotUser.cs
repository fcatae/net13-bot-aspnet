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
        public static string Reply(Message message)
        {
            string returnedText = $"{message.User} disse '{message.Text}'";

            var doc = new BsonDocument
            {
                {"id",  message.Id},
                {"user", message.User },
                {"originalText", message.Text },
                {"returnedText", returnedText },
                {"messageDT", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }
            };

            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("SimpleBotDB");
            var col = db.GetCollection<BsonDocument>("messagesTable");

            col.InsertOne(doc);

            UserProfile userProfile = GetProfile(message.Id);
            SetProfile(userProfile.Id, userProfile);

            //return $"{message.User} disse '{message.Text}'";
            return returnedText;
        }

        public static UserProfile GetProfile(string id)
        {
            
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("SimpleBotDB");
            var col = db.GetCollection<BsonDocument>("UserProfile");

            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            var res = col.Find(filtro).ToList();

            UserProfile userProfile = new UserProfile();
            if (res.Count == 0)
            {
                userProfile.Id = id;
                userProfile.Visitas = 1;
            }
            else
            {
            }

            return userProfile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var doc = new BsonDocument
            {
                {"Id", profile.Id },
                {"Visitas", profile.Visitas }
            };

            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("SimpleBotDB");
            var col = db.GetCollection<BsonDocument>("UserProfile");

            if (profile.Visitas == 1)
            {
                col.InsertOne(doc);
            }
            else
            {
                col.ReplaceOne(doc, doc);
            }
        }
    }
}