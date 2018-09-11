using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository.MongoDB
{
    [BsonIgnoreExtraElements]
    public class UserProfileMongo
    {
        public int _id { get; set; }

        public int Visitas { get; set; }
    }
}