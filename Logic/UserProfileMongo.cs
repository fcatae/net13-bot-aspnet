﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBot
{
    [BsonIgnoreExtraElements]
    public class UserProfileMongo
    {
        public string _id { get; set; }
        public int Visitas { get; set; }
    }
}