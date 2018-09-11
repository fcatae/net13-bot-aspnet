using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    [Table("UserProfile")]
    public class UserProfileEF
    {
        [Key]
        public string Id { get; set; }
        public int Visitas { get; set; }
    }
}