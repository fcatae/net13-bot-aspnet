using SimpleBot.Model;

namespace SimpleBot.Logic
{
    // Parabens! Retiraram o "static" da classe
    
    public  class SimpleBotUser
    {
        // sugestao: use o construtor para receber o Repositorio
        IRepository _repo;        
        public SimpleBotUser(IRepository repo)
        {
            this._repo = repo;
        }        
        
        // implementacao incorreta do Pattern
        
        public string Popula_UserProfile(Message message)
        {
            var id = message.Id;
            UserProfile prof;

            // Nao use a classe Contexto
            // Nao use a classe UserProfileSqlRepository
            // Use somente a propriedade _repo, definida anteriormente
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
