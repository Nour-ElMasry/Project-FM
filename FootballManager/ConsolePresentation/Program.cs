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
                .AddScoped<IPeopleRepository, PeopleRepository>()
                .AddScoped<IManagerRepository, ManagerRepository>()
                .AddScoped<ITeamRepository, TeamRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ILeagueRepository, LeagueRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .BuildServiceProvider();


            var mediator = diContainer.GetRequiredService<IMediator>();

            var person1 = await mediator.Send(new CreatePerson
            {
                Name = "Ancheloti",
                BirthDate = "1959-06-10",
                Country = "Italy"
            });

            var person2 = await mediator.Send(new CreatePerson
            {
                Name = "Xavi",
                BirthDate = "1980-01-25",
                Country = "Spain"
            });

            var manager1 = await mediator.Send(new CreateFakeManager
            {
                ManagerPersonId = person1.PersonId
            });

            var manager2 = await mediator.Send(new CreateFakeManager
            {
                ManagerPersonId = person2.PersonId
            });

            var team1 = await mediator.Send(new CreateTeam
            {
                Name = "Real Madrid",
                Country = "Spain",
                Venue = "Santiago Bernabeu",
                TeamManagerId = manager1.ManagerId
            });

            var team2 = await mediator.Send(new CreateTeam
            {
                Name = "Barcelona",
                Country = "Spain",
                Venue = "Spotify Camp Nou",
                TeamManagerId = manager2.ManagerId
            });

            await AddPlayersToTeam(team1.TeamId, mediator, 1);
            await AddPlayersToTeam(team2.TeamId, mediator, 2);

            var league = await mediator.Send(new CreateLeague
            {
                Name = "Amdaris League"
            });

            await mediator.Send(new AddTeamToLeague
            {
                LeagueId = league.LeagueId,
                TeamId = team1.TeamId
            });

            await mediator.Send(new AddTeamToLeague
            {
                LeagueId = league.LeagueId,
                TeamId = team2.TeamId
            });

            await mediator.Send(new GenerateLeagueFixtures
            {
                LeagueId = league.LeagueId,
            });

            var fixtures = await mediator.Send(new GetFixturesByLeague
            {
                LeagueId = league.LeagueId
            });

            fixtures.ForEach(f => Console.WriteLine($"{f.Teams[0].Name} vs {f.Teams[1].Name}"));

            await mediator.Send(new SimulateAllFixtures
            {
                LeagueId = league.LeagueId
            });

            var fixturesSimulated = await mediator.Send(new GetFixturesByLeague
            {
                LeagueId = league.LeagueId
            });

            //var flag = true;

            //var flag1 = false;

            //long userId = 0;

            //while (flag)
            //{
            //    Console.WriteLine("1 - Log In");
            //    Console.WriteLine("2- Register user");
            //    Console.WriteLine("3- Exit");

            //    var input = Console.ReadKey().KeyChar.ToString();

            //    switch (input)
            //    {
            //        case "1":
            //            Console.Write("\nUserName: ");
            //            var username = Console.ReadLine();
            //            Console.Write("Password: ");
            //            var password = Console.ReadLine();

            //            var authUser = await mediator.Send(new AuthUser
            //            {
            //                UserName = username,
            //                Password = password
            //            });

            //            flag = authUser != null;

            //            if (flag == false)
            //                Console.Write("\n--------Wrong credentials!--------\n");
            //            else
            //            {
            //                Console.Write("\n--------Successfully logged in!--------\n");
            //                flag1 = true;
            //                flag = false;
            //                userId = authUser.UserId;
            //            }
            //            break;

            //        case "2":
            //            Console.Write("\nName: ");
            //            var nameRegister = Console.ReadLine();
            //            Console.Write("Country: ");
            //            var countryRegister = Console.ReadLine();
            //            Console.Write("DateOfBirth: ");
            //            var dateOfBirthRegister = Console.ReadLine();

            //            var personRegister = await mediator.Send(new CreatePerson
            //            {
            //                Name = nameRegister,
            //                Country = countryRegister,
            //                BirthDate = dateOfBirthRegister
            //            });

            //            Console.Write("\nUserName: ");
            //            var usernameRegister = Console.ReadLine();
            //            Console.Write("Password: ");
            //            var passwordRegister = Console.ReadLine();

            //            var newUser = await mediator.Send(new CreateUser
            //            {
            //                Username = usernameRegister,
            //                Password = passwordRegister,
            //                UserPerson = personRegister
            //            });

            //            flag = newUser != null;

            //            if (flag == false)
            //                Console.Write("\n--------Username Already exists!--------\n");
            //            else
            //            {
            //                Console.Write("\n--------Successfully registered!--------\n");
            //                flag1 = true;
            //                flag = false;
            //                userId = newUser.UserId;
            //            }
            //            break;

            //        case "3":
            //            flag = false;
            //            break;
            //        default:
            //            Console.WriteLine("Not a command!!");
            //            break;
            //    }
            //}

            //while (flag1)
            //{
            //    var league = await mediator.Send(new GetLeagueById
            //    {
            //        LeagueId = 1
            //    });

            //    var user = await mediator.Send(new GetUserById
            //    {
            //        UserId = userId,
            //    });

            //    await mediator.Send(new GenerateLeagueFixtures { 
            //        LeagueId = league.LeagueId });

            //    Console.WriteLine("1 - Create Team");
            //    Console.WriteLine("2 - View League Fixtures");
            //    Console.WriteLine("3 - Simulate Fixtures");
            //    Console.WriteLine("4 - Exit");

            //    var input = Console.ReadKey().KeyChar.ToString();

            //    switch (input)
            //    {
            //        case "1":
            //            Console.Write("\nTeam Name:");
            //            var teamName = Console.ReadLine();
            //            Console.Write("Country: ");
            //            var teamCountry = Console.ReadLine();
            //            Console.Write("Venue: ");
            //            var teamVenue = Console.ReadLine();

            //            var manager = await mediator.Send(new CreateRealManager
            //            {
            //                UserManager = user
            //            });

            //            var team = await mediator.Send(new CreateTeam
            //            {
            //                Name = teamName,
            //                Country = teamCountry,
            //                Venue = teamVenue,
            //                TeamManagerId = manager.ManagerId
            //            });


            //            await Task.Run(() => AddPlayersToTeam(team.TeamId, mediator));

            //            await mediator.Send(new AddTeamToLeague
            //            {
            //                LeagueId = league.LeagueId,
            //                TeamId = team.TeamId,
            //            });

            //            break;

            //        case "2":
            //            Console.WriteLine($"\n{league.Name}'s Fixtues\n");
            //            league.Fixtures
            //                .ForEach(f => Console.WriteLine($"{f.teams[0]} vs {f.teams[1]}"));
            //            break;

            //        case "3":
            //            Console.WriteLine($"\n{league.Name}'s Fixtue results\n");
            //            await mediator.Send(new SimulateAllFixtures
            //            {
            //                LeagueId = 1
            //            });
            //            league.Fixtures
            //                .ForEach(f => Console.WriteLine($"{f.teams[0]} {f.HomeTeamScore} - {f.AwayTeamScore} {f.teams[1]}"));
            //            break;
            //    }
            //}

        }

        public static async Task AddPlayersToTeam(long teamId, IMediator mediator, long teamNumber)
        {

            switch (teamNumber)
            {
                case 1:
                    await AddAttackerToCreatedTeam(mediator, teamId, "Karim Benzema", "France", "1987-09-19", "ST");
                    await AddMidfielderToCreatedTeam(mediator, teamId, "Luka Modric", "Croatia", "1985-09-09", "CM");
                    await AddDefenderToCreatedTeam(mediator, teamId, "Alaba", "Austria", "1992-06-24", "CB");
                    await AddGoalkeeperToCreatedTeam(mediator, teamId, "Courtois", "Belgium", "1992-05-11", "GK");

                    break;
                case 2:
                    await AddAttackerToCreatedTeam(mediator, teamId, "Lewandowski", "Poland", "1988-08-21", "ST");
                    await AddMidfielderToCreatedTeam(mediator, teamId, "De Jong", "Netherlands", "1997-05-12", "CDM");
                    await AddDefenderToCreatedTeam(mediator, teamId, "Pique", "Spain", "1987-02-02", "CB");
                    await AddGoalkeeperToCreatedTeam(mediator, teamId, "Ter Stegen", "Germany", "1992-04-30", "GK");

                    break;
                default:
                    break;
            }
        }
        private static async Task AddMidfielderToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var playerPerson = await mediator.Send(new CreatePerson
            {
                Name = name,
                BirthDate = dateOfBirth,
                Country = country
            });

            var player = await mediator.Send(new CreateMidfielder
            {
                Position = position,
                PlayerPersonId = playerPerson.PersonId,
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddAttackerToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var playerPerson = await mediator.Send(new CreatePerson
            {
                Name = name,
                BirthDate = dateOfBirth,
                Country = country
            });

            var player = await mediator.Send(new CreateAttacker
            {
                Position = position,
                PlayerPersonId = playerPerson.PersonId,
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddDefenderToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var playerPerson = await mediator.Send(new CreatePerson
            {
                Name = name,
                BirthDate = dateOfBirth,
                Country = country
            });

            var player = await mediator.Send(new CreateDefender
            {
                Position = position,
                PlayerPersonId = playerPerson.PersonId,
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }

        private static async Task AddGoalkeeperToCreatedTeam(IMediator mediator, long teamId, string name, string country, string dateOfBirth, string position)
        {
            var playerPerson = await mediator.Send(new CreatePerson
            {
                Name = name,
                BirthDate = dateOfBirth,
                Country = country
            });

            var player = await mediator.Send(new CreateGoalkeeper
            {
                PlayerPersonId = playerPerson.PersonId,
            });

            await mediator.Send(new AddPlayerToTeam
            {
                TeamId = teamId,
                PlayerId = player.PlayerId
            });
        }
    }
}