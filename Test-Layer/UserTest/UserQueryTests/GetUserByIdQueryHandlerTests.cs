using Application_Layer.Queries.GetUserById;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.QueryTests
{
    [TestFixture]
    public class GetUserByIdQueryHandlerTests
    {
        private IUserRepository _userRepository;
        private GetUserByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _handler = new GetUserByIdQueryHandler(_userRepository);
        }

        [Test]
        public async Task Handle_GetUserById_Returns_UserModel_When_User_Found()
        {
            // Arrange
            var userId = new Guid().ToString();
            var expectedUser = new UserModel { Id = userId, UserName = "TestUser", Email = "email@gmail.com" };

            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(expectedUser);

            var request = new GetUserByIdQuery(userId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
            Assert.That(result.Email, Is.EqualTo(expectedUser.Email));
        }

        [Test]
        public void Handle_GetUserById_Throws_KeyNotFoundException_When_User_Not_Found()
        {
            // Arrange
            UserModel? notFoundUser = null;
            var userId = new Guid().ToString();
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId))!.Returns(notFoundUser);

            var request = new GetUserByIdQuery(userId);

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo($"User with ID {userId} was not found!"));
        }
        [Test]
        public void Handle_InvalidUserId_ThrowsArgumentException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var invalidUserId = string.Empty;
            var getUserByIdQuery = new GetUserByIdQuery(invalidUserId);

            var handler = new GetUserByIdQueryHandler(userRepository);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(getUserByIdQuery, CancellationToken.None);
            });

            // Assert that the exception message is as expected
            StringAssert.Contains("UserId cannot be empty.", ex.Message);
        }

    }
}
