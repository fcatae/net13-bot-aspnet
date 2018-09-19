using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Logic;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static IUserProfileRepository _userProfileMongoDB;
        static IUserProfileRepository _userProfileSQLDB;

        public SimpleBotUser()
        {
            _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");
            _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");
            //Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
        }

        public static string Reply(Message message)
        {
            /*
             * Salva mensagens
             * 
            var doc = new BsonDocument
            {
                {"id",  message.Id},
                {"user", message.User },
                {"originalText", message.Text },
                {"returnedText", returnedText },
                {"messageDT", DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") }
            };

            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("SimpleBotDB");
            var col = db.GetCollection<BsonDocument>("messagesTable");

            col.InsertOne(doc);

            UserProfile userProfile = GetProfile(message.Id);
            userProfile.Visitas += 1;
            SetProfile(userProfile.Id, userProfile);
            */

            var visitas = 0;

            var profileMongoDB = RegistraVisitaEmMongoDB(message);
            visitas = profileMongoDB.Visitas;

            //var profileSQLDB = ProfileSQLDB(message);
            //visitas = profileSQLDB.Visitas;

            return $"{message.User} disse '{message.Text}' e mandou {visitas} messages";
        }

        public static UserProfile RegistraVisitaEmMongoDB(Message message)
        {
            var id = message.Id;
            if (_userProfileMongoDB == null)
                _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");

            var profileMongoDB = _userProfileMongoDB.GetProfile(id);

            if (profileMongoDB == null)
            {
                profileMongoDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };
            }
   
            profileMongoDB.Visitas += 1;

            _userProfileMongoDB.SetProfile(id, profileMongoDB);

            return profileMongoDB;
        }

        public static UserProfile ProfileSQLDB(Message message)
        {           
            var id = message.Id;

            if (_userProfileSQLDB == null)
                _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");

            var profileSQLDB = _userProfileSQLDB.GetProfile(id);

            if (profileSQLDB == null)
            {
                profileSQLDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0
                };
            }

            profileSQLDB.Visitas += 1;

            _userProfileSQLDB.SetProfile(id, profileSQLDB);

            return profileSQLDB;
        }

        //    public static UserProfile GetProfile(string id)
        //    {

        //        var client = new MongoClient("mongodb://localhost:27017");
        //        var db = client.GetDatabase("SimpleBotDB");
        //        var col = db.GetCollection<BsonDocument>("UserProfile");

        //        var filtro = Builders<BsonDocument>.Filter.Eq("Id", id);
        //        var res = col.Find(filtro).ToList();

        //        UserProfile userProfile = new UserProfile();
        //        if (res.Count == 0)
        //        {
        //            userProfile.Id = id;
        //            userProfile.Visitas = 1;
        //        }
        //        else if (res.Count == 1)
        //        {
        //            foreach (var e in res)
        //            {
        //                var s = e.Values.ToList();
        //                userProfile.Id = e[1].ToString();
        //                userProfile.Visitas = Int32.Parse(e[2].ToString());
        //                //userProfile.Visitas += 1;
        //            }

        //        }
        //        else
        //        { }

        //        return userProfile;
        //    }

        //    public static void SetProfile(string id, UserProfile profile)
        //    {          
        //        var doc = new BsonDocument
        //        {
        //            {"Id", profile.Id },
        //            {"Visitas", profile.Visitas }
        //        };

        //        var client = new MongoClient("mongodb://localhost:27017");
        //        var db = client.GetDatabase("SimpleBotDB");
        //        var col = db.GetCollection<BsonDocument>("UserProfile");
        //        var filtro = Builders<BsonDocument>.Filter.Eq("Id", id);

        //        if (profile.Visitas == 1)
        //        {
        //            col.InsertOne(doc);
        //        }
        //        else
        //        {
        //            col.ReplaceOne(filtro, doc);
        //        }
        //    }
    }
}