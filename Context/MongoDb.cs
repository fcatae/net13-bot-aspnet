using Microsoft.Bot.Connector;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SimpleBot.Context
{
    public static class MongoDb
    {
        private static MongoClient client = new MongoClient("mongodb://127.0.0.1");
        private static IMongoDatabase database = client.GetDatabase("test");
        private static IMongoCollection<Message> colMessages = database.GetCollection<Message>("messages");
        private static IMongoCollection<Activity> colActivities = database.GetCollection<Activity>("activities");
        private static IMongoCollection<UserProfile> colProfiles = database.GetCollection<UserProfile>("profiles");

        public static void SaveReceivedMessage(Message message)
        {
            colMessages.InsertOne(message);
        }

        public static void SaveSendedMessage(Message message)
        {
            message.MessageKind = "Output";
            colMessages.InsertOne(message);
        }

        public static void SaveActivity(Activity activity)
        {
            colActivities.InsertOne(activity);
        }        

        public static UserProfile GetProfile(string userId)
        {
            Expression<Func<UserProfile, bool>> filter = x => x.Id == userId;
            var profile = colProfiles.Find(filter);

            if (!profile.Any())
                colProfiles.InsertOne(new UserProfile() { Id = userId, Visitas = 0 });
            else
                return profile.First();
            try
            {
                var lista = colProfiles.Find(filter).ToList();
                var retorno = lista.First();
                return retorno;
            }
            catch(Exception ex) { }

            return new UserProfile();
        }

        public static UserProfile SetProfile(UserProfile profile)
        {
            Expression<Func<UserProfile, bool>> filter = x => x.Id == profile.Id;

            colProfiles.ReplaceOne(filter, profile, new UpdateOptions { IsUpsert = true });
            return profile;
        }

        public static UserProfile AddVisitReturnProfile(string userId)
        {
            var profile = GetProfile(userId);
            profile.Visitas++;
            SetProfile(profile);

            return profile;
        }
    }
}