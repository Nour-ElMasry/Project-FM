using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Career> Careers { get; set; }
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
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Fixture>()
                .HasMany(f => f.FixtureEvents)
                .WithOne(e => e.EventFixture)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fixture>()
                .HasOne(f => f.FixtureScore)
                .WithOne()
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
