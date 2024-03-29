﻿using Application.Commands;
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

namespace UnitTests
{
    [TestClass]
    public class LeagueControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();

        [TestMethod]
        public async Task Get_All_Leagues_GetAllLeaguesQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllLeagues>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllLeagues();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllLeagues>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_League_By_Id_GetLeagueByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetLeagueById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetLeagueById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_League_By_Id_GetLeagueByIdWithCorrectLeagueIdIsCalled()
        {
            long leagueId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetLeagueById>(), It.IsAny<CancellationToken>()))
                .Returns<GetLeagueById, CancellationToken>(async (q, c) =>
                {
                    leagueId = q.LeagueId;
                    return await Task.FromResult(
                        new League());
                });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueById(1);

            Assert.AreEqual(leagueId, 1);
        }

        [TestMethod]
        public async Task Get_League_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetLeagueById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                        new League());

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetLeagueById(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_By_Id_ShouldreturnFoundLeague()
        {
            var league = new League
            {
                LeagueId = 1,
                Name = "Fake League",
                CurrentSeason = new Season(2026)
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetLeagueById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(league);

            _mockMapper.Setup(x => x.Map<LeagueGetDto>(It.IsAny<League>()))
            .Returns((League l) =>
            {
                return new LeagueGetDto
                {
                    Id = l.LeagueId,
                    Name = l.Name,
                    CurrentSeason = l.CurrentSeason
                };
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetLeagueById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(league.LeagueId, ((LeagueGetDto)okResult.Value).Id);
            Assert.AreEqual(league.Name, ((LeagueGetDto)okResult.Value).Name);
            Assert.AreEqual(league.CurrentSeason, ((LeagueGetDto)okResult.Value).CurrentSeason);
        }

        [TestMethod]
        public async Task Get_LeagueTeams_By_Id_GetLeagueTeamsByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamsByLeague>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueTeamsById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetTeamsByLeague>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_LeagueTeams_By_Id_GetLeagueTeamsByIdWithCorrectLeagueIdIsCalled()
        {
            long leagueId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamsByLeague>(), It.IsAny<CancellationToken>()))
                .Returns<GetTeamsByLeague, CancellationToken>(async (q, c) =>
                {
                    leagueId = q.LeagueId;
                    return await Task.FromResult(
                       new List<Team>());
                });

            _mockMapper.Setup(x => x.Map<List<TeamGetDto>>(It.IsAny<List<Team>>()))
                .Returns((List<Team> ps) =>
                {
                    List<TeamGetDto> result = new();

                    ps.ForEach(p =>
                    {
                        result.Add(new TeamGetDto());
                    });

                    return result;
                });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueTeamsById(1);

            Assert.AreEqual(leagueId, 1);
        }

        [TestMethod]
        public async Task Get_LeagueTeams_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamsByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new List<Team>());

            _mockMapper.Setup(x => x.Map<List<TeamGetDto>>(It.IsAny<List<Team>>()))
                .Returns((List<Team> ps) =>
                {
                    List<TeamGetDto> result = new();

                    ps.ForEach(p =>
                    {
                        result.Add(new TeamGetDto());
                    });

                    return result;
                });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetLeagueTeamsById(1);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_LeagueTeams_By_Id_ShouldreturnFoundTeams()
        {
            var league = new League
            {
                LeagueId = 1,
                Name = "Fake League",
                CurrentSeason = new Season(2026),
                Teams = new List<Team> {
                            new Team("Team1", "Country1", "Team1 Stadium"),
                            new Team("Team2", "Country2", "Team2 Stadium")}
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamsByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(league.Teams);

            _mockMapper.Setup(x => x.Map<List<TeamGetDto>>(It.IsAny<List<Team>>()))
            .Returns((List<Team> ts) =>
            {
                List<TeamGetDto> result = new();

                ts.ForEach(t =>
                {
                    result.Add(new TeamGetDto());
                });

                return result;
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetLeagueTeamsById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(league.Teams.Count, ((List<TeamGetDto>)okResult.Value).Count);
        }

        [TestMethod]
        public async Task Get_TopScorers_By_LeagueId_GetTopScorersByLeagueIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTopScorersByLeague>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTopScorersByLeagueId(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetTopScorersByLeague>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_TopScorers_By_LeagueId_GetTopScorersByLeagueIdWithCorrectLeagueIdIsCalled()
        {
            long leagueId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTopScorersByLeague>(), It.IsAny<CancellationToken>()))
                .Returns<GetTopScorersByLeague, CancellationToken>(async (q, c) =>
                {
                    leagueId = q.LeagueId;
                    return await Task.FromResult(
                       new List<Player>());
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

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetTopScorersByLeagueId(1);

            Assert.AreEqual(leagueId, 1);
        }

        [TestMethod]
        public async Task Get_TopScorers_By_LeagueId_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTopScorersByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new List<Player>());

            _mockMapper.Setup(x => x.Map<List<ShortPlayerGetDto>>(It.IsAny<Pager<Player>>()))
                .Returns((Pager<Player> ps) =>
                {
                    List<ShortPlayerGetDto> result = new();

                    ps.PageResults.ForEach(p =>
                    {
                        result.Add(new ShortPlayerGetDto());
                    });

                    return result;
                });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetTopScorersByLeagueId(1);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_LeaguePlayers_By_Id_ShouldreturnFoundPlayers()
        {
            var league = new League
            {
                LeagueId = 1,
                Name = "Fake League",
                CurrentSeason = new Season(2026),
                Teams = new List<Team> {
                            new Team("Team1", "Country1", "Team1 Stadium")
                            {
                                Players = new List<Player> {
                                        new Attacker(new Person("Person1", "2001-02-16", "Somewhere1")),
                                        new Midfielder(new Person("Person2", "2001-02-16", "Somewhere2"))
                                    }
                            },
                            new Team("Team2", "Country2", "Team2 Stadium")
                            {
                                Players = new List<Player> {
                                        new Attacker(new Person("Person3", "2001-02-16", "Somewhere3")),
                                        new Midfielder(new Person("Person4", "2001-02-16", "Somewhere4"))
                                    }
                            }
                }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTopScorersByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    var players = new List<Player>();
                    league.Teams.ForEach(t => t.Players.ForEach(p => players.Add(p)));
                    return players;
                });

            _mockMapper.Setup(x => x.Map<List<ShortPlayerGetDto>>(It.IsAny<List<Player>>()))
            .Returns((List<Player> ps) =>
            {
                List<ShortPlayerGetDto> result = new();

                ps.ForEach(p =>
                {
                    result.Add(
                        new ShortPlayerGetDto
                        {
                            Id = p.PlayerId,
                            PlayerPerson = p.PlayerPerson,
                        });
                });

                return result;
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetTopScorersByLeagueId(1);
            var okResult = result as OkObjectResult;

            var playersCount = 0;

            league.Teams.ForEach(t => playersCount += t.Players.Count);

            Assert.AreEqual(playersCount, ((List<ShortPlayerGetDto>)okResult.Value).Count);
        }
        [TestMethod]
        public async Task Get_LeagueFixtures_By_Id_GetLeagueFixturesByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByLeague>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueFixturesById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetFixturesByLeague>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_LeagueFixtures_By_Id_GetLeagueFixturesByIdWithCorrectLeagueIdIsCalled()
        {
            long leagueId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByLeague>(), It.IsAny<CancellationToken>()))
                .Returns<GetFixturesByLeague, CancellationToken>(async (q, c) =>
                {
                    leagueId = q.LeagueId;
                    return await Task.FromResult(
                      new Pager<Fixture>(2, 1)
                      {
                          PageResults = new List<Fixture>() {
                            new Fixture(),
                            new Fixture()
                       }
                      });
                });

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<Pager<Fixture>>()))
            .Returns((Pager<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.PageResults.ForEach(f =>
                {
                    result.Add(
                        new FixtureGetDto
                        {
                            Id = f.FixtureId,
                        });
                });

                return result;
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetLeagueFixturesById(1);

            Assert.AreEqual(leagueId, 1);
        }

        [TestMethod]
        public async Task Get_LeagueFixtures_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new Pager<Fixture>(2, 1)
                       {
                           PageResults = new List<Fixture>() {
                            new Fixture(),
                            new Fixture()
                       }
                       });

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<Pager<Fixture>>()))
            .Returns((Pager<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.PageResults.ForEach(f =>
                {
                    result.Add(
                        new FixtureGetDto
                        {
                            Id = f.FixtureId,
                        });
                });

                return result;
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetLeagueFixturesById(1);

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_LeagueFixtures_By_Id_ShouldreturnFoundFixtures()
        {
            var league = new League
            {
                LeagueId = 1,
                Name = "Fake League",
                CurrentSeason = new Season(2026),
                Fixtures = new List<Fixture> {
                            new Fixture(),
                            new Fixture()
                       }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetFixturesByLeague>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Pager<Fixture>(2, 1) { PageResults = league.Fixtures });

            _mockMapper.Setup(x => x.Map<List<FixtureGetDto>>(It.IsAny<List<Fixture>>()))
            .Returns((List<Fixture> fs) =>
            {
                List<FixtureGetDto> result = new();

                fs.ForEach(f =>
                {
                    result.Add(
                        new FixtureGetDto
                        {
                            Id = f.FixtureId,
                        });
                });

                return result;
            });

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetLeagueFixturesById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(league.Fixtures.Count, ((Pager<FixtureGetDto>)okResult.Value).PageResults.Count);
        }
        [TestMethod]
        public async Task Delete_League_DeleteLeagueIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteLeague>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.DeleteLeague(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteLeague>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Remove_Team_From_League_RemoveTeamFromLeagueIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<RemoveTeamFromLeague>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.RemoveTeamFromLeague(1, 1);

            _mockMediator.Verify(x => x.Send(It.IsAny<RemoveTeamFromLeague>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Simulate_All_League_Fixture_SimulateAllLeagueFixtureIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<SimulateAllFixtures>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.SimulateAllLeagueFixture(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<SimulateAllFixtures>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Simulate_A_Fixtures_SimulateAFixturesIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<SimulateAFixture>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.SimulateALeagueFixture(1, 1);

            _mockMediator.Verify(x => x.Send(It.IsAny<SimulateAFixture>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Generate_League_Fixture_GenerateLeagueFixtureIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GenerateLeagueFixtures>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GenerateLeagueFixture(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GenerateLeagueFixtures>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Next_League_Season_NextLeagueSeasonIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<NextSeason>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new LeagueController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.NextLeagueSeason();

            _mockMediator.Verify(x => x.Send(It.IsAny<NextSeason>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
