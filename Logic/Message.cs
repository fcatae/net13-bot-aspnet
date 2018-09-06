using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class Message
    {
        public string UserId { get; }
        public string User { get; }
        public string Text { get; }
        public string MessageKind { get; set; }

        public Message(string Userid, string username, string text, string messageKind = "Input")
        {
            this.UserId = Userid;
            this.User = username;
            this.Text = text;
            this.MessageKind = messageKind;
        }
    }
}