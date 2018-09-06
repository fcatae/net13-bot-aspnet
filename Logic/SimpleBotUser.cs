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

            //return $"{message.User} disse '{message.Text}'";
            return returnedText;
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