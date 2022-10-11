using Application.Dto;
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
            _factory = new CustomWebApplicationFactory<Program>();

            var player = new PlayerPutPostDto
            {
                Name = "New Player",
                Country = "New Country",
                DateOfBirth = "2009-05-17",
                Position = "Midfielder",
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("api/v1/Players",
                new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json"));


            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Players_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/All");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Players_ShouldReturnExistingPlayers()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/All");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Player_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Player_By_Id_ShouldReturnExistingPlayer()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Players/1");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Delete_Player_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Player_DeletedPlayerShouldNotExist()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Players/1");

            var checkDelete = await client.GetAsync("api/v1/Players/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }

        [TestMethod]
        public async Task Update_Player_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();

            var updatePlayer = new PlayerPutPostDto
            {
                Name = "New Name",
                Country = "New Country",
                DateOfBirth = "2000-02-15",
                Position = "Attacker",
            };

            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/v1/Players/1",
                new StringContent(JsonConvert.SerializeObject(updatePlayer), Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}