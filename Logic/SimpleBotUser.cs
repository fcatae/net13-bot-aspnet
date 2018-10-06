using SimpleBot.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {   
            var profile = MongoDb.AddVisitReturnProfile(message.UserId);

            return $"{message.User} disse '{message.Text}' - Esta é a " + profile.Visitas + " vez que envia uma mensagem !";
        }

        public static UserProfile GetProfile(string id)
        {
            return MongoDb.GetProfile(id);            
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            MongoDb.SetProfile(profile);
        }
    }
}