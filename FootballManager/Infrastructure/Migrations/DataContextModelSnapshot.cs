﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Property<long>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EventId"), 1L, 1);

                    b.Property<long?>("EventFixtureId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GoalAssisterId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GoalScorerId")
                        .HasColumnType("bigint");

                    b.HasKey("EventId");

                    b.HasIndex("EventFixtureId");

                    b.HasIndex("GoalAssisterId");

                    b.HasIndex("GoalScorerId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Domain.Entities.Fixture", b =>
                {
                    b.Property<long>("FixtureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("FixtureId"), 1L, 1);

                    b.Property<long?>("AwayTeamId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("FixtureLeagueId")
                        .HasColumnType("bigint");

                    b.Property<long?>("FixtureScoreId")
                        .HasColumnType("bigint");

                    b.Property<long?>("HomeTeamId")
                        .HasColumnType("bigint");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPlayed")
                        .HasColumnType("bit");

                    b.HasKey("FixtureId");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("FixtureLeagueId");

                    b.HasIndex("FixtureScoreId")
                        .IsUnique()
                        .HasFilter("[FixtureScoreId] IS NOT NULL");

                    b.HasIndex("HomeTeamId");

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

                    b.Property<long?>("CurrentTeamId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ManagerPersonId")
                        .HasColumnType("bigint");

                    b.HasKey("ManagerId");

                    b.HasIndex("CurrentTeamId")
                        .IsUnique()
                        .HasFilter("[CurrentTeamId] IS NOT NULL");

                    b.HasIndex("ManagerPersonId");

                    b.ToTable("Managers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Manager");
                });

            modelBuilder.Entity("Domain.Entities.Person", b =>
                {
                    b.Property<long>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PersonId"), 1L, 1);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
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

                    b.Property<long?>("CurrentPlayerStatsId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrentTeamId")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("PlayerPersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PlayerRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("CurrentPlayerStatsId");

                    b.HasIndex("CurrentTeamId");

                    b.HasIndex("PlayerPersonId");

                    b.HasIndex("PlayerRecordId");

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

                    b.Property<int>("OverallRating")
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

            modelBuilder.Entity("Domain.Entities.Score", b =>
                {
                    b.Property<long>("ScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ScoreId"), 1L, 1);

                    b.Property<int>("AwayScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeScore")
                        .HasColumnType("int");

                    b.HasKey("ScoreId");

                    b.ToTable("Score");
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

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("TeamManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.HasIndex("CurrentLeagueId");

                    b.HasIndex("CurrentSeasonStatsId");

                    b.HasIndex("CurrentTeamSheetId");

                    b.HasIndex("TeamManagerId")
                        .IsUnique()
                        .HasFilter("[TeamManagerId] IS NOT NULL");

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
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<long?>("UserPersonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserPersonId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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

                    b.Property<string>("UserManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("UserManagerId")
                        .IsUnique()
                        .HasFilter("[UserManagerId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("RealManager");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.HasOne("Domain.Entities.Fixture", "EventFixture")
                        .WithMany("FixtureEvents")
                        .HasForeignKey("EventFixtureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Player", "GoalAssister")
                        .WithMany()
                        .HasForeignKey("GoalAssisterId");

                    b.HasOne("Domain.Entities.Player", "GoalScorer")
                        .WithMany()
                        .HasForeignKey("GoalScorerId");

                    b.Navigation("EventFixture");

                    b.Navigation("GoalAssister");

                    b.Navigation("GoalScorer");
                });

            modelBuilder.Entity("Domain.Entities.Fixture", b =>
                {
                    b.HasOne("Domain.Entities.Team", "AwayTeam")
                        .WithMany("AwayFixtures")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.League", "FixtureLeague")
                        .WithMany("Fixtures")
                        .HasForeignKey("FixtureLeagueId");

                    b.HasOne("Domain.Entities.Score", "FixtureScore")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Fixture", "FixtureScoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Entities.Team", "HomeTeam")
                        .WithMany("HomeFixtures")
                        .HasForeignKey("HomeTeamId");

                    b.Navigation("AwayTeam");

                    b.Navigation("FixtureLeague");

                    b.Navigation("FixtureScore");

                    b.Navigation("HomeTeam");
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
                    b.HasOne("Domain.Entities.Team", null)
                        .WithOne("TeamManager")
                        .HasForeignKey("Domain.Entities.Manager", "CurrentTeamId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Domain.Entities.Person", "ManagerPerson")
                        .WithMany()
                        .HasForeignKey("ManagerPersonId");

                    b.Navigation("ManagerPerson");
                });

            modelBuilder.Entity("Domain.Entities.Player", b =>
                {
                    b.HasOne("Domain.Entities.PlayerStats", "CurrentPlayerStats")
                        .WithMany()
                        .HasForeignKey("CurrentPlayerStatsId");

                    b.HasOne("Domain.Entities.Team", "CurrentTeam")
                        .WithMany("Players")
                        .HasForeignKey("CurrentTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Person", "PlayerPerson")
                        .WithMany()
                        .HasForeignKey("PlayerPersonId");

                    b.HasOne("Domain.Entities.Record", "PlayerRecord")
                        .WithMany()
                        .HasForeignKey("PlayerRecordId");

                    b.Navigation("CurrentPlayerStats");

                    b.Navigation("CurrentTeam");

                    b.Navigation("PlayerPerson");

                    b.Navigation("PlayerRecord");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.HasOne("Domain.Entities.League", "CurrentLeague")
                        .WithMany("Teams")
                        .HasForeignKey("CurrentLeagueId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.SeasonStats", "CurrentSeasonStats")
                        .WithMany()
                        .HasForeignKey("CurrentSeasonStatsId");

                    b.HasOne("Domain.Entities.TeamSheet", "CurrentTeamSheet")
                        .WithMany()
                        .HasForeignKey("CurrentTeamSheetId");

                    b.HasOne("Domain.Entities.Manager", null)
                        .WithOne("CurrentTeam")
                        .HasForeignKey("Domain.Entities.Team", "TeamManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.RealManager", b =>
                {
                    b.HasOne("Domain.Entities.User", "UserManager")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.RealManager", "UserManagerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("UserManager");
                });

            modelBuilder.Entity("Domain.Entities.Fixture", b =>
                {
                    b.Navigation("FixtureEvents");
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
                    b.Navigation("AwayFixtures");

                    b.Navigation("HomeFixtures");

                    b.Navigation("Players");

                    b.Navigation("TeamManager");
                });
#pragma warning restore 612, 618
        }
    }
}
