using SimpleBot;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace SimpleBot.Logic
{
    class UserProfileConfiguration : EntityTypeConfiguration<UserProfileSQL>
    {
        public UserProfileConfiguration()
        {
            ToTable("UserProfile");

            #region [Propriedades]

            Property(x => x.Id).HasColumnName("ID").HasColumnType("varchar").IsRequired();
            Property(x => x.Visitas).HasColumnName("VISITAS").HasColumnType("int").IsRequired();

            #endregion

            #region [Chaves]

            HasKey(x => x.Id);

            #endregion
        }
    }
}
