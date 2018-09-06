using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{

    [BsonIgnoreExtraElements]
    [BsonDiscriminator("bot")]
    public class MessageBson
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }

        [BsonElement("idUser")]
        public string idUser { get; set; }

        [BsonElement("user")]
        public string User { get; }
        [BsonElement("text")]
        public string Text { get; }

        public MessageBson(string id, string username, string text)
        {
            this.idUser = id;
            this.User = username;
            this.Text = text;
        }
    }
}