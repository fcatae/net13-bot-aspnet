using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using SimpleBot.Models;
using Newtonsoft.Json;
using SimpleBot.Interfaces;

namespace SimpleBot.Repository
{
    public class UserProfileMongoRepository 
    {
        // Esse codigo abaixo nao funciona - falta adicionar o tipo correto.
        var col;
        
        public UserProfileMongoRepository(string connectionString)
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var db = cliente.GetDatabase("13Net");
            
            // Define em uma propriedade
            this.col = db.GetCollection<BsonDocument>("tabela01");
        }
        
        public string Reply(MessageModel message)
        {
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

        public UserProfileModel GetProfile(string id)
        {
            var doc = new BsonDocument()
            {
                { "Id", id}

            };

            var filtro = Builders<BsonDocument>.Filter.Gt("id", id);

            var res = col.Find(filtro).ToList();

            var qtdVisitas = col.Find<BsonDocument>(doc).ToList();

            UserProfileModel user = new UserProfileModel() { };

            return user;
        }

        public void SetProfile(string id, UserProfileModel profile)
        {
            // Nao foi implementado???
        }
    }
}
