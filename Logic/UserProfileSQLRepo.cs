using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    public class UserProfileSQLRepo : IUserProfileRepository
    {
        UserProfileSQLDbContext _dbContext;

        public UserProfileSQLRepo(string connectionString)
        {
            _dbContext = new UserProfileSQLDbContext(connectionString);
        }

        public UserProfile GetProfile(string id)
        {
            UserProfileSQL profileSql = _dbContext.UserProfile.FirstOrDefault(x => x.UserId == id);
            if (profileSql == null)
                return null;
            else
            {
                UserProfile userProfile = new UserProfile()
                {
                    Id = profileSql.UserId,
                    Visitas = profileSql.Visitas,
                    HorarioRegistro = profileSql.HorarioRegistro
                };

                return userProfile;
            }
        }

        public void SetProfile(string id, UserProfile profile)
        {
            UserProfileSQL profileSql = _dbContext.UserProfile.FirstOrDefault(x => x.UserId == id);

            if (profileSql == null)
            {
                profileSql = new UserProfileSQL()
                {
                    UserId = profile.Id,
                    Visitas = profile.Visitas,
                    HorarioRegistro = profile.HorarioRegistro
                };

                _dbContext.UserProfile.Add(profileSql);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.UserProfile.First(x => x.Id == profileSql.Id).UserId = profile.Id;
                _dbContext.UserProfile.First(x => x.Id == profileSql.Id).Visitas = profile.Visitas;
                _dbContext.UserProfile.First(x => x.Id == profileSql.Id).HorarioRegistro = profile.HorarioRegistro;
                _dbContext.SaveChanges();
            }
        }
    }
}