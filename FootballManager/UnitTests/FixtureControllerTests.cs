using Application.Commands;
using Application.Pagination;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Controllers;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class FixtureControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();

        [TestMethod]
        public async Task Get_All_Fixtures_GetAllFixturesQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllFixtures>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Pager<Fixture>(1, 1))
                .Verifiable();

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllFixtures(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllFixtures>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        [TestMethod]
        public async Task Get_Fixture_By_Id_GetFixtureByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixtureById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetFixtureById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetFixtureById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_Fixture_By_Id_GetFixtureByIdWithCorrectFixtureIdIsCalled()
        {
            long fixtureId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixtureById>(), It.IsAny<CancellationToken>()))
                .Returns<GetFixtureById, CancellationToken>(async (q, c) =>
                {
                    fixtureId = q.FixtureId;
                    return await Task.FromResult(
                        new Fixture
                        {
                            HomeTeam = new Team
                            {
                                Name = "HomeTeam1"
                            },
                            AwayTeam = new Team
                            {
                                Name = "AwayTeam1"
                            },
                            FixtureLeague = new League
                            {
                                Name = "League1"
                            },
                            FixtureScore = new Score
                            {
                                HomeScore = 0,
                                AwayScore = 0
                            },
                            Date = DateTime.Now,
                            Venue = "HomeTeam1 Stadium"
                        });
                });

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetFixtureById(1);

            Assert.AreEqual(fixtureId, 1);
        }

        [TestMethod]
        public async Task Get_Fixture_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixtureById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                        new Fixture
                        {
                            HomeTeam = new Team
                            {
                                Name = "HomeTeam1"
                            },
                            AwayTeam = new Team
                            {
                                Name = "AwayTeam1"
                            },
                            FixtureLeague = new League
                            {
                                Name = "League1"
                            },
                            FixtureScore = new Score
                            {
                                HomeScore = 0,
                                AwayScore = 0
                            },
                            Date = DateTime.Now,
                            Venue = "HomeTeam1 Stadium"
                        });

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetFixtureById(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_Fixture_By_Id_ShouldreturnFoundFixture()
        {
            var fixture = new Fixture
            {
                HomeTeam = new Team
                {
                    Name = "HomeTeam1"
                },
                AwayTeam = new Team
                {
                    Name = "AwayTeam1"
                },
                FixtureLeague = new League
                {
                    Name = "League1"
                },
                FixtureScore = new Score
                {
                    HomeScore = 0,
                    AwayScore = 0
                },
                Date = DateTime.Now,
                Venue = "HomeTeam1 Stadium"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixtureById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fixture);

            _mockMapper.Setup(x => x.Map<FixtureGetDto>(It.IsAny<Fixture>()))
            .Returns((Fixture f) =>
                {
                    return new FixtureGetDto
                    {
                        FixtureLeague = new ShortLeagueGetDto 
                        {
                            LeagueId = f.FixtureLeague.LeagueId,
                            LeagueName = f.FixtureLeague.Name
                        },
                        HomeTeam = new ShortTeamGetDto 
                        {
                            TeamId = f.HomeTeam.TeamId,
                            TeamName = f.HomeTeam.Name,
                            TeamLogo = f.HomeTeam.Logo
                        },
                        AwayTeam = new ShortTeamGetDto
                        {
                            TeamId = f.AwayTeam.TeamId,
                            TeamName = f.AwayTeam.Name,
                            TeamLogo = f.AwayTeam.Logo
                        },
                        FixtureScore = new Score
                        {
                            HomeScore = 0,
                            AwayScore = 0
                        },
                        Date = f.Date,
                        Venue = f.Venue,
                    };
                });

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetFixtureById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(fixture.HomeTeam.Name, ((FixtureGetDto)okResult.Value).HomeTeam.TeamName);
            Assert.AreEqual(fixture.AwayTeam.Name, ((FixtureGetDto)okResult.Value).AwayTeam.TeamName);
            Assert.AreEqual(fixture.FixtureLeague.Name, ((FixtureGetDto)okResult.Value).FixtureLeague.LeagueName);
            Assert.AreEqual(fixture.FixtureScore.HomeScore, ((FixtureGetDto)okResult.Value).FixtureScore.HomeScore);
            Assert.AreEqual(fixture.FixtureScore.AwayScore, ((FixtureGetDto)okResult.Value).FixtureScore.AwayScore);
            Assert.AreEqual(fixture.Venue, ((FixtureGetDto)okResult.Value).Venue);
            Assert.AreEqual(fixture.Date, ((FixtureGetDto)okResult.Value).Date);
        }
        [TestMethod]
        public async Task Update_Fixture_UpdateFixtureIsCalled()
        {
            var fixture = new FixturePutDto();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdateFixture>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.UpdateFixture(1, fixture);

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateFixture>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}