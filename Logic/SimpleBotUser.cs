using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            //MongoClient client = new MongoClient("mongodb://localhost:27017");
            //var db = client.GetDatabase("User");
            //var col = db.GetCollection<BsonDocument>("Messages");

            //var ms = new BsonDocument {
            //    { "id", message.Id},
            //    { "mensagem", message.Text},
            //    { "app","conn"}
            //};
            //col.InsertOne(new Pessoa()
            //{
            //    FirstName = "Dona",
            //    LastName = "Maria",
            //    Idade = 50,
            //    Email = "donamaria@outlook.com.br"
            //});
            //col.InsertOne(ms);
            var ret = GetProfile(message.Id);
            int total = ret == null ? 0 : ret.Visitas;
            SetProfile(message.Id, new UserProfile() { Id = message.Id, Visitas = ++total });

            ret = GetProfile(message.Id);
            return $"{message.User} disse '{message.Text}', total de Visitas: '{ret.Visitas}'";
        }

        public static UserProfile GetProfile(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("User");
            var col = db.GetCollection<UserProfile>("UserProfile");
            var filtro = Builders<UserProfile>.Filter.Eq("_id", id);
            return col.Find(filtro).FirstOrDefault();

        }

        public static void SetProfile(string id, UserProfile profile)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("User");
            IMongoCollection<UserProfile> col = db.GetCollection<UserProfile>("UserProfile");
            FilterDefinition<UserProfile> filtro = Builders<UserProfile>.Filter.Eq("_id", id);
            col.ReplaceOne(filtro, profile, new UpdateOptions() { IsUpsert = true });

        }
    }
}