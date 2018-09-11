using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using MongoDB.Bson.Serialization;
using SimpleBot.Repository;

namespace SimpleBot
{
    public class SimpleBotUser
    {       
        public static string Reply(Message message)
        {
            UserRepositorySQL user = new UserRepositorySQL();
            var id = message.Id;
            var profile = user.GetProfile(id);

            user.SetProfile(id, ref profile);
            switch (message.Text)
            {
                case "Qual o meu id?":
                    return $"Seu id é {profile.IdUser}";                    
                case "Apagar meu profile":
                    user.RemoveUserProfile(profile);
                    return $"Profile '{profile.IdUser}' apagado com sucesso...";
            }
            if(profile.Visitas ==1)
                return $"Ola, seja bem vindo! Essa é a sua {profile.Visitas}ª mensagem";

            return $"{message.User} enviou a {profile.Visitas}ª mensagem";
        }

    }
}