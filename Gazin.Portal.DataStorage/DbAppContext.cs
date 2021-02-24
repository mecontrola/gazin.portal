using Gazin.Portal.Data.Entities;
using Gazin.Portal.DataStorage.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace Gazin.Portal.DataStorage
{
    public class DbAppContext : DbContext, IDbAppContext
    {
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<WorkdayOfWeek> WorkdayOfWeeks { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAppContext).Assembly);
            modelBuilder.Seed();
        }
    }
}