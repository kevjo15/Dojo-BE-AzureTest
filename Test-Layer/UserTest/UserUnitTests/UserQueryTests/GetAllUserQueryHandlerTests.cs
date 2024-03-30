using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Queries.GetAllUsers;
using AutoFixture.NUnit3;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UserQueryTests
{
    [TestFixture]
    public class GetAllUsersQueryHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_CallsGetAllUsersAsync_ReturnsAllUsers(
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            IEnumerable<UserModel> expectedUsers,
            GetAllUsersQueryHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
                              .ReturnsAsync(expectedUsers);

            // Act
            var result = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedUsers));
            userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
        }

        [Test, CustomAutoData]
        public void Handle_WhenRepositoryThrowsException_ThrowsException(
         [Frozen] Mock<IUserRepository> userRepositoryMock,
         GetAllUsersQueryHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
                              .ThrowsAsync(new Exception("Database connection failed"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => handler.Handle(new GetAllUsersQuery(), CancellationToken.None),
                "Expected GetAllUsersQueryHandler to throw an Exception when UserRepository throws an exception, but it did not.");
        }
    }
}
