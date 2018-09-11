using Dapper;
using SimpleBot.Logic;
using SimpleBot.Repository.Interfaces;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace SimpleBot.Repository.SqlServer
{
    public class SqlServerUserProfileRepository : IUserProfileRepository
    {
        public UserProfile GetProfile(string id)
        {
            UserProfile user = null;
            string sql = "SELECT * FROM UserProfile WHERE Id = @id";
            var param = new { id };
            
            using (var connection = new SqlConnection(Config.SQLServerConfiguration.SimpleBotConnectionString))                            
                user = connection.QueryFirstOrDefault<UserProfile>(sql, param: param);
                        
            return user;
        }
        public long SetProfile(UserProfile profile)
        {
            if (profile is null) return 0;

            long affectedRows = 0;
            string sql = $"UPDATE UserProfile SET QtdMensagens = @QtdMensagens WHERE Id = @Id";
            var param = new { profile.Id, profile.QtdMensagens };
            
            using (var connection = new SqlConnection(Config.SQLServerConfiguration.SimpleBotConnectionString))
                affectedRows = connection.Execute(sql, param: param);

            return affectedRows;
        }

        public UserProfile InsertProfile(string Id)
        {
            int QtdMensagens = 0;            
            string sql = $"INSERT INTO UserProfile VALUES (@Id, @QtdMensagens)";
            var param = new { Id, QtdMensagens };
            UserProfile user = null;

            using (var connection = new SqlConnection(Config.SQLServerConfiguration.SimpleBotConnectionString))
                user = connection.ExecuteScalar<UserProfile>(sql, param: param);
            
            return user;
        }       
    }
}
