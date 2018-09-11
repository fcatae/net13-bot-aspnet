using SimpleBot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;

namespace SimpleBot.Logic
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {

        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        public UserProfileMongoRepository()
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
                    .Find(u => u.Id == id).First();
            }
            catch
            {
            }

            return usuarioEncontrado;
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var col = db.GetCollection<UserProfile>("UserProfile");

            if (profile == null)
            {
                profile = new UserProfile();
                profile.Id = id;
                profile.Visitas = 1;
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
            var col = db.GetCollection<UserProfile>("UserProfile");

            col.DeleteOne(p => p.Id == profile.Id);
        }
    }

}