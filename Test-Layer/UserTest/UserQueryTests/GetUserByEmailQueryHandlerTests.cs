using Application_Layer.Queries.GetUserByEmail;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.QueryTests
{
    [TestFixture]
    public class GetUserByEmailQueryHandlerTests
    {
        private IUserRepository _userRepository;
        private GetUserByEmailQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _handler = new GetUserByEmailQueryHandler(_userRepository);
        }

        [Test]
        public async Task Handle_GetUserByEmail_Returns_UserModel_When_User_Found()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUser = new UserModel { Id = "1", UserName = "TestUser", Email = email };

            A.CallTo(() => _userRepository.GetUserByEmailAsync(email)).Returns(expectedUser);

            var request = new GetUserByEmailQuery(email);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
            Assert.That(result.Email, Is.EqualTo(expectedUser.Email));
        }

        [Test]
        public void Handle_GetUserByEmail_Throws_KeyNotFoundException_When_User_Not_Found()
        {
            // Arrange
            UserModel? notFoundUser = null;
            var email = "nonexistent@example.com";
            A.CallTo(() => _userRepository.GetUserByEmailAsync(email))!.Returns(notFoundUser);

            var request = new GetUserByEmailQuery(email);

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo($"User with Email '{email}' cannot be found!."));
        }
        [Test]
        public void Handle_InvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var invalidEmail = string.Empty; 
            var getUserByEmailQuery = new GetUserByEmailQuery(invalidEmail);

            var handler = new GetUserByEmailQueryHandler(userRepository);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(getUserByEmailQuery, CancellationToken.None);
            });

            // Assert that the exception message is as expected
            StringAssert.Contains("Email cannot be empty!", ex.Message);
        }

    }
}
