using Application_Layer.Queries.GetAllUsers;
using AutoFixture;
using Domain_Layer.Models.User;
using FakeItEasy;
using FluentAssertions;
using Infrastructure_Layer.Repositories.User;

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

        [Test]
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

        [Test]
        public async Task Handle_ReturnsAllUsers_WithExpectedProperties()
        {
            // Arrange
            var expectedUsers = _fixture.CreateMany<UserModel>(5).ToList(); // Skapa en lista med 5 användare
            A.CallTo(() => _userRepository.GetAllUsersAsync()).Returns(expectedUsers);

            // Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedUsers, options => options.ComparingByMembers<UserModel>());
            foreach (var user in result)
            {
                var expectedUser = expectedUsers.FirstOrDefault(u => u.Id == user.Id);
                Assert.That(user.FirstName, Is.EqualTo(expectedUser.FirstName));
                Assert.That(user.LastName, Is.EqualTo(expectedUser.LastName));
                Assert.That(user.Id, Is.EqualTo(expectedUser.Id));
                Assert.That(user.Role, Is.EqualTo(expectedUser.Role));
            }
        }

        [Test]
        public async Task Handle_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Arrange
            A.CallTo(() => _userRepository.GetAllUsersAsync()).Returns(new List<UserModel>());

            // Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Empty);
        }


        [Test]
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
