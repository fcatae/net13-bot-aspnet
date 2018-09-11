using SimpleBot.Logic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository
{
    public class UserProfileEFRepository : IUserProfileRepository
    {

        private readonly Database Db;

        public UserProfileEFRepository(string connectionString)
        {
            Db = new Database(connectionString);
        }
        public UserProfile GetProfile(string id)
        {
            var ret = Db.UserProfile.FirstOrDefault(x => x.Id == id);
            return new UserProfile()
            {
                Id = ret.Id,
                Visitas = ret.Visitas
            };
        }

        public void SetProfile(string id, UserProfile profile)
        {
            try
            {
                var prof = Db.UserProfile.FirstOrDefault(x => x.Id == id);
                if (prof != null)
                {
                    prof.Visitas = profile.Visitas;
                    Db.Entry(prof).State = EntityState.Modified;
                }
                else
                {
                    Db.UserProfile.Add(new UserProfileEF()
                    {
                        Id = profile.Id,
                        Visitas = profile.Visitas
                    });
                }
                Db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}