using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository
{
    public class UserProfileSqlClientRepository : IUserProfileRepository
    {
        private readonly string _connectionString;
        public UserProfileSqlClientRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public UserProfile GetProfile(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("select * from UserProfile where id=@id", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserProfile()
                            {
                                Id = reader.GetString(0),
                                Visitas = reader.GetInt32(1)
                            };
                        }
                    }
                }
            }
            return new UserProfile();
        }

        public void SetProfile(string id, UserProfile profile)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string script = string.Empty;
                var user = GetProfile(id);
                script = user == null ? "insert into UserProfile (id,Visitas) values(@id,@visitas)" : "update UserProfile set Visitas =@Visitas where id=@id";
                using (var command = new SqlCommand(script, connection))
                {
                    command.Parameters.AddWithValue("@id", profile.Id);
                    command.Parameters.AddWithValue("@Visitas", profile.Visitas);
                    command.CommandType = System.Data.CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}