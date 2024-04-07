using Application_Layer.DTO_s;
using Application_Layer.Queries.LoginUser;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Microsoft.AspNetCore.Identity;

namespace Test_Layer.UserTests.QueryTests
{
    [TestFixture]
    public class LoginUserQueryHandlerTests
    {
        private IUserRepository _userRepository;
        private LoginUserQueryHandler _handler;
        private SignInManager<UserModel> _signInManager;
        private UserManager<UserModel> _userManager;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _signInManager = A.Fake<SignInManager<UserModel>>();
            _userManager = A.Fake<UserManager<UserModel>>();
            _handler = new LoginUserQueryHandler(_signInManager, _userManager, _userRepository);
        }
        [Test]
        public async Task Handle_LoginUserQuery_Returns_Successful_LoginResult_When_User_Found_And_Password_Correct()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var lockoutOnFailure = false;
            var user = new UserModel { Email = email, UserName = "TestUser" };
            A.CallTo(() => _userManager.FindByEmailAsync(email)).Returns(user);
            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure)).Returns(SignInResult.Success);
            A.CallTo(() => _userRepository.GenerateJwtTokenAsync(user)).Returns("token");

            var request = new LoginUserQuery(new LoginUserDTO { Email = email, Password = password });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.Successful);
            Assert.IsNull(result.Error);
            Assert.IsNotNull(result.Token);
        }

        [Test]
        public async Task Handle_LoginUserQuery_Returns_Unsuccessful_LoginResult_When_User_NotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            A.CallTo(() => _userManager.FindByEmailAsync(email)).Returns((UserModel)null!);

            var request = new LoginUserQuery(new LoginUserDTO { Email = email, Password = "password" });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Successful);
            Assert.That(result.Error, Is.EqualTo("User not found."));
            Assert.IsNull(result.Token);
        }

        [Test]
        public async Task Handle_LoginUserQuery_Returns_Unsuccessful_LoginResult_When_Password_Incorrect()
        {
            // Arrange
            var email = "test@example.com";
            var password = "wrongpassword";
            var lockoutOnFailure = false;
            var user = new UserModel { Email = email, UserName = "TestUser" };
            A.CallTo(() => _userManager.FindByEmailAsync(email)).Returns(user);
            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure)).Returns(SignInResult.Failed);

            var request = new LoginUserQuery(new LoginUserDTO { Email = email, Password = password });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Successful);
            Assert.That(result.Error, Is.EqualTo("Invalid login attempt."));
            Assert.IsNull(result.Token);
        }

    }
}
