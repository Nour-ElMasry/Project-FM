using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;

namespace IntegrationTests
{
    [TestClass]
    public class LeagueControllerIntegrationTests
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
        public async Task Get_All_Leagues_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Leagues_ShouldReturnExistingLeagues()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues");

            var result = await response.Content.ReadAsStringAsync();
            var leagues = JsonConvert.DeserializeObject<List<LeagueGetDto>>(result);

            Assert.IsNotNull(leagues);
        }

        [TestMethod]
        public async Task Get_League_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_By_Id_ShouldReturnExistingLeague()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1");

            var result = await response.Content.ReadAsStringAsync();
            var league = JsonConvert.DeserializeObject<LeagueGetDto>(result);

            Assert.AreEqual(league.Id, 1);
        }

        [TestMethod]
        public async Task Delete_League_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Leagues/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_League_DeletedLeagueShouldNotExist()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Leagues/1");

            var checkDelete = await client.GetAsync("api/v1/Leagues/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_Teams_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Teams");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_Teams_By_Id_ShouldReturnExistingLeagueTeams()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Teams");

            var result = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<TeamGetDto>>(result);

            Assert.IsNotNull(teams);
        }

        [TestMethod]
        public async Task Remove_Team_From_League_By_Id_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Leagues/1/Teams/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_Players_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Players");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_Players_By_Id_ShouldReturnExistingLeaguePlayers()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Players");

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Get_League_Fixtures_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Fixtures");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_League_Fixtures_By_Id_ShouldReturnExistingLeagueFixtures()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Fixtures");

            var result = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<FixtureGetDto>>(result);

            Assert.IsNotNull(teams);
        }

        [TestMethod]
        public async Task Generate_League_Fixture_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/GenerateFixtures");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Generate_League_Fixture_ShouldReturnGeneratedFixtures()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/GenerateFixtures");

            var result = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<FixtureGetDto>>(result);

            Assert.IsNotNull(teams);
        }

        [TestMethod]
        public async Task Simulate_A_League_Fixture_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Fixtures/1/SimulateFixture");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Simulate_A_League_Fixture_ShouldReturnSimulatedFixture()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/Fixtures/1/SimulateFixture");

            var result = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<FixtureGetDto>(result);

            Assert.IsNotNull(teams);
        }

        [TestMethod]
        public async Task Next_League_Season_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Leagues/1/NextSeason");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}