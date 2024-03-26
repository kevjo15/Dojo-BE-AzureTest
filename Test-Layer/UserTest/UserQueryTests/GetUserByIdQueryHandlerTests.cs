using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Queries.GetUserById;
using AutoFixture.NUnit3;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using MediatR;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UserQueryTests
{
    [TestFixture]
    public class GetUserByIdQueryHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_ValidUserId_ReturnsUser(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            GetUserByIdQuery query,
            UserModel expectedUser,
            GetUserByIdQueryHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(query.UserId))
                              .ReturnsAsync(expectedUser);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(expectedUser, result);
            userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(query.UserId), Times.Once);
        }

        [Test, CustomAutoData]
        public void Handle_NonExistentUserId_ThrowsKeyNotFoundException(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            GetUserByIdQuery query,
            GetUserByIdQueryHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(query.UserId))
                              .ReturnsAsync((UserModel)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));

            // Kontrollera att felmeddelandet innehåller det angivna användar-ID:t
            StringAssert.Contains(query.UserId, exception.Message);
        }

        [Test, CustomAutoData]
        public void Handle_EmptyUserId_ThrowsArgumentException(
            GetUserByIdQueryHandler handler)
        {
            // Arrange
            var query = new GetUserByIdQuery("");

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("UserId cannot be empty", exception.Message);
        }
    }

}
