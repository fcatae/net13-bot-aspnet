using SimpleBot.Repository.SqlServer;

namespace SimpleBot.Logic
{
    public class SqlServerSimpleBotUser
    {

        public static string Reply(Message message)
        {          
            var profile = GetProfile(message.Id);            
            SetProfile(profile);
            profile = GetProfile(message.Id);

            return $"{message.User} disse '{message.Text}' e mandou {profile.QtdMensagens} mensagens.";
        }


        public static UserProfile GetProfile(string id)
        {
            var repo = new SqlServerUserProfileRepository();
            var profile = repo.GetProfile(id);
            if (profile == null)
            {
                repo.InsertProfile(id);
                profile = repo.GetProfile(id);
            }

            return profile;
        }
        public static void SetProfile(UserProfile profile)
        {
            profile.QtdMensagens++;

            var repo = new SqlServerUserProfileRepository();
            repo.SetProfile(profile);
        }       
    }
}