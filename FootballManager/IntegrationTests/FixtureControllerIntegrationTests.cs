using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class FixtureControllerIntegrationTests
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
        public  async Task Get_All_Fixtures_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Fixtures");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Fixtures_ShouldReturnExistingFixtures()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Fixtures");

            var result = await response.Content.ReadAsStringAsync();
            var fixtures = JsonConvert.DeserializeObject<List<FixtureGetDto>>(result);

            Assert.IsNotNull(fixtures);
        }

        [TestMethod]
        public async Task Get_Fixture_By_Id_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Fixtures/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Fixture_By_Id_ShouldReturnExistingFixture()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Fixtures/1");

            var result = await response.Content.ReadAsStringAsync();
            var fixture = JsonConvert.DeserializeObject<FixtureGetDto>(result);

            Assert.AreEqual(fixture.Id, 1);
        }

        [TestMethod]
        public async Task Update_Fixture_ShouldReturnNoContentResponse()
        {
            var updateFixture = new FixturePutDto
            {
                Date = "2024-03-17"
            };

            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/v1/Fixtures/1",
                new StringContent(JsonConvert.SerializeObject(updateFixture), Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Update_Fixture_ShouldReturnUpdatedFixture()
        {
            var updateFixture = new FixturePutDto
            {
                Date = "2027-03-17"
            };

            var client = _factory.CreateClient();
            var response = await client.PutAsync("api/v1/Fixtures/1",
                new StringContent(JsonConvert.SerializeObject(updateFixture), Encoding.UTF8, "application/json"));

            var response2 = await client.GetAsync("api/v1/Fixtures/1");

            var result = await response2.Content.ReadAsStringAsync();
            var fixture = JsonConvert.DeserializeObject<FixtureGetDto>(result);

            Assert.AreEqual(fixture.Date, new DateTime(2027,03,17));
        }
    }
}