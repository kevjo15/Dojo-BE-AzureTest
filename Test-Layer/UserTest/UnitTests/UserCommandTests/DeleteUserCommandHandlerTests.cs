using Application_Layer.Commands.DeleteUser;
using AutoFixture;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTest.UnitTests.UserCommandTests
{
    [TestFixture]
    public class DeleteUserCommandHandlerTests
    {
        private IUserRepository _userRepository;
        private DeleteUserCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _handler = new DeleteUserCommandHandler(_userRepository);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_UserDeletionSucceeds_ReturnsTrue()
        {
            // Arrange
            var command = _fixture.Create<DeleteUserCommand>();
            A.CallTo(() => _userRepository.DeleteUserByIdAsync(command.UserId)).Returns(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result);
            A.CallTo(() => _userRepository.DeleteUserByIdAsync(command.UserId)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Handle_UserDeletionFails_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = _fixture.Create<DeleteUserCommand>();
            A.CallTo(() => _userRepository.DeleteUserByIdAsync(command.UserId)).Returns(false);

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, default));
            Assert.That(exception.Message, Is.EqualTo("Failed to delete the user."));
        }
    }
}
