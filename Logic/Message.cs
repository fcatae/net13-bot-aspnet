namespace SimpleBot.Logic
{
    public class Message
    {
        public string Id { get; }
        public string User { get; }
        public string Text { get; }

        public Message(string id, string username, string text)
        {
            Id = id;
            User = username;
            Text = text; 
        }
    }
}