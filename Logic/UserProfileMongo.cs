using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace SimpleBot.Logic
{
    [BsonIgnoreExtraElements]
    public class UserProfileMongo
    {
        public string Id { get; set; }
        public int Visitas { get; set; }

    }
}