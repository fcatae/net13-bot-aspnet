using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Logic;
using SimpleBot.Model;

namespace SimpleBot
{
    public  class SimpleBotUser
    {

        public string Popula_UserProfile(Message message)
        {
            var id = message.Id;
            UserProfile prof;

            using (var context = new Contexto())
            {
                var userProfileSql = new UserProfileSQLRepository(context);

                prof = userProfileSql.GetProfile(id);

                prof.Visitas += 1;

                userProfileSql.SetProfile(id, prof);
            }

            return $"{message.User} disse '{message.Text} e mandou {prof.Visitas} '";
        }
        
        public string Reply(Message message)
        {

           return Popula_UserProfile(message);

            //var client = new MongoClient("mongodb://localhost:27017");


            //var db = client.GetDatabase("db01");
            //var col = db.GetCollection<BsonDocument>("tabela01");
            
            //var doc = new BsonDocument()
            //{
            //    {"id",message.Id },
            //    {"texto", message.Text },
            //    {"app","teste2" }
                
            //};         

            //col.InsertOne(doc);
           
           // return $"{message.User} disse '{message.Text}'";
        }

       
    }
}