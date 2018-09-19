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

            if (profile == null)
                return null;
            else
            {
                UserProfile userProfile = new UserProfile()
                {
                    Id = profile.Id,
                    Visitas = profile.Visitas,
                    HorarioRegistro = profile.HorarioRegistro
                };

                return userProfile;
            }
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var filter = Builders<UserProfileMongo>.Filter.Eq("Id", id);

            var doc = new UserProfileMongo
            {
                Id = profile.Id,
                Visitas = profile.Visitas,
                HorarioRegistro = profile.HorarioRegistro
            };

            _collection.ReplaceOne(filter, doc, new UpdateOptions { IsUpsert = true });
        }
    }
}