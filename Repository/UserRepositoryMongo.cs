using MongoDB.Driver;
using SimpleBot.Interfaces.Repository;
using System;
using System.Linq;
using System.Configuration;

namespace SimpleBot.Repository
{
    public class UserRepositoryMongo : IUserProfileRepository
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase db;
        public UserRepositoryMongo()
        {
            client = new MongoClient(ConfigurationManager.AppSettings["ConnectionStringMongo"].ToString());
            db = client.GetDatabase("dbNet13");
        }

        public UserProfile GetProfile(string id)
        {      
            UserProfile usuarioEncontrado = null;
            try
            {
                usuarioEncontrado = db.GetCollection<UserProfile>("UserProfile")
                    .Find(u => u.IdUser == id).First();
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }

            return usuarioEncontrado;
        }

        public void SetProfile(string id, ref UserProfile profile)
        {           
            var col = db.GetCollection<UserProfile>("UserProfile");

            profile.Visitas += 1;
            this.update(profile);
        }

        public void RemoveUserProfile(UserProfile profile)
        {
            try
            {
                var col = db.GetCollection<UserProfile>("UserProfile");

                col.DeleteOne(p => p.IdUser == profile.IdUser);
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.Message);
            }
        }
        
        public void update(UserProfile profile)
        {
            var col = db.GetCollection<UserProfile>("UserProfile");

            col.ReplaceOne(p => p.IdUser == profile.IdUser, profile);
        }

        public void insert(UserProfile profile)
        {
            var col = db.GetCollection<UserProfile>("UserProfile");

            col.InsertOne(profile);
        }

        public void Dispose()
        {
            if (client != null)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}