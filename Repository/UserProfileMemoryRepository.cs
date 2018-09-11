using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBot.Logic;
namespace SimpleBot.Repository
{
    public class UserProfileMemoryRepository : IUserProfileRepository
    {
       
        public UserProfileMemoryRepository(string connectionString)
        {

        }
        public UserProfile GetProfile(string id)
        {
            throw new NotImplementedException();
        }

        public void SetProfile(string id, UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}