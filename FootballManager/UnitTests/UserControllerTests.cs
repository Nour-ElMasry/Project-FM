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

namespace UnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();

        [TestMethod]
        public async Task Get_All_Users_GetAllUsersQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllUsers>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllUsers();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllUsers>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        [TestMethod]
        public async Task Get_User_By_Id_GetUserByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetUserById("1");

            _mockMediator.Verify(x => x.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_User_By_Id_GetUserByIdWithCorrectUserIdIsCalled()
        {
            string userId = "";

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>()))
                .Returns<GetUserById, CancellationToken>(async (q, c) =>
                {
                    userId = q.UserId;
                    return await Task.FromResult(
                        new User());
                });

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetUserById("1");

            Assert.AreEqual(userId, "1");
        }

        [TestMethod]
        public async Task Get_User_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new User());

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetUserById("1");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_User_By_Id_ShouldreturnFoundUser()
        {
            var user = new User();

            user.UserName = "FakeUser";
            user.PasswordHash = "1234";

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetUserById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _mockMapper.Setup(x => x.Map<UserGetDto>(It.IsAny<User>()))
            .Returns((User u) =>
            {
                return new UserGetDto
                {
                    Username = u.UserName,
                };
            });

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetUserById("1");
            var okResult = result as OkObjectResult;

            Assert.AreEqual(user.UserName, ((UserGetDto)okResult.Value).Username);
        }
        [TestMethod]
        public async Task Get_User_Team_GetUserTeamIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamByUserId>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetUserTeam("1");

            _mockMediator.Verify(x => x.Send(It.IsAny<GetTeamByUserId>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_User_Team_GetUserTeamWithCorrectUserIdIsCalled()
        {
            string userId = "";

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamByUserId>(), It.IsAny<CancellationToken>()))
                .Returns<GetTeamByUserId, CancellationToken>(async (q, c) =>
                {
                    userId = q.UserId;
                    return await Task.FromResult(
                        new Team());
                });

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetUserTeam("1");

            Assert.AreEqual(userId, "1");
        }

        [TestMethod]
        public async Task Get_User_Team_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamByUserId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new Team());

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetUserTeam("1");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_User_Team_ShouldreturnFoundTeam()
        {
            var userTeam = new Team
            {
                TeamId = 1,
                Name = "Fake Team",
                TeamManager = new RealManager(new User() { UserPerson = new Person("Fake User", "2001-02-16", "Country") })
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTeamByUserId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userTeam);

            _mockMapper.Setup(x => x.Map<TeamGetDto>(It.IsAny<Team>()))
            .Returns((Team u) =>
            {
                return new TeamGetDto
                {
                    Id = u.TeamId,
                    TeamManager = u.TeamManager.ManagerPerson,
                };
            });

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetUserTeam("1");
            var okResult = result as OkObjectResult;

            Assert.AreEqual(userTeam.TeamId, ((TeamGetDto)okResult.Value).Id);
            Assert.AreEqual(userTeam.TeamManager.ManagerPerson, ((TeamGetDto)okResult.Value).TeamManager);
        }

        [TestMethod]
        public async Task Auth_User_AuthUserIsCalled()
        {
            var user = new UserAuthDto();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<LoginUser>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.LoginUser(user);

            _mockMediator.Verify(x => x.Send(It.IsAny<LoginUser>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Delete_User_DeleteUserIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteUser>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new UserController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.DeleteUser("1");

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteUser>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}