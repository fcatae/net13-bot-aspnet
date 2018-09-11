﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Interface
{
    interface IUserProfileRepository
    {
        UserProfile GetProfile(string id);

        void SetProfile(string id, UserProfile profile);
    }
}
