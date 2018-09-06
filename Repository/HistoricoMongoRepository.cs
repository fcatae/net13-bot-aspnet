using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot.Repository
{
    public class HistoricoMongoRepository : IHistoricoRepository
    {
        public void SalvarHistorico(Message message)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var doc = new BsonDocument
            {
                { "id", message.Id },
                { "texto", message.Text},
                { "app", "teste"}
            };

            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");
            col.InsertOne(doc);
        }
    }
}