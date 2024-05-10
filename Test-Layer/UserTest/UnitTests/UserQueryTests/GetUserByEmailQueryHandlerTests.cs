using Application_Layer.Queries.GetUserByEmail;
using Domain_Layer.Models.User;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;


namespace Test_Layer.UserTest.UnitTests.UserQueryTests
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
        public async Task Handle_ValidEmail_ReturnsUser()
        {
            // Arrange
            string email = "user@example.com";
            var query = new GetUserByEmailQuery(email);
            var expectedUser = new UserModel { Email = email };

            A.CallTo(() => _userRepository.GetUserByEmailAsync(query.Email))
                .Returns(Task.FromResult(expectedUser));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
        }


        [Test]
        public void Handle_EmailNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            string email = "nonexistent@example.com";
            var query = new GetUserByEmailQuery(email);
            A.CallTo(() => _userRepository.GetUserByEmailAsync(email))

                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Test]
        public void Handle_InvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            var query = new GetUserByEmailQuery("");

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("Email cannot be empty!", exception.Message);
        }

        [Test]
        public void Handle_EmailDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            string nonExistentEmail = "nonexistent@example.com";
            var query = new GetUserByEmailQuery(nonExistentEmail);
            A.CallTo(() => _userRepository.GetUserByEmailAsync(nonExistentEmail))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            StringAssert.Contains(nonExistentEmail, exception.Message);
        }

    }
}
