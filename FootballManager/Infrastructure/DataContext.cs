using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=FootballManager_Database;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
               .HasMany(t => t.Players)
               .WithOne(p => p.CurrentTeam)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeFixtures)
                .WithOne(f => f.HomeTeam);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayFixtures)
                .WithOne(g => g.AwayTeam)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.TeamManager)
                .WithOne()
                .HasForeignKey<Manager>("CurrentTeamId")
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Manager>()
                .HasOne(m => m.CurrentTeam)
                .WithOne()
                .HasForeignKey<Team>("TeamManagerId")
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RealManager>()
                .HasOne(m => m.UserManager)
                .WithOne()
                .HasForeignKey<User>("UserManagerId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fixture>()
                .HasMany(f => f.FixtureEvents)
                .WithOne(e => e.EventFixture)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.CurrentLeague)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealManager>();
            modelBuilder.Entity<FakeManager>();

            modelBuilder.Entity<TeamSheet>();

            modelBuilder.Entity<Attacker>();
            modelBuilder.Entity<Midfielder>();
            modelBuilder.Entity<Defender>();
            modelBuilder.Entity<Goalkeeper>();

            modelBuilder.Entity<AttackingStats>();
            modelBuilder.Entity<MidfieldStats>();
            modelBuilder.Entity<DefendingStats>();
            modelBuilder.Entity<GoalkeepingStats>();

            modelBuilder.Entity<AttackingTactic>();
            modelBuilder.Entity<BalancedTactic>();
            modelBuilder.Entity<DefendingTactic>();
        }
    }
}
