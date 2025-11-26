using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MysteryCaseDomain;
using System;
using System.IO;
using System.Linq;

namespace MysteryCaseInfrastructure
{
    public class MysteryCaseDbContext : DbContext
    {
        public MysteryCaseDbContext()
        {
        }

        public MysteryCaseDbContext(DbContextOptions<MysteryCaseDbContext> options)
            : base(options)
        {
        }
        public DbSet<Case> Cases { get; set; } = null!;
        public DbSet<Clue> Clues { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Solution> Solutions { get; set; } = null!;
        public DbSet<UserCaseInteraction> UserCaseInteractions { get; set; } = null!;
        public DbSet<UserClueLog> UserClueLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Solution)
                .WithOne(s => s.Case)
                .HasForeignKey<Solution>(s => s.CaseId);

            modelBuilder.Entity<UserCaseInteraction>()
                .HasIndex(uci => new { uci.UserId, uci.CaseId })
                .IsUnique();

            modelBuilder.Entity<UserClueLog>()
                .HasIndex(ucl => new { ucl.UserId, ucl.ClueId })
                .IsUnique();

            modelBuilder.Entity<UserClueLog>()
                .HasOne(ucl => ucl.User)
                .WithMany(u => u.ClueLogs)
                .HasForeignKey(ucl => ucl.UserId);

            modelBuilder.Entity<UserClueLog>()
                .HasOne(ucl => ucl.Clue)
                .WithMany()
                .HasForeignKey(ucl => ucl.ClueId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
