using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    public class UserProfileSQLDbContext : DbContext
    {
        public DbSet<UserProfileSQL> UserProfile { get; set; }

        public UserProfileSQLDbContext(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }
    }
}