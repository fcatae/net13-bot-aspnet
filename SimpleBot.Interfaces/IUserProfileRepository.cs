using SimpleBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Interfaces
{
    public interface IUserProfileRepository
    {
        string Reply(MessageModel message);

         UserProfileModel GetProfile(string id);

        void SetProfile(string id, MessageModel profile);
    }
}
