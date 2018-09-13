using SimpleBot.Repository;

namespace SimpleBot
{
    public class SimpleBotUser
    {       
        private static readonly DBContext context = new DBContext();

        public static string Reply(Message message)
        {
            UserRepositoryMongo user = new UserRepositoryMongo();

            var id = message.Id;
            var profile = user.GetProfile(id);

            if (profile == null)
            {
                profile = new UserProfile();
                profile.IdUser = id;
                profile.Visitas = 0;
                user.insert(profile);
            }

            user.SetProfile(id, ref profile);
            switch (message.Text)
            {
                case "Qual o meu id?":
                    return $"Seu id é {profile.IdUser}";                    
                case "Apagar meu profile":
                    user.RemoveUserProfile(profile);
                    return $"Profile '{profile.IdUser}' apagado com sucesso...";
            }
            if(profile.Visitas ==1)
                return $"Ola, seja bem vindo! Essa é a sua {profile.Visitas}ª mensagem";

            return $"{message.User} enviou a {profile.Visitas}ª mensagem";
        }

    }
}