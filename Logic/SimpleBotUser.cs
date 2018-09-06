using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft;
using Newtonsoft.Json;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var db = cliente.GetDatabase("13Net");

            var col = db.GetCollection<BsonDocument>("tabela01");

            var profile = GetProfile(message.Id);

            var doc = new BsonDocument()
            {
                { "Id", message.Id },
                { "Mensagem", message.Text },
                { "User", message.User },
                { "TipoRegistro", message.GetType().Name}

            };

            col.InsertOne(doc);
            
            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var db = cliente.GetDatabase("13Net");

            var col = db.GetCollection<BsonDocument>("tabela01");

            var doc = new BsonDocument()
            {
                { "Id", id}
                
            };

            var result = col.Find<BsonDocument>(doc).FirstOrDefault();


            List<UserProfile> profile = JsonConvert.DeserializeObject<List<UserProfile>>(result.ToString()).ToList();
            var qtdVisitas = col.Find<BsonDocument>(doc).ToList();

            UserProfile user = new UserProfile()
            {
                Id = profile.FirstOrDefault().Id,
                Visitas = profile == null ? 0 :Convert.ToInt32(profile.Count) + 1
            };

            return user;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            
        }
    }
}