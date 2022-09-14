using Application.Commands;
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
                .Verifiable();

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllFixtures();

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
                            AwayTeamScore = 0,
                            HomeTeamScore = 0,
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
                            AwayTeamScore = 0,
                            HomeTeamScore = 0,
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
                AwayTeamScore = 0,
                HomeTeamScore = 0,
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
                        FixtureLeagueName = f.FixtureLeague.Name,
                        HomeTeamName = f.HomeTeam.Name,
                        AwayTeamName = f.AwayTeam.Name,
                        HomeTeamScore = f.HomeTeamScore,
                        AwayTeamScore = f.AwayTeamScore,
                        Date = f.Date,
                        Venue = f.Venue,
                    };
                });

            var controller = new FixtureController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetFixtureById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(fixture.HomeTeam.Name, ((FixtureGetDto)okResult.Value).HomeTeamName);
            Assert.AreEqual(fixture.AwayTeam.Name, ((FixtureGetDto)okResult.Value).AwayTeamName);
            Assert.AreEqual(fixture.FixtureLeague.Name, ((FixtureGetDto)okResult.Value).FixtureLeagueName);
            Assert.AreEqual(fixture.HomeTeamScore, ((FixtureGetDto)okResult.Value).HomeTeamScore);
            Assert.AreEqual(fixture.AwayTeamScore, ((FixtureGetDto)okResult.Value).AwayTeamScore);
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