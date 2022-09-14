using Application.Commands;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using FootballManagerAPI.Controllers;
using FootballManagerAPI.Dto;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ManagerControllerTests
    {
        private readonly Mock<IMediator> _mockMediator = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<ILogger<object>> _mockLogger = new();

        [TestMethod]
        public async Task Get_All_Managers_GetAllManagersQueryIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllManagers>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetAllManagers();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllManagers>(), It.IsAny<CancellationToken>()), Times.Once());
        }
        [TestMethod]
        public async Task Get_Manager_By_Id_GetManagerByIdIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetManagerById>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetManagerById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetManagerById>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [TestMethod]
        public async Task Get_Manager_By_Id_GetManagerByIdWithCorrectManagerIdIsCalled()
        {
            long managerId = 0;

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetManagerById>(), It.IsAny<CancellationToken>()))
                .Returns<GetManagerById, CancellationToken>(async (q, c) =>
                {
                    managerId = q.ManagerId;
                    return await Task.FromResult(
                        new FakeManager());
                });

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);
            await controller.GetManagerById(1);

            Assert.AreEqual(managerId, 1);
        }

        [TestMethod]
        public async Task Get_Manager_By_Id_ReturnsOkStatusCode()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetManagerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                       new FakeManager());

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetManagerById(1);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_Manager_By_Id_ShouldreturnFoundManager()
        {
            var manager = new FakeManager
            {
                ManagerId = 1,
                ManagerPerson = new Person("ManagerPerson", "2001-02-16", "Somewhere"),
                CurrentTeam = new Team("Team1", "Country1", "Team1 Stadium")
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetManagerById>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(manager);

            _mockMapper.Setup(x => x.Map<ManagerGetDto>(It.IsAny<Manager>()))
            .Returns((Manager f) =>
            {
                return new ManagerGetDto
                {
                    ManagerId = f.ManagerId,
                    ManagerPerson = f.ManagerPerson,
                    CurrentTeamName = f.CurrentTeam.Name
                };
            });

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            var result = await controller.GetManagerById(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(manager.ManagerId, ((ManagerGetDto)okResult.Value).ManagerId);
            Assert.AreEqual(manager.ManagerPerson.Name, ((ManagerGetDto)okResult.Value).ManagerPerson.Name);
            Assert.AreEqual(manager.CurrentTeam.Name, ((ManagerGetDto)okResult.Value).CurrentTeamName);
        }
        [TestMethod]
        public async Task Delete_Manager_DeleteManagerIsCalled()
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<DeleteManager>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new ManagerController(_mockMapper.Object, _mockMediator.Object, _mockLogger.Object);

            await controller.DeleteManager(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteManager>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}