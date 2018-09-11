using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Config;
using SimpleBot.Repository.SqlServer;
using System;
using System.Linq;

namespace SimpleBot.Logic
{
    public class SimpleBotUser
    {

        public static string Reply(Message message)
        {
            GravarMensagem(message);

            var profile = GetProfile(message.Id);            
            SetProfile(profile);
            profile = GetProfile(message.Id);

            return $"{message.User} disse '{message.Text}' e mandou {profile.QtdMensagens} mensagens.";
        }


        public static UserProfile GetProfile(string id)
        {
            #region mongo
            //try
            //{
            //    var connection = MongoDbConfiguration.Conexao;
            //    var cliente = new MongoClient(connection);

            //    var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            //    var col = db.GetCollection<BsonDocument>("usuario");

            //    var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            //    var bson = col.Find(filtro).FirstOrDefault();

            //    if (bson == null)
            //    {
            //        return new UserProfile { Id = id, QtdMensagens = 1 };
            //    }
            //    else
            //    {
            //        return new UserProfile
            //        {
            //            Id = bson["id"].ToString(),
            //            QtdMensagens = bson["mensagens"].ToInt32()
            //        };
            //    }

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            #endregion

            var repo = new SqlServerUserProfileRepository();
            var profile = repo.GetProfile(id);
            if (profile == null)
            {
                repo.InsertProfile(id);
                profile = repo.GetProfile(id);
            }

            return profile;
        }
        public static void SetProfile(UserProfile profile)
        {
            #region mongo
            //var connection = MongoDbConfiguration.Conexao;
            //var cliente = new MongoClient(connection);

            //var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            //var col = db.GetCollection<BsonDocument>(MongoDbConfiguration.TabelaUsuario);

            //var filtro = Builders<BsonDocument>.Filter.Eq("id", profile.Id);
            //var bson = col.Find(filtro).FirstOrDefault();

            //if (bson == null)
            //{
            //    col.InsertOne(new BsonDocument { { "id", profile.Id }, { "mensagens", 1 } });
            //}
            //else
            //{
            //    bson["mensagens"] = profile.QtdMensagens + 1;
            //    col.ReplaceOne(filtro, bson);
            //}
            #endregion mongo

            profile.QtdMensagens++;

            var repo = new SqlServerUserProfileRepository();
            repo.SetProfile(profile);
        }
        public static void GravarMensagem(Message message)
        {
            //try
            //{
            //    var connection = MongoDbConfiguration.Conexao;
            //    var cliente = new MongoClient(connection);

            //    var db = cliente.GetDatabase(MongoDbConfiguration.Banco);

            //    var col = db.GetCollection<BsonDocument>(MongoDbConfiguration.TabelaMensagem);
            //    var bson = new BsonDocument()
            //{
            //    { "id" , message.Id  },
            //    { "user" , message.User  },
            //    { "text" , message.Text  }
            //};

            //    col.InsertOne(bson);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}