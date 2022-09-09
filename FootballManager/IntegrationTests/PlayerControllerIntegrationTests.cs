using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class PlayerControllerIntegrationTests
    {
        private static TestContext _context;
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _context = testContext;
            _factory = new CustomWebApplicationFactory<Program>();

        }

        [TestMethod]
        public async Task Create_Player_ShouldReturnCreatedRespons()
        {
            var player = new PlayerPutPostDto { 
                Name = "New Player",
                Country = "New Country",
                DateOfBirth = "2009-05-17",
                PlayerRole = "Midfielder",
                Position = "CDM"
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("api/v1/Players",
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));


            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Players_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Players_ShouldReturnExistingPlayers()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Player_By_Id_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Player_By_Id_ShouldReturnExistingPlayer()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/1");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Delete_Player_ShouldReturnNoContentResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Player_DeletedPlayerShouldNotExist()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Players/1");

            var checkDelete = await client.GetAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }

        [TestMethod]
        public async Task Update_Player_ShouldReturnNoContentResponse()
        {
            var updatePlayer = new PlayerPutPostDto
            { 
                Name = "New Name",
                Country = "New Country",
                DateOfBirth = "2000-02-15",
                PlayerRole = "Attacker",
                Position = "LW"
            };

            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/v1/Players/1",
                new StringContent(JsonConvert.SerializeObject(updatePlayer), Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }     
    }
}