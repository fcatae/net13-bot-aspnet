using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SimpleBot.Repository
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {
        readonly IMongoCollection<UserProfileMongo> _profileCollection;

        public UserProfileMongoRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("bot");
            var col = db.GetCollection<UserProfileMongo>("profile");

            this._profileCollection = col;
        }

        public UserProfile GetProfile(string id)
        {
            var col = _profileCollection;

            var cursor = col.Find(p => p._id == id);

            var profile = cursor.FirstOrDefault();

            if (profile == null)
            {
                return new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };
            }

            return new UserProfile()
            {
                Id = profile._id,
                Visitas = profile.Visitas
            };
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var col = _profileCollection;
            var opts = new UpdateOptions { IsUpsert = true };

            var doc = new UserProfileMongo
            {
                _id = profile.Id,
                Visitas = profile.Visitas
            };

            col.ReplaceOne(p => p._id == id, doc, opts);
        }
    }
}