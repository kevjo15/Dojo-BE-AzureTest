using Application_Layer.Queries.GetUserById;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Test_Layer.TestHelper;

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

        [Test, CustomAutoData]
        public async Task Handle_ValidUserId_ReturnsUser(GetUserByIdQuery query, UserModel expectedUser)
        {
            // Arrange
            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId))
                .Returns(expectedUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUser));
            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId)).MustHaveHappenedOnceExactly();
        }

        [Test, CustomAutoData]
        public void Handle_NonExistentUserId_ThrowsKeyNotFoundException(GetUserByIdQuery query)
        {
            // Arrange
            A.CallTo(() => _userRepository.GetUserByIdAsync(query.UserId))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            StringAssert.Contains(query.UserId, exception.Message);
        }

        [Test, CustomAutoData]
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
