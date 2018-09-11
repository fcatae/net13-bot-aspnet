using MongoDB.Driver;
using SimpleBot.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            catch
            {
            }

            return usuarioEncontrado;
        }

        public void SetProfile(string id, ref UserProfile profile)
        {           
            var col = db.GetCollection<UserProfile>("UserProfile");

            if (profile == null)
            {
                profile = new UserProfile();
                profile.IdUser = id;
                profile.Visitas = 1;
                col.InsertOne(profile);
            }
            else
            {
                profile.Visitas += 1;
                col.ReplaceOne(p => p.IdUser == id, profile);
            }
        }

        public void RemoveUserProfile(UserProfile profile)
        {            
            var col = db.GetCollection<UserProfile>("UserProfile");

            col.DeleteOne(p => p.IdUser == profile.IdUser);
        }
    }
}