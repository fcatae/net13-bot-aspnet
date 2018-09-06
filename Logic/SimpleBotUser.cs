using MongoDB.Driver;
using SimpleBot.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{

    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            var messageBson = new MessageBson(message.Id, message.User, message.Text);
            //INSERT
            mongoDB.postDocument(messageBson);
            //GET
            var user = GetProfile(message.Id);

            return $"{message.User} disse '{message.Text}' e mandou { user.Visitas } mensagens";
        }

        public static UserProfile GetProfile(string id)
        {
            //GET
            var userProfile = mongoDB.getDocument(id);
            //INSERT PROFILE
            if (userProfile == null)
            {
                //CREATE
                userProfile = new UserProfile { Id = id, Visitas = 1 };
                mongoDB.postDocumentPerfil(userProfile);
            }
            else
            {
                //UPDATE
                userProfile.Visitas = userProfile.Visitas + 1;
                SetProfile(userProfile);
            }
               

            return userProfile;
        }

        public static void SetProfile(UserProfile profile)
        {
            UpdateDefinition<UserProfile> update = Builders<UserProfile>.Update.Set("Visitas", profile.Visitas);
            mongoDB.putDocument(profile.Id, update);
        }
    }
}