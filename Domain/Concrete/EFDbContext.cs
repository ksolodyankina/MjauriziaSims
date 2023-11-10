using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Security.AccessControl;

namespace Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Msg> Messages { get; set; }
        public DbSet<CharacterPreference> CharacterPreferences { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<UserPack> UserPacks { get; set; }

        private readonly string _connectionString;

        public EFDbContext()
        {
            _connectionString = "User ID=postgres;Password=gogorengers;Host=mjauriziasims.ru;Port=5432;Database=sims;";
        }
        public EFDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
