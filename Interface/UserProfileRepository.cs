using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private static string mongoBase = "fiap-aula";
        private static string collectionPerfil = "perfil";

        //MONGO COLLECTION
        private IMongoCollection<UserProfile> _collection;

        public UserProfileRepository(string stringConnection)
        {
            MongoClient client = new MongoClient(stringConnection);
            var db = client.GetDatabase(mongoBase);
            _collection = db.GetCollection<UserProfile>(collectionPerfil);
        }


        public UserProfile GetProfile(string id)
        {
            try
            {
                //GET
                var builder = Builders<UserProfile>.Filter;
                var filter = builder.Eq("Id", id);
                var userProfile = _collection.Find(filter).ToListAsync().Result.FirstOrDefault();
                return userProfile;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserProfile SetProfile(string id, UserProfile profile)
        {
            try
            {
                if (profile == null)
                {
                    //CREATE
                    profile = new UserProfile { Id = id, Visitas = 1 };
                    _collection.InsertOne(profile);
                }
                else
                {
                    //UPDATE
                    profile.Visitas = profile.Visitas + 1;
                    UpdateDefinition<UserProfile> update = Builders<UserProfile>.Update.Set("Visitas", profile.Visitas);
                    var builder = Builders<UserProfile>.Filter;
                    var filter = builder.Eq("Id", profile.Id);
                    _collection.FindOneAndUpdate(filter, update);
                }

                return profile;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}