using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class Message
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public string Text { get; set; }

        public Message(string UserId, string username, string text)
        {
            this.UserId = UserId;
            this.User = username;
            this.Text = text;
        }
    }
}