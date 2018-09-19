using SimpleBot.Model;

namespace SimpleBot.Logic
{
    public  class SimpleBotUser
    {

        public string Popula_UserProfile(Message message)
        {
            var id = message.Id;
            UserProfile prof;

            using (var context = new Contexto())
            {
                var userProfileSql = new UserProfileSqlRepository(context);

                prof = userProfileSql.GetProfile(id);

                prof.Visitas += 1;

                userProfileSql.SetProfile(id, prof);
            }

            return $"{message.User} disse '{message.Text} e mandou {prof.Visitas} '";
        }
        
        public string Reply(Message message)
        {
           return Popula_UserProfile(message);
        }

       
    }
}