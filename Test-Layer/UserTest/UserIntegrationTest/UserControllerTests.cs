using Application_Layer.DTO_s;
using Newtonsoft.Json;
using System.Text;
using Domain_Layer.Models.UserModel;
using System.Net;
using Test_Layer.TestHelper;

namespace Test.Application.Integration
{
    [TestFixture]
    public class UserControllerIntegrationTests
    {
        private CustomWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CanRegisterUser()
        {
            // Arrange
            var userDto = new RegisterUserDTO
            {
                Role = "Student",
                FirstName = "Test",
                LastName = "Testson",
                Email = "testuser@GTest.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            // Konvertera DTO till JSON
            var content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/register", content);

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            var responseContent = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserModel>(responseContent);

            Assert.IsNotNull(user);
            Assert.That(user.FirstName, Is.EqualTo(userDto.FirstName));
            Assert.That(user.LastName, Is.EqualTo(userDto.LastName));
            Assert.That(user.Email, Is.EqualTo(userDto.Email));
            Assert.IsNotNull(user.PasswordHash);
        }

        [Test]
        public async Task Register_ReturnsBadRequest_ForInvalidUser()
        {
            // Arrange
            var invalidUserDto = new RegisterUserDTO
            {
                Role = "UnknownRole",
                FirstName = "",
                LastName = "",
                Email = "notanemail",
                Password = "123",
                ConfirmPassword = "1234"
            };

            var content = new StringContent(JsonConvert.SerializeObject(invalidUserDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user/register", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
