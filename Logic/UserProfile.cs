using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserProfile
    {
        public string Id { get; set; }
        public int Visitas { get; set; }
        public DateTime HorarioRegistro { get; set; }
    }

    public class UserProfileSQL
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Visitas { get; set; }
        public DateTime HorarioRegistro { get; set; }
    }

}