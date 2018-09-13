
using SimpleBot.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity;

namespace SimpleBot.Repository
{
    public class UserRepositoryEntity : IUserProfileRepository
    {
        protected DBContext Db;
        protected DbSet<UserProfile> DbSet;


        public UserRepositoryEntity(DBContext context)
        {
            Db = context;
            DbSet = Db.Set<UserProfile>();
        }

        public UserProfile GetProfile(string id)
        {
            UserProfile user = null;
            try
            {
                user = DbSet.Find(id);
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }
            return user;
        }        
        public void SetProfile(string id, ref UserProfile profile)
        {
            profile.Visitas += 1;
            this.update(profile);                
        }
        public void update(UserProfile profile)
        {
            try
            {
                var entry = Db.Entry(profile);
                DbSet.Attach(profile);
                entry.State = EntityState.Modified;
                Db.SaveChanges();
            }catch(Exception erro)
            {
                string er = erro.Message;
            }
        }
        public void insert(UserProfile profile)
        {
            try
            {
                var objReturn = DbSet.Add(profile);
                Db.SaveChanges();
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }
        }
        public void RemoveUserProfile(UserProfile profile)
        {
            DbSet.Remove(DbSet.Find(profile.IdUser));
            Db.SaveChanges();
        }
        public void Dispose()
        {
            if (Db != null)
            {
                Db.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}