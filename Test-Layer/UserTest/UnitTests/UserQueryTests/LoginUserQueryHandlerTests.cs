using Application_Layer.Queries.LoginUser;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitTests.UserQueryTests
{
    [TestFixture]
    public class LoginUserQueryHandlerTests
    {
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;
        private IUserRepository _userRepository;
        private LoginUserQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            var userStore = A.Fake<IUserStore<UserModel>>();
            _userManager = A.Fake<UserManager<UserModel>>(
                options => options.WithArgumentsForConstructor(() => new UserManager<UserModel>(
                    userStore, null, null, null, null, null, null, null, null)));
            var claimsFactory = A.Fake<IUserClaimsPrincipalFactory<UserModel>>();
            _signInManager = A.Fake<SignInManager<UserModel>>(
                options => options.WithArgumentsForConstructor(() => new SignInManager<UserModel>(
                    _userManager, A.Fake<IHttpContextAccessor>(), claimsFactory, null, null, null, null)));
            _userRepository = A.Fake<IUserRepository>();

            _sut = new LoginUserQueryHandler(_signInManager, _userManager, _userRepository);
        }

        [Test, CustomAutoData]
        public async Task Handle_SuccessfulLogin_ReturnsSuccessToken(UserModel user, LoginUserQuery query)
        {
            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(user);
            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(user, A<string>.Ignored, false))
                .Returns(SignInResult.Success);
            A.CallTo(() => _userRepository.GenerateJwtTokenAsync(A<UserModel>.Ignored))
                .Returns("TestToken");

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsTrue(result.Successful);
            Assert.IsNotNull(result.Token);
            Assert.IsNull(result.Error);
        }

        [Test, CustomAutoData]
        public async Task Handle_InvalidCredentials_ReturnsFailure(UserModel user, LoginUserQuery query)
        {
            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored))
                .Returns(user);
            A.CallTo(() => _signInManager.CheckPasswordSignInAsync(user, A<string>.Ignored, false))
                .Returns(SignInResult.Failed);

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.Successful);
            Assert.IsNull(result.Token);
            Assert.That(result.Error, Is.EqualTo("Invalid login attempt."));
        }

        [Test, CustomAutoData]
        public async Task Handle_UserNotFound_ReturnsFailure(LoginUserQuery query)
        {
            A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored))
                .Returns((UserModel)null);

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.Successful);
            Assert.IsNull(result.Token);
            Assert.That(result.Error, Is.EqualTo("User not found."));
        }


    }
}
