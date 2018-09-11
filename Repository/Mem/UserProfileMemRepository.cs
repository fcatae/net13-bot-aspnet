using SimpleBot.Repository.Interfaces;
using SimpleBot.Logic;
using System;

namespace SimpleBot.Repository.Mem    
{
    public class UserProfileMemRepository : IUserProfileRepository
    {
        public UserProfile GetProfile(string id)
        {
            throw new NotImplementedException();
        }

        public long SetProfile(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}