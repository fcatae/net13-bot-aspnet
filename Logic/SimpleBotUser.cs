using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{

    public class SimpleBotUser
    {
        private static IUserProfileRepository _userProfileRepository;
        private static IMessageRepository _messageRepository;
        private static string stringConnection = "mongodb://localhost";

        static SimpleBotUser()
        {
            _userProfileRepository = new UserProfileRepository(stringConnection);
            _messageRepository = new MessageRepository(stringConnection);
        }

        public static string Reply(Message message)
        {
            try
            {
                var messageBson = new MessageBson(message.Id, message.User, message.Text);
                //INSERT MESSAGE
                _messageRepository.postMessage(messageBson);

                //GET PROFILE 
                var user = _userProfileRepository.GetProfile(message.Id);

                //SET PROFILE
                user = _userProfileRepository.SetProfile(message.Id, user);

                return $"{message.User} disse '{message.Text}' e mandou { user.Visitas } mensagens";
            }
            catch (Exception ex)
            {
                throw;
            }

           
        }
        
    }
}