using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Commands.RegisterNewUser;
using AutoFixture.NUnit3;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitUserTests.UserCommandTests
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_ValidNewUser_ReturnsRegisteredUser(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            [Frozen] Mock<IMapper> mapperMock,
            RegisterUserCommand command,
            UserModel newUser,
            UserModel registeredUser,
            RegisterUserCommandHandler handler)
        {
            // Arrange
            mapperMock.Setup(x => x.Map<UserModel>(command.NewUser))
                      .Returns(newUser); // Simulate the mapping of DTO to UserModel
            userRepositoryMock.Setup(x => x.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role))
                              .ReturnsAsync(registeredUser);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(registeredUser));
            userRepositoryMock.Verify(x => x.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role), Times.Once);
        }

        [Test, CustomAutoData]
        public void Handle_InvalidNewUser_ThrowsArgumentNullException(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            [Frozen] Mock<IMapper> mapperMock,
            UserModel newUser,
            UserModel registeredUser,
            RegisterUserCommandHandler handler)
        {
            // Skapa en ny instans av RegisterUserCommand med null som NewUser direkt här
            var command = new RegisterUserCommand(null); // Antaget att konstruktören tillåter null

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command, CancellationToken.None));
        }



    }
}
