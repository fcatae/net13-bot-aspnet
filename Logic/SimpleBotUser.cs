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
            var client = new MongoClient("mongodb://localhost:27017");

            var doc = new BsonDocument()
            {
                { "id", message.Id},
                { "texto", message.Text},
                { "app", "TesteApp"}
            };

            var db = client.GetDatabase("dbNet13");
            var col = db.GetCollection<BsonDocument>("user");
            col.InsertOne(doc);

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            return null;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}