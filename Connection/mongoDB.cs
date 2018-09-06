using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SimpleBot.Connection
{
    public class mongoDB
    {
        private static string mongoString = "mongodb://localhost";
        private static string mongoBase = "fiap-aula";
        private static string collectionDAL = "bot";
        private static string collectionPerfil = "perfil";


        public static bool postDocument(MessageBson objToSave)
        {
            try
            {
                bool ret = false;
                MongoClient client = new MongoClient(mongoString);
                BsonDocument document = BsonDocument.Parse(objToSave.ToJson());
                var db = client.GetDatabase(mongoBase);
                var collection = db.GetCollection<BsonDocument>(collectionDAL);
                collection.InsertOne(document);
                ret = true;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static UserProfile getDocument(string id)
        {
            try
            {
                MongoClient client = new MongoClient(mongoString);
                var db = client.GetDatabase(mongoBase);
                var collection = db.GetCollection<UserProfile>(collectionPerfil);
                var builder = Builders<UserProfile>.Filter;
                var filter = builder.Eq("Id", id);
                return collection.Find(filter).ToListAsync().Result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool putDocument(string id, UpdateDefinition<UserProfile> update)
        {
            try
            {
                bool ret = false;

                MongoClient client = new MongoClient(mongoString);
                var db = client.GetDatabase(mongoBase);
                var collection = db.GetCollection<UserProfile>(collectionPerfil);
                var builder = Builders<UserProfile>.Filter;
                var filter = builder.Eq("Id", id);
                collection.FindOneAndUpdate(filter, update);
                ret = true;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool postDocumentPerfil(UserProfile objToSave)
        {
            try
            {
                bool ret = false;
                MongoClient client = new MongoClient(mongoString);
                BsonDocument document = BsonDocument.Parse(objToSave.ToJson());
                var db = client.GetDatabase(mongoBase);
                var collection = db.GetCollection<BsonDocument>(collectionPerfil);
                collection.InsertOne(document);
                ret = true;

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}