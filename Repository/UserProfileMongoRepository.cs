﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {
        private readonly IMongoCollection<UserProfileMongo> _collection;

        public UserProfileMongoRepository()
        {
            _collection = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString)
                                        .GetDatabase("net13")
                                        .GetCollection<UserProfileMongo>("userProfile");
        }
        public UserProfile GetProfile(string id)
        {
            var filtro = Builders<UserProfileMongo>.Filter.Eq("id", id);
            var userProfileMongo = _collection.Find(filtro).FirstOrDefault();

            return new UserProfile
            {
                Id = userProfileMongo._id,
                Visitas = userProfileMongo.Visitas
            };
        }

        public void SetProfile(string id, UserProfile profile)
        {
            var filtro = Builders<UserProfileMongo>.Filter.Eq("id", id);

            var bson = new UserProfileMongo
            {
                _id = profile.Id,
                Visitas = profile.Visitas
            };

            _collection.ReplaceOne(filtro, bson, new UpdateOptions { IsUpsert = true });
        }
    }
}