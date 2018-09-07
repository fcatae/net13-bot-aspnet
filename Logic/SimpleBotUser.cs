using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson.Serialization;
using System.Configuration;

namespace SimpleBot
{
    public class SimpleBotUser
    {       
        public static string Reply(Message message)
        {
            
            var id = message.Id;
            var profile = GetProfile(id);
           
            SetProfile(id, ref profile);
            switch (message.Text)
            {
                case "Qual o meu id?":
                    return $"Seu id é {profile.IdUser}";                    
                case "Apagar meu profile":
                    RemoveUserProfile(profile);
                    return $"Profile '{profile.IdUser}' apagado com sucesso...";
            }
            if(profile.Visitas ==1)
                return $"Ola, seja bem vindo! Essa é a sua {profile.Visitas}ª mensagem";

            return $"{message.User} enviou a {profile.Visitas}ª mensagem";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient();
            try
            {                
                   client = new MongoClient(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            }catch(Exception er)
            {
                string erro = er.Message;
            }
            var db = client.GetDatabase("dbNet13");

            UserProfile usuarioEncontrado = null;
            try
            {
                usuarioEncontrado = db.GetCollection<UserProfile>("UserProfile")
                    .Find(u => u.IdUser == id).First();                
            }
            catch
            {
            }

            return usuarioEncontrado;
        }

        public static void SetProfile(string id,ref  UserProfile profile)
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            var db = client.GetDatabase("dbNet13");
            var col = db.GetCollection<UserProfile>("UserProfile");

            if (profile == null)
            {
                profile = new UserProfile();
                profile.IdUser = id;
                profile.Visitas = 1;
                col.InsertOne(profile);
            }
            else
            {
                profile.Visitas += 1;
                col.ReplaceOne(p=>p.IdUser == id,profile);
            }
        }

        public static void RemoveUserProfile(UserProfile profile)
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            var db = client.GetDatabase("dbNet13");
            var col = db.GetCollection<UserProfile>("UserProfile");

            col.DeleteOne(p=>p.IdUser==profile.IdUser);
        }
    }
}