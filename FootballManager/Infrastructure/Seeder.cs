using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class Seeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Person>().HasData(
           //     new Person("Thomas Tuchel", "1979-05-13", "Germany") { PersonId = 9 },
           //     new Person("Kante", "1987-02-16", "France") { PersonId = 10 },
           //     new Person("Havertz", "1988-07-13", "Germany") { PersonId = 11 },
           //     new Person("Koulibaly", "1987-08-10", "Senegal") { PersonId = 12 },
           //     new Person("Mendy", "1991-04-05", "Senegal") { PersonId = 13 },
           //     new Person("Pep Guardiola", "1977-03-13", "Spain") { PersonId = 14 },
           //     new Person("Kevin De Bruyne", "1991-02-16", "Belgium") { PersonId = 15 },
           //     new Person("Gabriel Jesus", "1995-07-13", "Brazil") { PersonId = 16 },
           //     new Person("Ruben Dias", "1996-08-10", "Portugal") { PersonId = 17 },
           //     new Person("Ederson", "1992-07-13", "Brazil") { PersonId = 18 },
           //     new Person("Jurgen Klopp", "1971-02-13", "German") { PersonId = 19 },
           //     new Person("Thiago", "1988-02-16", "Spain") { PersonId = 20 },
           //     new Person("Mohamed Salah", "1994-07-13", "Egypt") { PersonId = 21 },
           //     new Person("Van Dijk", "1994-08-10", "Netherlands") { PersonId = 22 },
           //     new Person("Alisson", "1996-07-13", "Brazil") { PersonId = 23 }
           //     );


           // modelBuilder.Entity<Season>().HasData(
           //     new Season(2022) { SeasonId = 1 }
           //     );

           // modelBuilder.Entity<League>().HasData(
           //     new League { LeagueId = 1, Name = "Amdaris League", CurrentSeasonId = 1 }
           //     );

           // modelBuilder.Entity<BalancedTactic>().HasData(
           //     new BalancedTactic { TacticId = 1 },
           //     new BalancedTactic { TacticId = 2 },
           //     new BalancedTactic { TacticId = 3 }
           //     );

           // modelBuilder.Entity<TeamSheet>().HasData(
           //     new TeamSheet { TeamSheetId = 1, TeamSheetTeamId = 1, TeamTacticId = 1 },
           //     new TeamSheet { TeamSheetId = 2, TeamSheetTeamId = 2, TeamTacticId = 2 },
           //     new TeamSheet { TeamSheetId = 3, TeamSheetTeamId = 3, TeamTacticId = 3 }
           //     );

           // modelBuilder.Entity<SeasonStats>().HasData(
           //    new SeasonStats { SeasonStatsId = 1 },
           //    new SeasonStats { SeasonStatsId = 2 },
           //    new SeasonStats { SeasonStatsId = 3 }
           //    );

           // modelBuilder.Entity<FakeManager>().HasData(
           //     new FakeManager(9) { ManagerId = 1 },
           //     new FakeManager(14) { ManagerId = 2 },
           //     new FakeManager(19) { ManagerId = 3 }
           //     );


           // modelBuilder.Entity<Team>().HasData(
           //     new Team
           //     {
           //         TeamId = 1,
           //         Name = "Chelsea",
           //         Country = "England",
           //         Venue = "Stamford Bridge",
           //         TeamManagerId = 1,
           //         CurrentTeamSheetId = 1,
           //         CurrentSeasonStatsId = 1,
           //         CurrentLeagueId = 1
           //     },
           //     new Team
           //     {
           //         TeamId = 2,
           //         Name = "Manchester City",
           //         Country = "England",
           //         Venue = "Etihad stadium",
           //         TeamManagerId = 2,
           //         CurrentTeamSheetId = 2,
           //         CurrentSeasonStatsId = 2,
           //         CurrentLeagueId = 1
           //     },
           //     new Team
           //     {
           //         TeamId = 3,
           //         Name = "Liverpool",
           //         Country = "England",
           //         Venue = "Anfield Stadium",
           //         TeamManagerId = 3,
           //         CurrentTeamSheetId = 3,
           //         CurrentSeasonStatsId = 3,
           //         CurrentLeagueId = 1
           //     }
           //     );

           // modelBuilder.Entity<Record>().HasData(
           //     new Record() { RecordId = 1 },
           //     new Record() { RecordId = 2 },
           //     new Record() { RecordId = 3 },
           //     new Record() { RecordId = 4 },
           //     new Record() { RecordId = 5 },
           //     new Record() { RecordId = 6 },
           //     new Record() { RecordId = 7 },
           //     new Record() { RecordId = 8 },
           //     new Record() { RecordId = 9 },
           //     new Record() { RecordId = 10 },
           //     new Record() { RecordId = 11 },
           //     new Record() { RecordId = 12 }
           //     );

           // modelBuilder.Entity<MidfieldStats>().HasData(
           //     new MidfieldStats { PlayerStatsId = 1, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //     new MidfieldStats { PlayerStatsId = 2, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //     new MidfieldStats { PlayerStatsId = 3, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 }
           // );

           // modelBuilder.Entity<Midfielder>().HasData(
           //      new Midfielder()
           //      {
           //          PlayerId = 1,
           //          Position = "CDM",
           //          PlayerPersonId = 10,
           //          CurrentTeamId = 1,
           //          PlayerStatsId = 1,
           //          PlayerRecordId = 1
           //      },
           //      new Midfielder
           //      {
           //          PlayerId = 2,
           //          Position = "CAM",
           //          PlayerPersonId = 15,
           //          CurrentTeamId = 2,
           //          PlayerStatsId = 2,
           //          PlayerRecordId = 2
           //      },
           //      new Midfielder(10, "CDM")
           //      {
           //          PlayerId = 3,
           //          Position = "CM",
           //          PlayerPersonId = 20,
           //          CurrentTeamId = 3,
           //          PlayerStatsId = 3,
           //          PlayerRecordId = 3
           //      }
           //     );

           // modelBuilder.Entity<AttackingStats>().HasData(
           //     new AttackingStats { PlayerStatsId = 4, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //     new AttackingStats { PlayerStatsId = 5, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //     new AttackingStats { PlayerStatsId = 6, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 }
           // );

           // modelBuilder.Entity<Attacker>().HasData(
           //     new Attacker
           //     {
           //         PlayerId = 4,
           //         Position = "ST",
           //         PlayerPersonId = 11,
           //         CurrentTeamId = 1,
           //         PlayerStatsId = 4,
           //         PlayerRecordId = 4
           //     },
           //      new Attacker
           //      {
           //          PlayerId = 5,
           //          Position = "ST",
           //          PlayerPersonId = 16,
           //          CurrentTeamId = 2,
           //          PlayerStatsId = 5,
           //          PlayerRecordId = 5
           //      },
           //       new Attacker
           //       {
           //           PlayerId = 6,
           //           Position = "RW",
           //           PlayerPersonId = 21,
           //           CurrentTeamId = 3,
           //           PlayerStatsId = 6,
           //           PlayerRecordId = 6
           //       }
           //     );

           // modelBuilder.Entity<DefendingStats>().HasData(
           //    new DefendingStats { PlayerStatsId = 7, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //    new DefendingStats { PlayerStatsId = 8, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //    new DefendingStats { PlayerStatsId = 9, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 }
           //);

           // modelBuilder.Entity<Defender>().HasData(
           //     new Defender
           //     {
           //         PlayerId = 7,
           //         Position = "CB",
           //         PlayerPersonId = 12,
           //         CurrentTeamId = 1,
           //         PlayerStatsId = 7,
           //         PlayerRecordId = 7
           //     },
           //      new Defender
           //      {
           //          PlayerId = 8,
           //          Position = "CB",
           //          PlayerPersonId = 17,
           //          CurrentTeamId = 2,
           //          PlayerStatsId = 8,
           //          PlayerRecordId = 8
           //      },
           //       new Defender
           //       {
           //           PlayerId = 9,
           //           Position = "CB",
           //           PlayerPersonId = 22,
           //           CurrentTeamId = 3,
           //           PlayerStatsId = 9,
           //           PlayerRecordId = 9
           //       }
           //     );

           // modelBuilder.Entity<GoalkeepingStats>().HasData(
           //    new GoalkeepingStats { PlayerStatsId = 10, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //    new GoalkeepingStats { PlayerStatsId = 11, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 },
           //    new GoalkeepingStats { PlayerStatsId = 12, Attacking = 50, PlayMaking = 50, Defending = 50, Goalkeeping = 50 }
           //);

           // modelBuilder.Entity<Goalkeeper>().HasData(
           //       new Goalkeeper
           //       {
           //           PlayerId = 10,
           //           Position = "GK",
           //           PlayerPersonId = 13,
           //           CurrentTeamId = 1,
           //           PlayerStatsId = 10,
           //           PlayerRecordId = 10
           //       },
           //       new Goalkeeper
           //       {
           //           PlayerId = 11,
           //           Position = "GK",
           //           PlayerPersonId = 18,
           //           CurrentTeamId = 2,
           //           PlayerStatsId = 11,
           //           PlayerRecordId = 11
           //       },
           //       new Goalkeeper
           //       {
           //           PlayerId = 12,
           //           Position = "GK",
           //           PlayerPersonId = 23,
           //           CurrentTeamId = 3,
           //           PlayerStatsId = 12,
           //           PlayerRecordId = 12
           //       }
           //     );
        }
    }
}