using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class MessageRepository : IMessageRepository
    {
        private static string mongoBase = "fiap-aula";
        private static string collection = "bot";

        //MONGO COLLECTION
        private IMongoCollection<MessageBson> _collection;

        public MessageRepository(string stringConnection)
        {
            MongoClient client = new MongoClient(stringConnection);
            var db = client.GetDatabase(mongoBase);
            _collection = db.GetCollection<MessageBson>(collection);
        }

        public void postMessage(MessageBson message)
        {
            try
            {
                _collection.InsertOne(message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}