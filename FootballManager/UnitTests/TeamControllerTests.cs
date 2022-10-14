using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Application.Controllers;
using Application.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Pagination;
using Application.Filters;

namespace UnitTests
{
    [TestClass]
    public class TeamControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();

        [TestMethod]
        public async Task Get_All_Teams_GetAllTeamsQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllTeams>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllTeams();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllTeams>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_Team_By_Id_GetTeamByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetTeamById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_Team_By_Id_GetTeamByIdWithCorrectTeamIdIsCalled()
        {
            long teamId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamById>(), It.IsAny<CancellationToken>()))
                .Returns<GetTeamById, CancellationToken>(async (q, c) =>
                {
                    teamId = q.TeamId;
                    return await Task.FromResult(
                        new Team());
                });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamById(1);

            Assert.AreEqual(teamId, 1);
        }

        [TestMethod]
        public async Task Get_Team_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                        new Team());

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetTeamById(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_Team_By_Id_ShouldreturnFoundTeam()
        {
            var team = new Team
            {
                TeamId = 1,
                Name = "Fake Team",
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(team);

            _mockMapper.Setup(x => x.Map<TeamGetDto>(It.IsAny<Team>()))
            .Returns((Team t) =>
            {
                return new TeamGetDto
                {
                    Id = t.TeamId,
                    Name = t.Name
                };
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetTeamById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(team.TeamId, ((TeamGetDto)okResult.Value).Id);
            Assert.AreEqual(team.Name, ((TeamGetDto)okResult.Value).Name);
        }

        [TestMethod]
        public async Task Get_TeamPlayers_By_Id_GetTeamPlayersByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayersByTeam>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamPlayersById(1, new PlayerFilter());

            _mockMediator.Verify(x => x.Send(It.IsAny<GetPlayersByTeam>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_TeamPlayers_By_Id_GetTeamPlayersByIdWithCorrectTeamIdIsCalled()
        {
            long teamId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayersByTeam>(), It.IsAny<CancellationToken>()))
                .Returns<GetPlayersByTeam, CancellationToken>(async (q, c) =>
                {
                    teamId = q.TeamId;
                    return await Task.FromResult(
                       new Pager<Player>(1, 1));
                });

            _mockMapper.Setup(x => x.Map<List<ShortPlayerGetDto>>(It.IsAny<List<Player>>()))
            .Returns((List<Player> ps) =>
            {
                List<ShortPlayerGetDto> result = new();

                ps.ForEach(p =>
                {
                    result.Add(new ShortPlayerGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamPlayersById(1, new PlayerFilter());

            Assert.AreEqual(teamId, 1);
        }

        [TestMethod]
        public async Task Get_TeamPlayers_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayersByTeam>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new Pager<Player>(1, 1));

            _mockMapper.Setup(x => x.Map<List<ShortPlayerGetDto>>(It.IsAny<List<Player>>()))
            .Returns((List<Player> ps) =>
            {
                List<ShortPlayerGetDto> result = new();

                ps.ForEach(p =>
                {
                    result.Add(new ShortPlayerGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetTeamPlayersById(1, new PlayerFilter());

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_TeamPlayers_By_Id_ShouldreturnFoundPlayers()
        {
            var team = new Team
            {
                TeamId = 1,
                Name = "Fake Team",
                Players = new List<Player> {
                    new Attacker(),
                    new Midfielder(),
                    new Defender(),
                    new Goalkeeper()
                }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayersByTeam>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Pager<Player>(4, 1) { PageResults = team.Players });

            _mockMapper.Setup(x => x.Map<List<ShortPlayerGetDto>>(It.IsAny<List<Player>>()))
            .Returns((List<Player> ps) =>
            {
                List<ShortPlayerGetDto> result = new();

                ps.ForEach(p =>
                {
                    result.Add(new ShortPlayerGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetTeamPlayersById(1, new PlayerFilter());
            var okResult = result as OkObjectResult;

            Assert.AreEqual(team.Players.Count, ((Pager<ShortPlayerGetDto>)okResult.Value).PageResults.Count);
        }

        [TestMethod]
        public async Task Get_TeamFixtures_By_Id_GetTeamFixturesByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByTeam>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamFixturesById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetFixturesByTeam>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_TeamFixtures_By_Id_GetTeamFixturesByIdWithCorrectTeamIdIsCalled()
        {
            long teamId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByTeam>(), It.IsAny<CancellationToken>()))
                .Returns<GetFixturesByTeam, CancellationToken>(async (q, c) =>
                {
                    teamId = q.TeamId;
                    return await Task.FromResult(
                       new List<Fixture>());
                });

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<List<Fixture>>()))
            .Returns((List<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.ForEach(f =>
                {
                    result.Add(new FixtureGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTeamFixturesById(1);

            Assert.AreEqual(teamId, 1);
        }

        [TestMethod]
        public async Task Get_TeamFixtures_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByTeam>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new List<Fixture>());

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<List<Fixture>>()))
            .Returns((List<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.ForEach(f =>
                {
                    result.Add(new FixtureGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetTeamFixturesById(1);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_TeamFixturess_By_Id_ShouldreturnFoundFixtures()
        {
            var team = new Team
            {
                TeamId = 1,
                Name = "Fake Team",
                HomeFixtures = new List<Fixture> {
                    new Fixture(),
                    new Fixture(),
                    new Fixture(),
                },
                AwayFixtures = new List<Fixture> {
                    new Fixture(),
                    new Fixture(),
                }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByTeam>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    var ret = new List<Fixture>();
                    team.HomeFixtures.ForEach(f => ret.Add(f));
                    team.AwayFixtures.ForEach(f => ret.Add(f));

                    return ret;
                });

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<List<Fixture>>()))
            .Returns((List<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.ForEach(f =>
                {
                    result.Add(new FixtureGetDto());
                });

                return result;
            });

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetTeamFixturesById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual((team.HomeFixtures.Count + team.AwayFixtures.Count), ((List<FixtureGetDto>)okResult.Value).Count());
        }

        [TestMethod]
        public async Task Delete_Team_DeleteTeamIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteTeam>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.DeleteTeam(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteTeam>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Update_Team_UpdateTeamIsCalled()
        {

            var team = new TeamPutPostDto();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateTeam>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.UpdateTeam(1, team);

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateTeam>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Add_Player_From_Team_AddPlayerFromTeamIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<AddPlayerToTeam>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new TeamController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.AddPlayerToTeam(1, 1);

            _mockMediator.Verify(x => x.Send(It.IsAny<AddPlayerToTeam>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
