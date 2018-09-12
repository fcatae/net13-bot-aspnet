using SimpleBot;
using System.Data.Entity.ModelConfiguration;

namespace SimpleBot.EntityConfig
{
    /// <summary>
    /// Classe: MenuConfig
    /// </summary>
    /// Autor: Gerado automaticamento pelo sistema ClassGenerator por Wagner Sereia dos Santos
    /// Revisao da classe: 1
    public class UserProfileConfig : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfig()
        {
            HasKey(p => p.IdUser);

            Property(p => p.IdUser)                
                .IsRequired()
            .HasMaxLength(50);
                        
            Ignore(c => c._id);
            
            ToTable("UserProfile");
        }
    }
}
