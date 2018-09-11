using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Logic;
using SimpleBot.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class SimpleBotUser
    {

        private readonly IUserProfileRepository userProfileRepository;
        public SimpleBotUser()
        {
            //this.userProfileRepository = new UserProfileMongoRepository("mongodb://localhost:27017");
            this.userProfileRepository = new UserProfileDapperRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\logonrmlocal\Source\Repos\net13-bot-aspnet\App_Data\Database.mdf;Integrated Security=True");
            //this.userProfileRepository = new UserProfileSqlClientRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\logonrmlocal\Source\Repos\net13-bot-aspnet\App_Data\Database.mdf;Integrated Security=True");
            //this.userProfileRepository = new UserProfileEFRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\logonrmlocal\Source\Repos\net13-bot-aspnet\App_Data\Database.mdf;Integrated Security=True");
        }

        public string Reply(Message message)
        {
            var ret = this.userProfileRepository.GetProfile(message.Id);
            int total = ret == null ? 0 : ret.Visitas;
            this.userProfileRepository.SetProfile(message.Id, new UserProfile() { Id = message.Id, Visitas = ++total });

            ret = this.userProfileRepository.GetProfile(message.Id);
            return $"{message.User} disse '{message.Text}', total de Visitas: '{ret.Visitas}'";
        }
    }
}