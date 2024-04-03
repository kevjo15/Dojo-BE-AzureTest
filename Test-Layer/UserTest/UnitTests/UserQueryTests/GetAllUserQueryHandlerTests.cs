using Application_Layer.Queries.GetAllUsers;
using AutoFixture;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitTests.UserQueryTests
{
    [TestFixture]
    public class GetAllUsersQueryHandlerTests
    {
        private IUserRepository _userRepository;
        private GetAllUsersQueryHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _handler = new GetAllUsersQueryHandler(_userRepository);
            _fixture = new Fixture();
        }

        [Test, CustomAutoData]
        public async Task Handle_CallsGetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var expectedUsers = _fixture.CreateMany<UserModel>().ToList();
            A.CallTo(() => _userRepository.GetAllUsersAsync()).Returns(expectedUsers);

            // Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUsers));
            A.CallTo(() => _userRepository.GetAllUsersAsync()).MustHaveHappenedOnceExactly();
        }

        [Test, CustomAutoData]
        public void Handle_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var expectedException = new Exception("Database connection failed");
            A.CallTo(() => _userRepository.GetAllUsersAsync()).Throws(expectedException);

            // Act & Assert
            var actualException = Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetAllUsersQuery(), CancellationToken.None));
            Assert.That(actualException.Message, Is.EqualTo(expectedException.Message),
                "Expected GetAllUsersQueryHandler to throw an Exception with a specific message when UserRepository throws an exception, but it did not.");
        }
    }
}
