using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownMusicEvents
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Fan> Fans { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public DatabaseContext()
            : base("name = DatabaseContext")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
