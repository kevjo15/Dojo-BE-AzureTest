using Application_Layer.Commands.DeleteUser;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.CommandTests
{
    [TestFixture]
    public class DeleteUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_DeleteUser_Corect_Id()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();

            var userToDeleteId = Guid.NewGuid().ToString();
            var isDeleted = true;
            var deleteUserCommand = new DeleteUserCommand(userToDeleteId);

            A.CallTo(() => userRepository.DeleteUserByIdAsync(A<string>._))
            .WithAnyArguments()
            .Returns(Task.FromResult(isDeleted));

            var handler = new DeleteUserCommandHandler(userRepository);
            //Act
            var result = await handler.Handle(deleteUserCommand, CancellationToken.None);
            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<bool>());
            A.CallTo(() => userRepository.DeleteUserByIdAsync(userToDeleteId)).MustHaveHappened();
        }
        [Test]
        public void Handle_DeleteUser_NotSuccessed()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var userToDeleteId = Guid.NewGuid().ToString();
            var isDeleted = false;

            var deleteUserCommand = new DeleteUserCommand(userToDeleteId);

            A.CallTo(() => userRepository.DeleteUserByIdAsync(A<string>._))
           .WithAnyArguments()
           .Returns(Task.FromResult(isDeleted));
            var handler = new DeleteUserCommandHandler(userRepository);
            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await handler.Handle(deleteUserCommand, CancellationToken.None);
            });
        }
    }
}
