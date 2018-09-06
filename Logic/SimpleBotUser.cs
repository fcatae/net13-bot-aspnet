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

            UserProfile profile = new UserProfile(); 
            try
            {

                profile = GetProfile(message.UserId);
            }
            catch (Exception ex)
            {
                var a = 1;
            }

            try
            {
                if (profile != null && profile.Id != null) profile.Visitas++;
                SetProfile(message.UserId, profile);
            }
            catch (Exception ex)
            {
                var a = 1;
            }

            try
            {
                message.Text = BuildRealLifeAnalytics(message.Text);
                col.InsertOne(message.ToBsonDocument());
            }
            catch (Exception ex)
            {
                var a = 1;
            }




            return $"Visits: {profile.Visitas}\n{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {            
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("test");
            var col = db.GetCollection<UserProfile>("profiles");

            var filter = Builders<UserProfile>.Filter.Eq("ProfileId", id);            
            var profile = col.Find(filter).First();
           
            return profile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var db = client.GetDatabase("test");
            var col = db.GetCollection<UserProfile>("profiles");

            if (profile == null)
                col.InsertOne(new UserProfile() { Id = "UsuarioPadraoID", Name = "Meu nome", Visitas = 1 });
            else
            {
                var filter = Builders<UserProfile>.Filter.Eq("Id", id);
                col.ReplaceOne(filter, profile);
            }
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