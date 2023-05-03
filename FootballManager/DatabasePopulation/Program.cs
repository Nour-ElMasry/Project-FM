using Application.Abstract;
using Application.Commands;
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
                    options.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=FootballManager_Database;Integrated Security=True");
                })
                .AddMediatR(typeof(IUnitOfWork))
                .AddScoped<IPlayerRepository, PlayerRepository>()
                .AddScoped<IFixtureRepository, FixtureRepository>()
                .AddScoped<IManagerRepository, ManagerRepository>()
                .AddScoped<ITeamRepository, TeamRepository>()
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
                Name = "Premier League"
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
                    LeagueId = league.LeagueId
                });

                var teamId = (int)leagueTeams.response[i].team.id;

                dynamic teamPlayers1 = JsonConvert.DeserializeObject<dynamic>(await GetTeamPlayers(teamId, 1));
                dynamic teamPlayers2 = JsonConvert.DeserializeObject<dynamic>(await GetTeamPlayers(teamId, 2));

                await AssignPlayersToTeam(teamPlayers1, team, mediator);
                await AssignPlayersToTeam(teamPlayers2, team, mediator);

                dynamic teamManager = JsonConvert.DeserializeObject<dynamic>(await GetTeamManager(teamId));

                var managerCommand = new CreateFakeManager();

                if (teamManager.response.Count > 0)
                {
                    managerCommand = new CreateFakeManager
                    {
                        Name = teamManager.response[0].name,
                        Country = teamManager.response[0].birth.country,
                        DateOfBirth = teamManager.response[0].birth.date,
                    };
                }
                else
                {
                    managerCommand = new CreateFakeManager
                    {
                        Name = $"{team.Name}'s Manager",
                        Country = team.Country,
                        DateOfBirth = "1977-08-16",
                    };
                }
 
                var manager = await mediator.Send(managerCommand);

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

            await mediator.Send(new GenerateLeagueFixtures
            {
                LeagueId = league.LeagueId,
            });
        }


        private static async Task<String> GetLeagueTeams(int leagueId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/teams?league={leagueId}&season={DateTime.Now.Year - 1}"),
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
                RequestUri = new Uri($"https://api-football-v1.p.rapidapi.com/v3/players?team={teamId}&season={DateTime.Now.Year - 1}&page={pg}"),
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
