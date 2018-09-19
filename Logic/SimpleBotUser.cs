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

            var id = message.Id;

            var visitas = 0;
            DateTime horarioRegistro = DateTime.Now;

            var profileMongoDB = GetProfileMongoDB(id);

            var profileSQLDB = GetProfileSqlDB(id);

            if (profileMongoDB.Visitas != profileSQLDB.Visitas)
            {
                if (profileSQLDB.HorarioRegistro > profileMongoDB.HorarioRegistro)
                {
                    visitas = profileSQLDB.Visitas;
                }
                else if (profileSQLDB.HorarioRegistro < profileMongoDB.HorarioRegistro)
                {
                    visitas = profileMongoDB.Visitas;
                }
            }
            else
            {
                visitas = profileSQLDB.Visitas;
                //visitas = profileMongoDB.Visitas;
            }

            visitas += 1;

            profileMongoDB.Id = id;
            profileMongoDB.Visitas = visitas;
            profileMongoDB.HorarioRegistro = horarioRegistro;

            SetProfileMongoDB(id, profileMongoDB);

            profileSQLDB.Id = id;
            profileSQLDB.Visitas = visitas;
            profileSQLDB.HorarioRegistro = horarioRegistro;

            SetProfileSqlDB(id, profileSQLDB);

            return $"{message.User} disse '{message.Text}' e enviou {visitas} mensagens.";
        }

        public static UserProfile GetProfileMongoDB(string Id)
        {
            string id = Id;

            if (_userProfileMongoDB == null)
                _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");

            var profileMongoDB = _userProfileMongoDB.GetProfile(id);

            if (profileMongoDB == null)
            {
                profileMongoDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0,
                    HorarioRegistro = DateTime.Now
                };
            }
            return profileMongoDB;
        }

        public static void SetProfileMongoDB(string Id, UserProfile userProfile)
        {
            var id = Id;
            var profileMongoDB = userProfile;

            if (_userProfileMongoDB == null)
                _userProfileMongoDB = new UserProfileMongoRepo("mongodb://localhost:27017");

            _userProfileMongoDB.SetProfile(id, profileMongoDB);
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
                    Visitas = 0,
                    HorarioRegistro = DateTime.Now
                };
            }
   
            profileMongoDB.Visitas += 1;
            profileMongoDB.HorarioRegistro = DateTime.Now;

            _userProfileMongoDB.SetProfile(id, profileMongoDB);

            return profileMongoDB;
        }

        public static UserProfile GetProfileSqlDB(string Id)
        {
            string id = Id;

            if (_userProfileSQLDB == null)
                _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");

            var profileSQLDB = _userProfileSQLDB.GetProfile(id);

            if (profileSQLDB == null)
            {
                profileSQLDB = new UserProfile()
                {
                    Id = id,
                    Visitas = 0,
                    HorarioRegistro = DateTime.Now
                };
            }
            return profileSQLDB;
        }

        public static void SetProfileSqlDB(string Id, UserProfile userProfile)
        {
            var id = Id;
            var profileSQLDB = userProfile;

            if (_userProfileSQLDB == null)
                _userProfileSQLDB = new UserProfileSQLRepo("Server=.;Database=SimpleBotDB;User Id=sa;Password=Pa$$w0rd;");

            _userProfileSQLDB.SetProfile(id, profileSQLDB);
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