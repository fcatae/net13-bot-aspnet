using SimpleBot;
using SimpleBot.EntityConfig;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace SimpleBot.Repository
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base(ConfigurationManager.AppSettings["ConnectionStringSQL"].ToString())
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<UserProfile> userProfile { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new UserProfileConfig());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}