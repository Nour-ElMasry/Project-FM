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
    public class PlayerControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();
        [TestMethod]
        public async Task Get_All_Players_GetAllPlayersQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllPlayers>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllPlayers();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllPlayers>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        [TestMethod]
        public async Task Get_Player_By_Id_GetPlayerByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayerById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetPlayerById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetPlayerById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_Player_By_Id_GetPlayerByIdWithCorrectPlayerIdIsCalled()
        {
            long playerId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayerById>(), It.IsAny<CancellationToken>()))
                .Returns<GetPlayerById, CancellationToken>(async (q, c) =>
                {
                    playerId = q.PlayerId;
                    return await Task.FromResult(
                        new Attacker());
                });

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetPlayerById(1);

            Assert.AreEqual(playerId, 1);
        }

        [TestMethod]
        public async Task Get_Player_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                        new Attacker());

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetPlayerById(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_player_By_Id_ShouldreturnFoundPlayer()
        {
            var player = new Attacker
            {
                PlayerId = 1,
                PlayerPerson = new Person("PlayerPerson", "2001-02-16", "Somewhere")
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetPlayerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(player);

            _mockMapper.Setup(x => x.Map<PlayerGetDto>(It.IsAny<Player>()))
            .Returns((Player p) =>
            {
                return new PlayerGetDto
                {
                    Id = p.PlayerId,
                    PlayerPerson = p.PlayerPerson,
                };
            });

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetPlayerById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(player.PlayerId, ((PlayerGetDto)okResult.Value).Id);
            Assert.AreEqual(player.PlayerPerson.Name, ((PlayerGetDto)okResult.Value).PlayerPerson.Name);
        }
        [TestMethod]
        public async Task Delete_Player_DeleteManagerIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeletePlayer>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.DeletePlayer(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeletePlayer>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Update_Player_UpdateManagerIsCalled()
        {
            var player = new PlayerPutPostDto();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<UpdatePlayer>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.UpdatePlayer(1, player);

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdatePlayer>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Create_Player_CreatePlayerIsCalled()
        {
            var player = new PlayerPutPostDto();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreatePlayer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Attacker());

            _mockMapper.Setup(x => x.Map<CreatePlayer>(It.IsAny<PlayerPutPostDto>()))
            .Returns((PlayerPutPostDto p) =>
            {
                return new CreatePlayer();
            });

            _mockMapper.Setup(x => x.Map<PlayerGetDto>(It.IsAny<Player>()))
            .Returns((Player p) =>
            {
                return new PlayerGetDto();
            });

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.CreatePlayer(player);

            _mockMediator.Verify(x => x.Send(It.IsAny<CreatePlayer>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Create_Player_CreatePlayerReturnsDto()
        {
            var player = new PlayerPutPostDto
            {
                Name = "Someone",
                Country = "Country",
                DateOfBirth = "2001-02-15",
                Position = "Attacker"
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreatePlayer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Attacker(new Person(player.Name, player.DateOfBirth, player.Country)));

            _mockMapper.Setup(x => x.Map<CreatePlayer>(It.IsAny<PlayerPutPostDto>()))
            .Returns((Func<PlayerPutPostDto, CreatePlayer>)((PlayerPutPostDto p) =>
            {
                return new CreatePlayer
                {
                    Name = "Someone",
                    Image = "Country",
                    DateOfBirth = "2001-02-15",
                    Position = "Attacker"
                };
            }));

            _mockMapper.Setup(x => x.Map<PlayerGetDto>(It.IsAny<Player>()))
            .Returns((Player p) =>
            {
                return new PlayerGetDto
                {
                    Id = p.PlayerId,
                    PlayerPerson = p.PlayerPerson,
                    Position = p.Position
                };
            });

            var controller = new PlayerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.CreatePlayer(player);

            var okResult = result as CreatedAtActionResult;

            Assert.AreEqual(player.Name, ((PlayerGetDto)okResult.Value).PlayerPerson.Name);
            Assert.AreEqual(player.Country, ((PlayerGetDto)okResult.Value).PlayerPerson.Country);
            Assert.AreEqual(player.Position, ((PlayerGetDto)okResult.Value).Position);
        }
    }
}