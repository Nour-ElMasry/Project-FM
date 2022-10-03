using Application.Pagination;
using FootballManagerAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    [TestClass]
    public class UserControllerIntegrationTests
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

            var user = new UserPostDto
            {
                Name = "New User",
                Country = "New Country",
                DateOfBirth = "2009-05-17",
                Username = "User132123",
                Password = "User3141923@"
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("api/v1/Users",
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));


            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Users_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Users/All");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_All_Users_ShouldReturnExistingUsers()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Users/All");

            var result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<Pager<UserGetDto>>(result);

            Assert.IsNotNull(users);
        }

        [TestMethod]
        public async Task Get_User_By_Id_ShouldReturnOkResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Users/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_User_By_Id_ShouldReturnExistingUser()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/v1/Users/1");

            var result = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserGetDto>(result);

            Assert.AreEqual(user.Id, 1);
        }

        [TestMethod]
        public async Task Delete_User_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Users/1");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task Delete_User_DeletedManagerShouldNotExist()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var client = _factory.CreateClient();
            var response = await client.DeleteAsync("api/v1/Users/1");

            var checkDelete = await client.GetAsync("api/v1/Users/1");

            Assert.AreEqual(HttpStatusCode.NotFound, checkDelete.StatusCode);
        }

        [TestMethod]
        public async Task Auth_User_ShouldReturnNoContentResponse()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            var user = new UserAuthDto
            {
                Username = "username",
                Password = "password"
            };
            var client = _factory.CreateClient();

            var response = await client.PostAsync("api/v1/Users/Auth",
                new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}