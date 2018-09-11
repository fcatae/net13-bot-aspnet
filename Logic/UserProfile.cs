using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    [Table("UserProfile")]
    public class UserProfile
    {
        public string Id { get; set; }
        public int QtdMensagens { get; set; }
    }
}