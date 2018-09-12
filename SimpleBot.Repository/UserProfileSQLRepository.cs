using SimpleBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SimpleBot.Interfaces;

namespace SimpleBot.Repository
{
    public class UserProfileSQLRepository : IUserProfileRepository
    {
        private string connectionString = @"Data Source=.;Initial Catalog=ConectionSQL;Integrated Security=True";


        public UserProfileSQLRepository()
        {
           
        }
        
        public string Reply(MessageModel message)
        {
            var id = message.Id;

            var profile = this.GetProfile(id);
            
            if (profile != null)
            {
                profile.Visitas += 1;
            } 

            

            this.SetProfile(id, message);

            return $"{message.User} disse '{message.Text} e mandou{profile.Visitas} mensagens'";
        }

        public UserProfileModel GetProfile(string id)
        {

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PR_GetProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", id);

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read())
                {  

                    UserProfileModel ct = new UserProfileModel();
                    ct.Id = dr["Id"].ToString();
                    ct.Mensagem = dr["Mensagem"].ToString();
                    ct.User = dr["Usuario"].ToString();
                    ct.TipoRegistro = dr["TipoRegistro"].ToString();
                    return ct;

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

           
        }

        public void SetProfile(string id, MessageModel profile)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PR_SetProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@Mensagem", profile.Text);
                cmd.Parameters.AddWithValue("@User", profile.User);
                cmd.Parameters.AddWithValue("@TipoRegistro", "TEste");
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;  // retorna mensagem de erro
            }
            finally
            {
                con.Close();
            }
        }

        
    }
}
