using SimpleBot.Interfaces;
using SimpleBot.Models;
using SimpleBot.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Logic 
{
    public class SimpleBotUserLogic : IUserProfileRepository<UserProfileSQLRepository>
    {
        //IUserProfileRepository<UserProfileSQLRepository> db;

        UserProfileSQLRepository db = new UserProfileSQLRepository();

        public string Reply(MessageModel message)
        {
            return db.Reply(message);
        }

        public  UserProfileModel GetProfile(string id)
        {
            

            return db.GetProfile(id);
        }

        public  void SetProfile(string id, MessageModel profile)
        {
            db.SetProfile(id, profile);
        }
    }
}
