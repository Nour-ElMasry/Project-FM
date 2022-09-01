using Application.Abstract;
using Application.Commands;
using Application.Queries;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsolePresentation
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var diContainer = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=FootballManager;Integrated Security=True");
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


            var mediator = diContainer.GetRequiredService<IMediator>();

            //var person = await mediator.Send(new CreatePerson
            //{ 
            //    Name = "Someone",
            //    Country = "Somewhere",
            //    BirthDate = "1960-01-01"
            //});

            //var manager = await mediator.Send(new CreateFakeManager
            //{
            //    ManagerPerson = person
            //});

            //var team = await mediator.Send(new CreateTeam
            //{
            //    Name = "A7a FC",
            //    Country = "Egypt",
            //    Venue = "A7a 3ala dema8ak",
            //});

            //await mediator.Send(new AddManagerToTeam
            //{
            //    ManagerId = manager.ManagerId,
            //    TeamId = team.TeamId
            //});

            //await AddPlayersToTeam(team.TeamId, mediator);

            //await mediator.Send(new AddTeamToLeague
            //{
            //    LeagueId = 1,
            //    TeamId = team.TeamId,
            //});


            var flag = true;

            var flag1 = false;

            long userId = 0;

            while (flag)
            {
                Console.WriteLine("1 - Log In");
                Console.WriteLine("2- Register user");
                Console.WriteLine("3- Exit");

                var input = Console.ReadKey().KeyChar.ToString();

                switch (input)
                {
                    case "1":
                        Console.Write("\nUserName: ");
                        var username = Console.ReadLine();
                        Console.Write("Password: ");
                        var password = Console.ReadLine();

                        var authUser = await mediator.Send(new AuthUser
                        {
                            UserName = username,
                            Password = password
                        });

                        flag = authUser != null;

                        if (flag == false)
                            Console.Write("\n--------Wrong credentials!--------\n");
                        else
                        {
                            Console.Write("\n--------Successfully logged in!--------\n");
                            flag1 = true;
                            flag = false;
                            userId = authUser.UserId;
                        }
                        break;

                    case "2":
                        Console.Write("\nName: ");
                        var nameRegister = Console.ReadLine();
                        Console.Write("Country: ");
                        var countryRegister = Console.ReadLine();
                        Console.Write("DateOfBirth: ");
                        var dateOfBirthRegister = Console.ReadLine();
                        Console.Write("\nUserName: ");
                        var usernameRegister = Console.ReadLine();
                        Console.Write("Password: ");
                        var passwordRegister = Console.ReadLine();

                        var newUser = await mediator.Send(new CreateUser
                        {
                            Username = usernameRegister,
                            Password = passwordRegister,
                            Name = nameRegister,
                            Country = countryRegister,
                            DateOfBirth = dateOfBirthRegister
                        });

                        flag = newUser != null;

                        if (flag == false)
                            Console.Write("\n--------Username Already exists!--------\n");
                        else
                        {
                            Console.Write("\n--------Successfully registered!--------\n");
                            flag1 = true;
                            flag = false;
                            userId = newUser.UserId;
                        }
                        break;

                    case "3":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Not a command!!");
                        break;
                }
            }

            while (flag1)
            {
                var league = await mediator.Send(new GetLeagueById
                {
                    LeagueId = 1
                });

                var user = await mediator.Send(new GetUserById
                {
                    UserId = userId,
                });

                Console.WriteLine("\n1 - Create Team");
                Console.WriteLine("2 - View League Fixtures");
                Console.WriteLine("3 - View League Standings");
                Console.WriteLine("4 - Simulate Fixtures");
                Console.WriteLine("5 - Exit\n");

                var input = Console.ReadKey().KeyChar.ToString();

                switch (input)
                {
                    case "1":
                        Console.Write("\nTeam Name:");
                        var teamName = Console.ReadLine();
                        Console.Write("Country: ");
                        var teamCountry = Console.ReadLine();
                        Console.Write("Venue: ");
                        var teamVenue = Console.ReadLine();

                        var manager = await mediator.Send(new CreateRealManager
                        {
                            UserManagerId = user.UserId
                        });

                        var team = await mediator.Send(new CreateTeam
                        {
                            Name = teamName,
                            Country = teamCountry,
                            Venue = teamVenue,
                        });

                        await mediator.Send(new AddManagerToTeam
                        {
                            ManagerId = manager.ManagerId,
                            TeamId = team.TeamId
                        });

                        await AddPlayersToTeam(team.TeamId, mediator);

                        await mediator.Send(new AddTeamToLeague
                        {
                            LeagueId = league.LeagueId,
                            TeamId = team.TeamId,
                        });

                        break;

                    case "2":
                        Console.WriteLine($"\n{league.Name}'s Fixtues\n");
                        await mediator.Send(new GenerateLeagueFixtures
                        {
                            LeagueId = league.LeagueId
                        });

                        var fixtures = await mediator.Send(new GetFixturesByLeague
                        {
                            LeagueId = league.LeagueId
                        });

                        fixtures
                            .ForEach(f => Console.WriteLine($"{f.Teams[0].Name} vs {f.Teams[1].Name}"));
                        break;
                    case "3":
                        Console.WriteLine($"\n{league.Name}'s Standings\n");
                        var teams = await mediator.Send(new GetTeamsByLeague
                        {
                            LeagueId = league.LeagueId
                        });

                        teams
                            .OrderByDescending(t => t.CurrentSeasonStats.Points)
                            .ThenByDescending(t => t.CurrentSeasonStats.GoalsFor - t.CurrentSeasonStats.GoalsAgainst)
                            .ToList()
                            .ForEach(t => Console.WriteLine($"{t.Name} => \n{t.CurrentSeasonStats}"));
                        break;
                    case "4":
                        Console.WriteLine($"\n{league.Name}'s Fixtue results\n");
                        await mediator.Send(new SimulateAllFixtures
                        {
                            LeagueId = league.LeagueId
                        });

                        var Simfixtures = await mediator.Send(new GetFixturesByLeague
                        {
                            LeagueId = league.LeagueId
                        });

                        Simfixtures
                            .ForEach(f => Console.WriteLine($"{f.Teams[0].Name} {f.HomeTeamScore} - {f.AwayTeamScore} {f.Teams[1].Name}"));
                        break;

                    case "5":
                        flag1 = false;
                        break;
                }
            }
        }

        public static async Task AddPlayersToTeam(long teamId, IMediator mediator)
        {
            await AddAttackerToCreatedTeam(mediator, teamId, "Cristiano Ronaldo", "Portugal", "1985-02-05", "ST");
            await AddMidfielderToCreatedTeam(mediator, teamId, "Kevin De Bruyne", "Belgium", "1991-06-28", "CAM");
            await AddDefenderToCreatedTeam(mediator, teamId, "Sergio Ramos", "Spain", "1986-03-30", "CB");
            await AddGoalkeeperToCreatedTeam(mediator, teamId, "Neuer", "Germany", "1986-03-27", "GK");
        }

        private static async Task AddMidfielderToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var player = await mediator.Send(new CreatePlayer
            {
                Position = position,
                Name = name,
                DateOfBirth = dateOfBirth,
                Country = country,
                PlayerRole = "Midfielder"
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddAttackerToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var player = await mediator.Send(new CreatePlayer
            {
                Position = position,
                Name = name,
                DateOfBirth = dateOfBirth,
                Country = country,
                PlayerRole = "Attacker"
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddDefenderToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var player = await mediator.Send(new CreatePlayer
            {
                Position = position,
                Name = name,
                DateOfBirth = dateOfBirth,
                Country = country,
                PlayerRole = "Defender"
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddGoalkeeperToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var player = await mediator.Send(new CreatePlayer
            {
                Name = name,
                DateOfBirth = dateOfBirth,
                Country = country,
                Position = position,
                PlayerRole = "Goalkeeper"
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }
    }
}
