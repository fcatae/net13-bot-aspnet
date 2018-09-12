using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Models
{
    public class MessageModel
    {
        public MessageModel(string userFromId, string userFromName, string text)
        {
            Id = userFromId;
            User = userFromName;
            Text = text;

        }
        public string Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
    }
}
