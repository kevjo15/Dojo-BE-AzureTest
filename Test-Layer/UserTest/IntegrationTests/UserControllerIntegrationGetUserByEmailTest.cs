using Application_Layer.Controllers;
using Application_Layer.Queries.GetUserByEmail;
using Domain_Layer.Models.User;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationGetUserByEmailTest
    {
        private IMediator _mediator;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();
            _controller = new UserController(_mediator);
        }

        [Test]
        public async Task GetUserByEmail_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var email = "test@example.com";
            var user = new UserModel { Id = "1", Email = email, FirstName = "Test", LastName = "User" };
            A.CallTo(() => _mediator.Send(A<GetUserByEmailQuery>.That.Matches(q => q.Email == email), default))
                .Returns(user);

            // Act
            var result = await _controller.GetUserByEmail(email);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var userModel = okResult.Value as UserModel;
            Assert.IsNotNull(userModel);
            Assert.That(userModel.Email, Is.EqualTo(email));
        }

        [Test]
        public async Task GetUserByEmail_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var email = "nonexistent@example.com";
            A.CallTo(() => _mediator.Send(A<GetUserByEmailQuery>.That.Matches(q => q.Email == email), default))
                .Returns(Task.FromResult<UserModel>(null));

            // Act
            var result = await _controller.GetUserByEmail(email);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult.Value, Is.EqualTo($"User with Email {email} was not found."));
        }
    }
}
