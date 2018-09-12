

using Dapper;
using SimpleBot.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SimpleBot.Repository
{
    public class UserRepositoryDapper : IUserProfileRepository
    {
        protected DBContext Db;
        protected DbSet<UserProfile> DbSet;


        public UserRepositoryDapper(DBContext context)
        {
            Db = context;
            DbSet = Db.Set<UserProfile>();
        }

        public UserProfile GetProfile(string id)
        {
            UserProfile user = null;
            try
            {
                var cn = Db.Database.Connection;
                string sql = "select * from UserProfile where idUser=@idUser";
                user = cn.Query<UserProfile>(sql, new { idUser = id }).First();
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }
            return user;
        }

        
        public void SetProfile(string id, ref UserProfile profile)
        {
            if (profile == null)
            {
                profile = new UserProfile();
                profile.IdUser = id;
                profile.Visitas = 1;
                this.insert(profile);
            }
            else
            {
                profile.Visitas += 1;
                this.update(profile);
            }    
        }
        private void update(UserProfile profile)
        {
            try
            {
                var cn = Db.Database.Connection;
                string sql = "UPDATE [UserProfile] SET Visitas=@Visitas where idUser=@IdUser";
                cn.Query<UserProfile>(sql,
                    new
                    {
                        Visitas = profile.Visitas,
                        IdUser = profile.IdUser                        
                    }
                    );
            }catch(Exception erro)
            {
                string er = erro.Message;
            }
        }

        private void insert(UserProfile profile)
        {
            try
            {
                var cn = Db.Database.Connection;
                string sql = "insert into [UserProfile] (IdUser,Visitas)values(@IdUser,@Visitas)";
                cn.Query<UserProfile>(sql,new {
                    IdUser = profile.IdUser,
                    Visitas = profile.Visitas
                });
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }
        }

        public void RemoveUserProfile(UserProfile profile)
        {
            var cn = Db.Database.Connection;
            string sql = "Delete from [UserProfile] where idUser=@IdUser";
            cn.Query<UserProfile>(sql,new { idUser = profile.IdUser });            
        }
    }
}