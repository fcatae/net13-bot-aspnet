using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Config
{
    public static class SQLServerConfiguration
    {
        public static string SimpleBotConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SimpleBot;Integrated Security=True;Pooling=False";        
    }
}