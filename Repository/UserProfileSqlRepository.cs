using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace SimpleBot.Repository
{
    public class UserProfileSqlRepository : IUserProfileRepository
    {
        readonly string _connectionString;

        public UserProfileSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
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