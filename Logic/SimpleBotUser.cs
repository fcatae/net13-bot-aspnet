using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        private static string MongoDBConectionString = ConfigurationManager.AppSettings["MDBConectionString"];
        public static string Reply(Message message)
        {
            SetProfile(message.Id, null);

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {

            return null;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var MongoCliente = new MongoClient();

            var db = MongoCliente.GetDatabase("dbBOT");
            var col = db.GetCollection<BsonDocument>("Users_Profiles");

            var user = new BsonDocument
            {
                { "Id", id },
                { "Visitas", 1 }
            };
            col.InsertOne(user);


            var filtro = Builders<BsonDocument>.Filter.Eq("Id", id);

            var res = col.Find(filtro).ToList();
        }
    }
}