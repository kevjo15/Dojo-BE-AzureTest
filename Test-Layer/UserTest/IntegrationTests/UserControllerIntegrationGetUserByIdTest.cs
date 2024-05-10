using Application_Layer.Controllers;
using Application_Layer.Queries.GetUserById;
using Domain_Layer.Models.User;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationGetUserByIdTest
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
        public async Task GetUserById_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = "test-id";
            var user = new UserModel { Id = userId, FirstName = "Test", LastName = "User" };

            A.CallTo(() => _mediator.Send(A<GetUserByIdQuery>.That.Matches(q => q.UserId == userId), default))
                .Returns(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(user));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var returnedUser = okResult.Value as UserModel;
            Assert.NotNull(returnedUser);
            Assert.That(returnedUser.Id, Is.EqualTo(userId));
            Assert.That(returnedUser.FirstName, Is.EqualTo("Test"));
            Assert.That(returnedUser.LastName, Is.EqualTo("User"));

        }

        [Test]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "non-existent-id";

            A.CallTo(() => _mediator.Send(A<GetUserByIdQuery>.That.Matches(q => q.UserId == userId), default))
                .Returns(Task.FromResult<UserModel>(null));

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult.Value, Is.EqualTo($"User with ID {userId} was not found."));
        }
    }
}
