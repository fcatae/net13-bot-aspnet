using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Models
{
    public class UserProfileModel
    {
        public string Id { get; set; }
        public string Mensagem { get; set; }
        public string User { get; set; }
        public string TipoRegistro { get; set; }
        public int Visitas { get; set; }
    }
}
