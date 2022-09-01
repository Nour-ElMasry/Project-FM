﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220901192758_removedPeopleRepo")]
    partial class removedPeopleRepo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Fixture", b =>
                {
                    b.Property<long>("FixtureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("FixtureId"), 1L, 1);

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("FixtureLeagueID")
                        .HasColumnType("bigint");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FixtureId");

                    b.HasIndex("FixtureLeagueID");

                    b.ToTable("Fixtures");
                });

            modelBuilder.Entity("Domain.Entities.League", b =>
                {
                    b.Property<long>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LeagueId"), 1L, 1);

                    b.Property<long?>("CurrentSeasonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeagueId");

                    b.HasIndex("CurrentSeasonId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("Domain.Entities.Manager", b =>
                {
                    b.Property<long>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ManagerId"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ManagerPersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("ManagerId");

                    b.HasIndex("ManagerPersonId");

                    b.HasIndex("TeamId")
                        .IsUnique()
                        .HasFilter("[TeamId] IS NOT NULL");

                    b.ToTable("Managers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Manager");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<long>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PersonId"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("Domain.Entities.Player", b =>
                {
                    b.Property<long>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PlayerId"), 1L, 1);

                    b.Property<long?>("CurrentTeamId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PlayerPersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PlayerRecordId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PlayerStatsId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("CurrentTeamId");

                    b.HasIndex("PlayerPersonId");

                    b.HasIndex("PlayerRecordId");

                    b.HasIndex("PlayerStatsId");

                    b.ToTable("Players");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Player");
                });

            modelBuilder.Entity("Domain.Entities.PlayerStats", b =>
                {
                    b.Property<long>("PlayerStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PlayerStatsId"), 1L, 1);

                    b.Property<int>("Attacking")
                        .HasColumnType("int");

                    b.Property<int>("Defending")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Goalkeeping")
                        .HasColumnType("int");

                    b.Property<int>("PlayMaking")
                        .HasColumnType("int");

                    b.HasKey("PlayerStatsId");

                    b.ToTable("PlayerStats");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PlayerStats");
                });

            modelBuilder.Entity("Domain.Entities.Record", b =>
                {
                    b.Property<long>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RecordId"), 1L, 1);

                    b.Property<int>("Assists")
                        .HasColumnType("int");

                    b.Property<int>("CleanSheets")
                        .HasColumnType("int");

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("Goals")
                        .HasColumnType("int");

                    b.HasKey("RecordId");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("Domain.Entities.Season", b =>
                {
                    b.Property<long>("SeasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SeasonId"), 1L, 1);

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("SeasonId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("Domain.Entities.SeasonStats", b =>
                {
                    b.Property<long>("SeasonStatsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SeasonStatsId"), 1L, 1);

                    b.Property<int>("AwayGamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("GamesDrawn")
                        .HasColumnType("int");

                    b.Property<int>("GamesLost")
                        .HasColumnType("int");

                    b.Property<int>("GamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("GamesWon")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAgainst")
                        .HasColumnType("int");

                    b.Property<int>("GoalsFor")
                        .HasColumnType("int");

                    b.Property<int>("HomeGamesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("SeasonStatsId");

                    b.ToTable("SeasonStats");
                });

            modelBuilder.Entity("Domain.Entities.Tactic", b =>
                {
                    b.Property<long>("TacticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TacticId"), 1L, 1);

                    b.Property<int>("AttackingWeight")
                        .HasColumnType("int");

                    b.Property<int>("DefendingWeight")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TacticId");

                    b.ToTable("Tactic");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tactic");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Property<long>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TeamId"), 1L, 1);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CurrentLeagueId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrentSeasonStatsId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrentTeamSheetId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.HasIndex("CurrentLeagueId");

                    b.HasIndex("CurrentSeasonStatsId");

                    b.HasIndex("CurrentTeamSheetId");

                    b.HasIndex("ManagerId")
                        .IsUnique()
                        .HasFilter("[ManagerId] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Entities.TeamSheet", b =>
                {
                    b.Property<long>("TeamSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("TeamSheetId"), 1L, 1);

                    b.Property<int>("AttackingRating")
                        .HasColumnType("int");

                    b.Property<int>("DefendingRating")
                        .HasColumnType("int");

                    b.Property<long?>("TeamTacticId")
                        .HasColumnType("bigint");

                    b.HasKey("TeamSheetId");

                    b.HasIndex("TeamTacticId");

                    b.ToTable("TeamSheet");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"), 1L, 1);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserPersonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserPersonId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FixtureTeam", b =>
                {
                    b.Property<long>("FixturesFixtureId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamsTeamId")
                        .HasColumnType("bigint");

                    b.HasKey("FixturesFixtureId", "TeamsTeamId");

                    b.HasIndex("TeamsTeamId");

                    b.ToTable("FixtureTeam");
                });

            modelBuilder.Entity("Domain.Entities.Attacker", b =>
                {
                    b.HasBaseType("Domain.Entities.Player");

                    b.HasDiscriminator().HasValue("Attacker");
                });

            modelBuilder.Entity("Domain.Entities.AttackingStats", b =>
                {
                    b.HasBaseType("Domain.Entities.PlayerStats");

                    b.HasDiscriminator().HasValue("AttackingStats");
                });

            modelBuilder.Entity("Domain.Entities.AttackingTactic", b =>
                {
                    b.HasBaseType("Domain.Entities.Tactic");

                    b.HasDiscriminator().HasValue("AttackingTactic");
                });

            modelBuilder.Entity("Domain.Entities.BalancedTactic", b =>
                {
                    b.HasBaseType("Domain.Entities.Tactic");

                    b.HasDiscriminator().HasValue("BalancedTactic");
                });

            modelBuilder.Entity("Domain.Entities.Defender", b =>
                {
                    b.HasBaseType("Domain.Entities.Player");

                    b.HasDiscriminator().HasValue("Defender");
                });

            modelBuilder.Entity("Domain.Entities.DefendingStats", b =>
                {
                    b.HasBaseType("Domain.Entities.PlayerStats");

                    b.HasDiscriminator().HasValue("DefendingStats");
                });

            modelBuilder.Entity("Domain.Entities.DefendingTactic", b =>
                {
                    b.HasBaseType("Domain.Entities.Tactic");

                    b.HasDiscriminator().HasValue("DefendingTactic");
                });

            modelBuilder.Entity("Domain.Entities.FakeManager", b =>
                {
                    b.HasBaseType("Domain.Entities.Manager");

                    b.HasDiscriminator().HasValue("FakeManager");
                });

            modelBuilder.Entity("Domain.Entities.Goalkeeper", b =>
                {
                    b.HasBaseType("Domain.Entities.Player");

                    b.HasDiscriminator().HasValue("Goalkeeper");
                });

            modelBuilder.Entity("Domain.Entities.GoalkeepingStats", b =>
                {
                    b.HasBaseType("Domain.Entities.PlayerStats");

                    b.HasDiscriminator().HasValue("GoalkeepingStats");
                });

            modelBuilder.Entity("Domain.Entities.Midfielder", b =>
                {
                    b.HasBaseType("Domain.Entities.Player");

                    b.HasDiscriminator().HasValue("Midfielder");
                });

            modelBuilder.Entity("Domain.Entities.MidfieldStats", b =>
                {
                    b.HasBaseType("Domain.Entities.PlayerStats");

                    b.HasDiscriminator().HasValue("MidfieldStats");
                });

            modelBuilder.Entity("Domain.Entities.RealManager", b =>
                {
                    b.HasBaseType("Domain.Entities.Manager");

                    b.Property<long?>("UserManagerId")
                        .HasColumnType("bigint");

                    b.HasIndex("UserManagerId");

                    b.HasDiscriminator().HasValue("RealManager");
                });

            modelBuilder.Entity("Domain.Entities.Fixture", b =>
                {
                    b.HasOne("Domain.Entities.League", "FixtureLeague")
                        .WithMany("Fixtures")
                        .HasForeignKey("FixtureLeagueID");

                    b.Navigation("FixtureLeague");
                });

            modelBuilder.Entity("Domain.Entities.League", b =>
                {
                    b.HasOne("Domain.Entities.Season", "CurrentSeason")
                        .WithMany()
                        .HasForeignKey("CurrentSeasonId");

                    b.Navigation("CurrentSeason");
                });

            modelBuilder.Entity("Domain.Entities.Manager", b =>
                {
                    b.HasOne("Domain.Entities.Person", "ManagerPerson")
                        .WithMany()
                        .HasForeignKey("ManagerPersonId");

                    b.HasOne("Domain.Entities.Team", null)
                        .WithOne("TeamManager")
                        .HasForeignKey("Domain.Entities.Manager", "TeamId");

                    b.Navigation("ManagerPerson");
                });

            modelBuilder.Entity("Domain.Entities.Player", b =>
                {
                    b.HasOne("Domain.Entities.Team", "CurrentTeam")
                        .WithMany("Players")
                        .HasForeignKey("CurrentTeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Person", "PlayerPerson")
                        .WithMany()
                        .HasForeignKey("PlayerPersonId");

                    b.HasOne("Domain.Entities.Record", "PlayerRecord")
                        .WithMany()
                        .HasForeignKey("PlayerRecordId");

                    b.HasOne("Domain.Entities.PlayerStats", "PlayerStats")
                        .WithMany()
                        .HasForeignKey("PlayerStatsId");

                    b.Navigation("CurrentTeam");

                    b.Navigation("PlayerPerson");

                    b.Navigation("PlayerRecord");

                    b.Navigation("PlayerStats");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.HasOne("Domain.Entities.League", "CurrentLeague")
                        .WithMany("Teams")
                        .HasForeignKey("CurrentLeagueId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.SeasonStats", "CurrentSeasonStats")
                        .WithMany()
                        .HasForeignKey("CurrentSeasonStatsId");

                    b.HasOne("Domain.Entities.TeamSheet", "CurrentTeamSheet")
                        .WithMany()
                        .HasForeignKey("CurrentTeamSheetId");

                    b.HasOne("Domain.Entities.Manager", null)
                        .WithOne("CurrentTeam")
                        .HasForeignKey("Domain.Entities.Team", "ManagerId");

                    b.Navigation("CurrentLeague");

                    b.Navigation("CurrentSeasonStats");

                    b.Navigation("CurrentTeamSheet");
                });

            modelBuilder.Entity("Domain.Entities.TeamSheet", b =>
                {
                    b.HasOne("Domain.Entities.Tactic", "TeamTactic")
                        .WithMany()
                        .HasForeignKey("TeamTacticId");

                    b.Navigation("TeamTactic");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Person", "UserPerson")
                        .WithMany()
                        .HasForeignKey("UserPersonId");

                    b.Navigation("UserPerson");
                });

            modelBuilder.Entity("FixtureTeam", b =>
                {
                    b.HasOne("Domain.Entities.Fixture", null)
                        .WithMany()
                        .HasForeignKey("FixturesFixtureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.RealManager", b =>
                {
                    b.HasOne("Domain.Entities.User", "UserManager")
                        .WithMany()
                        .HasForeignKey("UserManagerId");

                    b.Navigation("UserManager");
                });

            modelBuilder.Entity("Domain.Entities.League", b =>
                {
                    b.Navigation("Fixtures");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Domain.Entities.Manager", b =>
                {
                    b.Navigation("CurrentTeam");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("TeamManager");
                });
#pragma warning restore 612, 618
        }
    }
}
