using MongoDB.Driver;
using System.Linq;

namespace SimpleBot.Logic
{
    public class UserProfileMongoRepo : IUserProfileRepository
    {
        private IMongoCollection<UserProfileMongo> _collection;
        private readonly string _dbname = "SimpleBotDB";
        private readonly string _collectionName = "UserProfile";

        public UserProfileMongoRepo(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(_dbname);
            var collection = db.GetCollection<UserProfileMongo>(_collectionName);

            this._collection = collection;
        }

        public UserProfile GetProfile(string id)
        {
            var filter = Builders<UserProfileMongo>.Filter.Eq("Id", id);

            var cursor = _collection.Find(filter);

            var profile = cursor.FirstOrDefault();

            //return new UserProfile
            //{
            //    Id = profile._id,
            //    Visitas = profile.Visitas
            //};

            if (profile == null)
                return null;
            else
            {
                UserProfile userProfile = new UserProfile()
                {
                    Id = profile.Id,
                    Visitas = profile.Visitas
                };

                return userProfile;
            }

            //var filtro = Builders<BsonDocument>.Filter.Eq("Id", id);
            //var res = col.Find(filtro).ToList();

            //UserProfile userProfile = new UserProfile();
            //if (res.Count == 0)
            //{
            //    userProfile.Id = id;
            //    userProfile.Visitas = 1;
            //}
            //else if (res.Count == 1)
            //{
            //    foreach (var e in res)
            //    {
            //        var s = e.Values.ToList();
            //        userProfile.Id = e[1].ToString();
            //        userProfile.Visitas = Int32.Parse(e[2].ToString());
            //    }

            //}
            //else
            //{ }

            //return userProfile;

        }

        public void SetProfile(string id, UserProfile profile)
        {
            var filter = Builders<UserProfileMongo>.Filter.Eq("Id", id);

            var doc = new UserProfileMongo
            {
                Id = profile.Id,
                Visitas = profile.Visitas
            };

            _collection.ReplaceOne(filter, doc, new UpdateOptions { IsUpsert = true });
        }
    }
}