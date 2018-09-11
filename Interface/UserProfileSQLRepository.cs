using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot
{
    public class UserProfileSQLRepository : IUserProfileRepository
    {

        private BotContext db = new BotContext();

        public UserProfile GetProfile(string id)
        {
            try
            {
                var profile = db.profileSQLs.Where(x => x.Id == id);
                return profile.Select(x => new UserProfile { Id = x.Id, Visitas = x.Visitas }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserProfile SetProfile(string id,UserProfile profile)
        {
            try
            {
                ProfileSQL sqlProfile;
                if(profile == null)
                {
                    //CREATE
                    sqlProfile = new ProfileSQL { Id = id, Visitas = 1 };
                    db.profileSQLs.Add(sqlProfile);
                    db.SaveChanges();
                }
                else
                {
                    //UPDATE
                    sqlProfile = new ProfileSQL { Id = profile.Id, Visitas = profile.Visitas + 1 };
                    db.Entry(sqlProfile).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return new UserProfile { Id = sqlProfile.Id, Visitas = sqlProfile.Visitas };

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}