using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserProfile
    {
        public ObjectId _id { get; set; }
        public string IdUser { get; set; }
        public int Visitas { get; set; }
    }
}