using MongoDB.Driver;
using SimpleBot.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {

        private readonly MongoClient client;

        public UserProfileMongoRepository(string connectionString)
        {
            client = new MongoClient(connectionString);
        }

        public UserProfile GetProfile(string id)
        {
            var db = client.GetDatabase("User");
            var col = db.GetCollection<UserProfile>("UserProfile");
            var filtro = Builders<UserProfile>.Filter.Eq("_id", id);
            return col.Find(filtro).FirstOrDefault();
        }

        public void SetProfile(string id, UserProfile profile)
        {
            IMongoDatabase db = client.GetDatabase("User");
            IMongoCollection<UserProfile> col = db.GetCollection<UserProfile>("UserProfile");
            FilterDefinition<UserProfile> filtro = Builders<UserProfile>.Filter.Eq("_id", id);
            col.ReplaceOne(filtro, profile, new UpdateOptions() { IsUpsert = true });
          
        }
    }
}