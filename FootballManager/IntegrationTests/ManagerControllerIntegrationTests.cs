using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class ManagerControllerIntegrationTests
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
        public async Task Get_All_Managers_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Managers");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Managers_ShouldReturnExistingManagers()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Managers");

            var result = await response.Content.ReadAsStringAsync();
            var fixtures = JsonConvert.DeserializeObject<List<ManagerGetDto>>(result);

            Assert.IsNotNull(fixtures);
        }

        [TestMethod]
        public async Task Get_Manager_By_Id_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Managers/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_Manager_By_Id_ShouldReturnExistingManager()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Managers/1");

            var result = await response.Content.ReadAsStringAsync();
            var manager = JsonConvert.DeserializeObject<ManagerGetDto>(result);

            Assert.AreEqual(manager.ManagerId, 1);
        }

        [TestMethod]
        public async Task Delete_Manager_ShouldReturnNoContentResponse()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Managers/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Manager_DeletedManagerShouldNotExist()
        {
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Managers/1");

            var checkDelete = await client.GetAsync("api/v1/Managers/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }
    }
}