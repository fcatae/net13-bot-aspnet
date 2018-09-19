using System;
using SimpleBot.Interface;
using MongoDB.Driver;
using System.Configuration;

namespace SimpleBot.Logic
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {
        private readonly IMongoDatabase _db;

        public UserProfileMongoRepository()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["ConnectionStringMongo"]);
            _db = client.GetDatabase("dbNet13");
        }

        public UserProfile GetProfile(string id)
        {
            UserProfile usuarioEncontrado = null;
            try
            {
                usuarioEncontrado = _db.GetCollection<UserProfile>("UserProfile")
                    .Find(u => u.Id == id).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return usuarioEncontrado;
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var col = _db.GetCollection<UserProfile>("UserProfile");

            if (profile == null)
            {
                profile = new UserProfile{Id = id, Visitas = 1};
                col.InsertOne(profile);
            }
            else
            {
                profile.Visitas += 1;
                col.ReplaceOne(p => p.Id == id, profile);
            }
        }          

        public void RemoveUserProfile(UserProfile profile)
        {
            try
            {
                var col = _db.GetCollection<UserProfile>("UserProfile");

                col.DeleteOne(p => p.Id == profile.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
            }            
        }
    }

}