using Application.Abstract;
using Application.Commands;
using Application.Queries;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ConsolePresentation
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var diContainer = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=FM_DB;Integrated Security=True");
                })
                .AddMediatR(typeof(IUnitOfWork))
                .AddScoped<IPlayerRepository, PlayerRepository>()
                .AddScoped<IFixtureRepository, FixtureRepository>()
                .AddScoped<IManagerRepository, ManagerRepository>()
                .AddScoped<ITeamRepository, TeamRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ILeagueRepository, LeagueRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();

            using (var scope = diContainer.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<DataContext>();

                db.Database.EnsureDeleted();

                db.Database.EnsureCreated();
            }

            var mediator = diContainer.GetRequiredService<IMediator>();

            var league = await mediator.Send(new CreateLeague
            {
                Name = "Amdaris League"
            });

            dynamic leagueTeams = JsonConvert.DeserializeObject<dynamic>(await GetLeagueTeams(39));

            for (int i = 0; i < leagueTeams.response.Count; i++)
            {
                var team = await mediator.Send(new CreateTeam
                {
                    Name = leagueTeams.response[i].team.name,
                    Country = leagueTeams.response[i].team.country,
                    Venue = leagueTeams.response[i].venue.name,
                    Logo = leagueTeams.response[i].team.logo,
                });

                var teamId = (int)leagueTeams.response[i].team.id;

                dynamic teamPlayers1 = JsonConvert.DeserializeObject<dynamic>(await GetTeamPlayers(teamId, 1));
                dynamic teamPlayers2 = JsonConvert.DeserializeObject<dynamic>(await GetTeamPlayers(teamId, 2));

                await AssignPlayersToTeam(teamPlayers1, team, mediator);
                await AssignPlayersToTeam(teamPlayers2, team, mediator);

                dynamic teamManager = JsonConvert.DeserializeObject<dynamic>(await GetTeamManager(teamId));

                var manager = await mediator.Send(new CreateFakeManager
                {
                    Name = teamManager.response[0].name,
                    Country = teamManager.response[0].birth.country,
                    DateOfBirth = teamManager.response[0].birth.date,
                });

                await mediator.Send(new AddManagerToTeam
                {
                    ManagerId = manager.ManagerId,
                    TeamId = team.TeamId
                });

                await mediator.Send(new AddTeamToLeague
                {
                    LeagueId = league.LeagueId,
                    TeamId = team.TeamId
                });

                Thread.Sleep(7500);
            }

            //await mediator.Send(new GenerateLeagueFixtures
            //{
            //    LeagueId = 1
            //});

            //await mediator.Send(new SimulateAllFixtures
            //{
            //    LeagueId = 1
            //});

            //var fixtures = await mediator.Send(new GetFixturesByLeague
            //{
            //    LeagueId = 1
            //});

            //fixtures.ForEach(f => Console.WriteLine($"{f.HomeTeam.Name} {f.HomeTeamScore} - {f.AwayTeamScore} {f.AwayTeam.Name}"));

            //var teams = await mediator.Send(new GetTeamsByLeague
            //{
            //    LeagueId = 1
            //});

            //Console.WriteLine("\n\n");

            //var leagueStanding = teams.OrderByDescending(t => t.CurrentSeasonStats.Points).ThenByDescending(t => t.CurrentSeasonStats.GoalsFor - t.CurrentSeasonStats.GoalsAgainst).ToList();

            //leagueStanding.ForEach(ls => Console.WriteLine($"{ls.Name} -> {ls.CurrentSeasonStats}"));

            //Console.WriteLine("\n\n");

            //var players = await mediator.Send(new GetPlayersByLeague
            //{
            //    LeagueId = 1
            //});

            //var TopGoalScorers = players.Where(p => p.PlayerRecord.Goals > 0).OrderByDescending(p => p.PlayerRecord.Goals).ToList();
            //var TopAssisters = players.Where(p => p.PlayerRecord.Assists > 0).OrderByDescending(p => p.PlayerRecord.Assists).ToList();

            //Console.WriteLine("Top Goal Scorers: \n");
            //TopGoalScorers.ForEach(p => Console.WriteLine($"{p.PlayerPerson.Name} => {p.PlayerRecord.Goals} Goals"));

            //Console.WriteLine("\n\n");
            //Console.WriteLine("Top Assisters: \n");

            //TopAssisters.ForEach(p => Console.WriteLine($"{p.PlayerPerson.Name} => {p.PlayerRecord.Assists} Assists"));
        }


        private static async Task<String> GetLeagueTeams(int leagueId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/teams?league={leagueId}&season={DateTime.Now.Year}"),
                Headers =
                        {
                            { "X-RapidAPI-Key", "5d0d6f6fb8msh738de3dc30becb2p117e42jsnc0f12f2c74c2" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        private static async Task<String> GetTeamPlayers(int teamId, int pg)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/players?team={teamId}&season=2022&page={pg}"),
                Headers =
                        {
                            { "X-RapidAPI-Key", "5d0d6f6fb8msh738de3dc30becb2p117e42jsnc0f12f2c74c2" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        private static async Task<String> GetTeamManager(int teamId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/coachs?team={teamId}"),
                Headers =
                        {
                            { "X-RapidAPI-Key", "5d0d6f6fb8msh738de3dc30becb2p117e42jsnc0f12f2c74c2" },
                            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                        },
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }

        private static async Task AssignPlayersToTeam(dynamic teamPlayers, Team team, IMediator mediator)
        {
            for (int j = 0; j < teamPlayers.response.Count; j++)
            {
                var player = await mediator.Send(new CreatePlayer
                {
                    Name = teamPlayers.response[j].player.name,
                    Country = teamPlayers.response[j].player.birth.country,
                    DateOfBirth = teamPlayers.response[j].player.birth.date,
                    Position = teamPlayers.response[j].statistics[0].games.position,
                    Image = teamPlayers.response[j].player.photo,
                });

                await mediator.Send(new AddPlayerToTeam
                {
                    PlayerId = player.PlayerId,
                    TeamId = team.TeamId
                });
            }
        } 
    }
}
