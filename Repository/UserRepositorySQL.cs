using SimpleBot.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SimpleBot.Repository
{
    public class UserRepositorySQL : IUserProfileRepository
    {
        private readonly SqlConnection client;

        public UserRepositorySQL()
        {
            client = new SqlConnection(ConfigurationManager.AppSettings["ConnectionStringSQL"].ToString());
            
        }
        public UserProfile GetProfile(string id)
        {
            UserProfile userProfile = null;
            try
            {
                client.Open();
                string sql = "select * from UserProfile where idUser=@idUser";
                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@idUser", SqlDbType.VarChar,50).Value = id;

                SqlDataReader rs = myCommand.ExecuteReader();
                if (rs.Read())
                {
                    userProfile = new UserProfile();
                    userProfile.IdUser = (string)rs["IdUser"];                    
                    userProfile.Visitas = (int)rs["Visitas"];
                }
                rs.Close();
            }
            catch(Exception erro)
            {
                string er = erro.Message;
            }
            finally
            {
                client.Close();
            }
                        
            return userProfile;
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
                client.Open();
                string sql = "UPDATE [UserProfile] SET Visitas=@Visitas where idUser=@IdUser";
                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@IdUser", SqlDbType.VarChar, 50).Value = profile.IdUser;
                myCommand.Parameters.Add("@Visitas", SqlDbType.Int).Value = profile.Visitas;

                myCommand.ExecuteNonQuery();
            }
            finally
            {
                client.Close();
            }            
        }

        private void insert(UserProfile profile)
        {
            try
            {
                client.Open();
                string sql = "insert into [UserProfile] (IdUser,Visitas)values(@IdUser,@Visitas)";

                SqlCommand myCommand = new SqlCommand(sql, client);
                myCommand.Parameters.Add("@IdUser", SqlDbType.VarChar, 50).Value = profile.IdUser;
                myCommand.Parameters.Add("@Visitas", SqlDbType.Int).Value = profile.Visitas;

                myCommand.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                string er = erro.Message;
            }
            finally
            {
                client.Close();
            }
        }

        public void RemoveUserProfile(UserProfile profile)
        {
            
        }
    }
}