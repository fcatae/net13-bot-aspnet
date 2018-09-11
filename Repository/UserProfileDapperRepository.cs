using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimpleBot.Repository
{
    public class UserProfileDapperRepository : IUserProfileRepository
    {
        private readonly string _connnectionString;
        public UserProfileDapperRepository(string ConnectionString)
        {
            this._connnectionString = ConnectionString;
        }
        public UserProfile GetProfile(string id)
        {
            using (var connection = new SqlConnection(this._connnectionString))
            {
                return connection.Query<UserProfile>("select * from UserProfile where id=@id", new
                {
                    @id = id
                }, commandType: System.Data.CommandType.Text).FirstOrDefault();
            }
        }

        public void SetProfile(string id, UserProfile profile)
        {
            using (var connection = new SqlConnection(this._connnectionString))
            {
                string script = "insert into UserProfile (id,Visitas) values(@id,@visitas)";
                var ret = GetProfile(id);
                if(ret != null)
                    script = "update UserProfile set Visitas  = @visitas where id = @id";
                connection.Execute(script, new
                {
                    @id = profile.Id,
                    @visitas = profile.Visitas,
                }, commandType: System.Data.CommandType.Text);
            }
        }
    }
}