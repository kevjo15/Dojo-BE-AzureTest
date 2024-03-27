using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Commands.DeleteUser;
using AutoFixture.NUnit3;
using Infrastructure_Layer.Repositories.User;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UserCommandTests
{
    [TestFixture]
    public class DeleteUserCommandHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_UserDeletionSucceeds_ReturnsTrue(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            DeleteUserCommand command,
            DeleteUserCommandHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(x => x.DeleteUserByIdAsync(command.UserId))
                              .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            userRepositoryMock.Verify(x => x.DeleteUserByIdAsync(command.UserId), Times.Once);
        }

        [Test, CustomAutoData]
        public void Handle_UserDeletionFails_ThrowsInvalidOperationException(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            DeleteUserCommand command,
            DeleteUserCommandHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(x => x.DeleteUserByIdAsync(command.UserId))
                              .ReturnsAsync(false); // Simulate failure

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
            Assert.That(exception.Message, Is.EqualTo("Failed to delete the user."));
        }
    }
}
