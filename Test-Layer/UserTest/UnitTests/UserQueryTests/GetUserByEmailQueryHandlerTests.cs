using Application_Layer.Queries.GetUserByEmail;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitTests.UserQueryTests
{
    [TestFixture]
    public class GetUserByEmailQueryHandlerTests
    {
        private IUserRepository _userRepository;
        private GetUserByEmailQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _handler = new GetUserByEmailQueryHandler(_userRepository);
        }

        [Test, CustomAutoData]
        public void Handle_EmailNotFound_ThrowsKeyNotFoundException(GetUserByEmailQuery query)
        {
            // Arrange
            A.CallTo(() => _userRepository.GetUserByEmailAsync(query.Email))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Test, CustomAutoData]
        public void Handle_InvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            var query = new GetUserByEmailQuery("");

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("Email cannot be empty!", exception.Message);
        }

        [Test, CustomAutoData]
        public void Handle_EmailDoesNotExist_ThrowsKeyNotFoundException(string nonExistentEmail)
        {
            // Arrange
            var query = new GetUserByEmailQuery(nonExistentEmail);
            A.CallTo(() => _userRepository.GetUserByEmailAsync(nonExistentEmail))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            StringAssert.Contains(nonExistentEmail, exception.Message);
        }

    }
}
