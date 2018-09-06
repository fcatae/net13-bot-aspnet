using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SimpleBot
{
    public class SimpleBotUser
    {         

        public static string Reply(Message message)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("test");            
            var col = db.GetCollection<BsonDocument>("messages");
            
            message.UserId = "UsuarioPadraoID";
            var profile = GetProfile(message.UserId);

            message.Text = BuildRealLifeAnalytics(message.Text);          
            
            col.InsertOne(message.ToBsonDocument());
           
            return $"Visits: {profile.Visitas}\n{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("profiles");

            var filter = Builders<BsonDocument>.Filter.Eq("UserID", id);            
            var res = col.Find(filter).First();

            var profile = new UserProfile()
            {
                Id = res["Id"].ToString(),
                Name = res["Name"].ToString(),
                Visitas = res["Visitas"].ToInt32()
            };

            return profile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("profiles");
            
            profile.Visitas++;
            

            message.Text = BuildRealLifeAnalytics(message.Text);

            col.InsertOne(message.ToBsonDocument());
        }

        private static string BuildRealLifeAnalytics(string message)
        {
            if (message.ToLower().Contains("igor"))
                message = "Igor?\n fala pra krl, quero saber disso n.";
            if (message.ToLower().Contains("renato"))
                message = "Sim.\n mó viadão esquisito da porra.";
            if (message.ToLower().Contains("jonas"))
                message = "Jonas brothers?";
            if (message.ToLower().Contains("harry"))
                message = "Harry? você quis dizer Bieber?";
            if (message.ToLower().Contains("kenzo"))
                message = "Ja sabe né, Kenzo é tão macho quanto o harry.";


            return message;
        }
    }
}