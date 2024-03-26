using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Queries.GetUserByEmail;
using AutoFixture.NUnit3;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UserQueryTests
{
    [TestFixture]
    public class GetUserByEmailQueryHandlerTests
    {
        [Test, CustomAutoData]
        public void Handle_EmailNotFound_ThrowsKeyNotFoundException(
         [Frozen] Mock<IUserRepository> userRepositoryMock,
          GetUserByEmailQuery query,
           GetUserByEmailQueryHandler handler)
        {
            // Arrange
            userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(query.Email))
                              .ReturnsAsync((UserModel)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
        [Test, CustomAutoData]
        public void Handle_InvalidEmail_ThrowsArgumentException(
            GetUserByEmailQueryHandler handler)
        {
            // Arrange - Skapar en ny instans med en tom sträng som ogiltig e-postadress
            var query = new GetUserByEmailQuery("");

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("Email cannot be empty!", exception.Message);
        }

        [Test, CustomAutoData]
        public void Handle_EmailDoesNotExist_ThrowsKeyNotFoundException(
    [Frozen] Mock<IUserRepository> userRepositoryMock,
    GetUserByEmailQueryHandler handler,
    string nonExistentEmail)
        {
            // Arrange
            var query = new GetUserByEmailQuery(nonExistentEmail);
            userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(nonExistentEmail))
                              .ReturnsAsync((UserModel)null); // Simulera att ingen användare hittas

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));

            // Kontrollera att exception-meddelandet innehåller den e-postadress som inte kunde hittas
            StringAssert.Contains(nonExistentEmail, exception.Message);
        }

    }
}
