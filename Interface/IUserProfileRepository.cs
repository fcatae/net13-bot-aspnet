using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot
{
    interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);

        UserProfile SetProfile(string id,UserProfile profile);
    }
}
