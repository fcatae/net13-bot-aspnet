using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBot.Repository;
using System.Configuration;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static readonly IUserProfileRepository _users;

        static SimpleBotUser()
        {
            var mongodb = ConfigurationManager.AppSettings["MONGO"];



            _users = new UserProfileMongoRepository("mongodb://127.0.0.1:27017");
        }

        static public string Reply(Message message)
        {
            var id = message.Id;

            var profile = _users.GetProfile(id);

            profile.Visitas += 1;

            _users.SetProfile(id, profile);

            return $"{message.User} disse '{message.Text} e mandou {profile.Visitas} mensagens'";
        }
    }
}