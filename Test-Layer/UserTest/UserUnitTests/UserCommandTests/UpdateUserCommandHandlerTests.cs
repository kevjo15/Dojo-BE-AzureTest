using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Commands.UpdateUser;
using AutoFixture.NUnit3;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitUserTests.UserCommandTests
{
    [TestFixture]
    public class UpdateUserCommandHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_UserExists_UpdatesUserSuccessfully(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            [Frozen] Mock<IMapper> mapperMock,
            UpdateUserCommand command,
            UserModel user,
            UserModel updatedUser,
            UpdateUserCommandHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(x => x.GetUserByEmailAsync(command.UpdatingUserInfo.Email))
                              .ReturnsAsync(user);
            mapperMock.Setup(x => x.Map(command.UpdatingUserInfo, user))
                      .Returns(updatedUser); // Simulate mapping result
            userRepositoryMock.Setup(x => x.UpdateUserAsync(user, command.UpdatingUserInfo.CurrentPassword, command.UpdatingUserInfo.NewPassword))
                              .ReturnsAsync(updatedUser);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(updatedUser));
            userRepositoryMock.Verify(x => x.UpdateUserAsync(user, command.UpdatingUserInfo.CurrentPassword, command.UpdatingUserInfo.NewPassword), Times.Once);
        }

        [Test, CustomAutoData]
        public void Handle_UserDoesNotExist_ThrowsArgumentNullException(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            UpdateUserCommand command,
            UpdateUserCommandHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(x => x.GetUserByEmailAsync(command.UpdatingUserInfo.Email))
                              .ReturnsAsync((UserModel)null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
