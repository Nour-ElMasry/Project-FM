using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class TeamControllerIntegrationTests
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
        public async Task Get_All_Teams_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Teams_ShouldReturnExistingTeams()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams");

            var result = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<TeamGetDto>>(result);

            Assert.IsNotNull(teams);
        }

        [TestMethod]
        public async Task Get_Team_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_By_Id_ShouldReturnExistingTeam()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1");

            var result = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamGetDto>(result);

            Assert.AreEqual(team.Id, 1);
        }

        [TestMethod]
        public async Task Delete_Team_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Teams/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Team_DeletedTeamShouldNotExist()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Teams/1");

            var checkDelete = await client.GetAsync("api/v1/Teams/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }

        [TestMethod]
        public async Task Get_Team_Players_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/Players");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Team_Players_By_Id_ShouldReturnExistingTeamPlayers()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/Players");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_Team_Fixtures_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/Fixtures");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Team_Fixtures_By_Id_ShouldReturnExistingTeamFixtures()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/Fixtures");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Add_Player_To_Team_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/Players/AddPlayer/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Remove_Player_From_Team_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Teams/1/Players/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Add_Manager_To_Team_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Teams/1/AddManager/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}