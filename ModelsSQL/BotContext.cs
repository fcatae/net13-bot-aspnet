using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBot 
{
    public class BotContext : DbContext
    {
        public BotContext() : base("name=BotContext")
        {

        }

        public DbSet<MessageSQL> messageSQLs;
        public DbSet<ProfileSQL> profileSQLs;

    }
}