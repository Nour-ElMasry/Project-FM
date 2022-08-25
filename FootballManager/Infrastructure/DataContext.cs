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
        public DbSet<Person> People { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TOPSKI\SQLEXPRESS;Database=FootballManager;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.CurrentTeam)
                .WithOne(t => t.TeamManager)
                .HasForeignKey<Team>(t => t.TeamManagerId);

            modelBuilder.Entity<Team>()
               .HasMany(t => t.Players)
               .WithOne(p => p.CurrentTeam)
               .OnDelete(DeleteBehavior.NoAction);

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

            //modelBuilder.Seed();
        }
    }
}
