using Application_Layer.Queries.GetUserById;
using Domain_Layer.Models.User;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTest.UnitTests.UserQueryTests
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
        public async Task Handle_ValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = new Guid().ToString();
            var expectedUser = new UserModel { Id = userId, UserName = "TestUser", Email = "email@gmail.com" };
            var query = new GetUserByIdQuery(userId);

            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId))
                .Returns(expectedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
            Assert.That(result.Email, Is.EqualTo(expectedUser.Email));
            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Handle_NonExistentUserId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userId = new Guid().ToString();
            var query = new GetUserByIdQuery(userId);

            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            StringAssert.Contains(query.UserId, exception.Message);
        }

        [Test]
        public void Handle_EmptyUserId_ThrowsArgumentException()
        {
            // Arrange
            var query = new GetUserByIdQuery("");

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("UserId cannot be empty", exception.Message);
        }
    }

}
