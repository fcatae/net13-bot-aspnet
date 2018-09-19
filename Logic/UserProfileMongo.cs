using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    [BsonIgnoreExtraElements]
    public class UserProfileMongo
    {
        //public string _id { get; set; }
        public string Id { get; set; }
        public int Visitas { get; set; }
        public DateTime HorarioRegistro { get; set; }
    }
}