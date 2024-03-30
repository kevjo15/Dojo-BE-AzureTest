using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Queries.LoginUser;
using AutoFixture.NUnit3;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Repositories.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UserQueryTests
{
    [TestFixture]
    public class LoginUserQueryHandlerTests
    {
        [Test, CustomAutoData]
        public async Task Handle_SuccessfulLogin_ReturnsSuccessToken(
            [Frozen] Mock<IUserStore<UserModel>> userStoreMock,
            [Frozen] Mock<IUserClaimsPrincipalFactory<UserModel>> claimsFactoryMock,
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            UserModel user,
            LoginUserQuery query)
        {
            var userManagerMock = new Mock<UserManager<UserModel>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var signInManagerMock = new Mock<SignInManager<UserModel>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                claimsFactoryMock.Object,
                null, null, null, null);

            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
                .ReturnsAsync(SignInResult.Success);

            userRepositoryMock.Setup(x => x.GenerateJwtTokenAsync(It.IsAny<UserModel>()))
                .ReturnsAsync("TestToken");

            var sut = new LoginUserQueryHandler(
                signInManagerMock.Object,
                userManagerMock.Object,
                userRepositoryMock.Object);

            // Act
            var result = await sut.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.Successful);
            Assert.IsNotNull(result.Token);
            Assert.IsNull(result.Error);
        }



        [Test, CustomAutoData]
        public async Task Handle_InvalidCredentials_ReturnsFailure(
            [Frozen] Mock<IUserStore<UserModel>> userStoreMock,
            [Frozen] Mock<IUserClaimsPrincipalFactory<UserModel>> claimsFactoryMock,
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            UserModel user,
            LoginUserQuery query)
        {
            var userManagerMock = new Mock<UserManager<UserModel>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var signInManagerMock = new Mock<SignInManager<UserModel>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                claimsFactoryMock.Object,
                null, null, null, null);

            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, It.IsAny<string>(), false))
                .ReturnsAsync(SignInResult.Failed);

            var sut = new LoginUserQueryHandler(
                signInManagerMock.Object,
                userManagerMock.Object,
                userRepositoryMock.Object);

            var result = await sut.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.Successful);
            Assert.IsNull(result.Token);
            Assert.That(result.Error, Is.EqualTo("Invalid login attempt."));
        }

        [Test, CustomAutoData]
        public async Task Handle_UserNotFound_ReturnsFailure(
            [Frozen] Mock<IUserStore<UserModel>> userStoreMock,
            [Frozen] Mock<IUserClaimsPrincipalFactory<UserModel>> claimsFactoryMock,
            [Frozen] Mock<IUserRepository> userRepositoryMock,
            LoginUserQuery query)
        {
            var userManagerMock = new Mock<UserManager<UserModel>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var signInManagerMock = new Mock<SignInManager<UserModel>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                claimsFactoryMock.Object,
                null, null, null, null);

            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((UserModel)null);

            var sut = new LoginUserQueryHandler(
                signInManagerMock.Object,
                userManagerMock.Object,
                userRepositoryMock.Object);

            var result = await sut.Handle(query, CancellationToken.None);

            Assert.IsFalse(result.Successful);
            Assert.IsNull(result.Token);
            Assert.That(result.Error, Is.EqualTo("User not found."));
        }


    }
}
