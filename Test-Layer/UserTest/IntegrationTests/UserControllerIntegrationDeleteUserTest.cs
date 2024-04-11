using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Commands.DeleteUser;
using Application_Layer.Controllers;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationDeleteUserTest
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
        public async Task DeleteUser_ReturnsOk_WhenUserSuccessfullyDeleted()
        {
            // Arrange
            var userId = "existing-user-id";
            A.CallTo(() => _mediator.Send(A<DeleteUserCommand>.That.Matches(c => c.UserId == userId), default))
                .Returns(true);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("User successfully deleted."));
        }

        [Test]
        public async Task DeleteUser_ReturnsBadRequest_WhenUserDeletionFails()
        {
            // Arrange
            var userId = "non-existing-user-id";
            A.CallTo(() => _mediator.Send(A<DeleteUserCommand>.That.Matches(c => c.UserId == userId), default))
                .Returns(false);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to delete the user."));
        }

        [Test]
        public async Task DeleteUser_ReturnsUnauthorized_WhenUserIdIsEmpty()
        {
            // Act
            var result = await _controller.DeleteUser(string.Empty);

            // Assert
            Assert.IsInstanceOf<UnauthorizedObjectResult>(result);
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
            Assert.That(unauthorizedResult.Value, Is.EqualTo("User is not recognized."));
        }
    }
}
