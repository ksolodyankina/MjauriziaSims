﻿using Domain.Entities;
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
        public DbSet<InheritanceLaw> InheritanceLaws { get; set; }

        private readonly string _connectionString;

        public EFDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
