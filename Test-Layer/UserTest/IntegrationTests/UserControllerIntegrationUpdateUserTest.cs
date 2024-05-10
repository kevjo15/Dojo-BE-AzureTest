using System.Security.Claims;
using Application_Layer.Commands.UpdateUser;
using Application_Layer.Controllers;
using Application_Layer.DTO_s;
using Domain_Layer.Models.User;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationUpdateUserTest
    {
        private IMediator _mediator;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();
            _controller = new UserController(_mediator);

            // Skapa en fejkad ClaimsPrincipal och sätt den som User för din kontroller.
            var fakeClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "testUserId"),
                new Claim(ClaimTypes.Name, "test@example.com")
            };
            var fakeIdentity = new ClaimsIdentity(fakeClaims, "TestAuthType");
            var fakePrincipal = new ClaimsPrincipal(fakeIdentity);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = fakePrincipal }
            };
        }

        [Test]
        public async Task UpdateUser_ReturnsOk_WhenUserIsSuccessfullyUpdated()
        {
            // Arrange
            var userDto = new UpdatingUserDTO
            {
                Email = "test@example.com",
                CurrentPassword = "OldPassword123",
                NewPassword = "NewPassword123"
            };

            var updatedUser = new UserModel
            {
                Email = userDto.Email,
                FirstName = "UpdatedName",
                LastName = "UpdatedLastName"
            };

            A.CallTo(() => _mediator.Send(A<UpdateUserCommand>.That.Matches(c => c.UpdatingUserInfo.Email == userDto.Email), default))
                .Returns(updatedUser);

            // Act
            var result = await _controller.UpdateUser(userDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(updatedUser));

            // Verify detailed properties if necessary
            var resultUser = okResult.Value as UserModel;
            Assert.That(resultUser.FirstName, Is.EqualTo("UpdatedName"));
            Assert.That(resultUser.LastName, Is.EqualTo("UpdatedLastName"));
        }

        [Test]
        public async Task UpdateUser_ReturnsBadRequest_WhenUpdateFails()
        {
            // Arrange
            var userDto = new UpdatingUserDTO
            {
                Email = "fail@example.com",
                CurrentPassword = "OldPassword123",
                NewPassword = "NewPassword123"
            };

            A.CallTo(() => _mediator.Send(A<UpdateUserCommand>.Ignored, default))
                .Returns(Task.FromResult<UserModel>(null));

            // Act
            var result = await _controller.UpdateUser(userDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to update user information."));
        }
        [Test]
        public async Task UpdateUser_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var userDto = new UpdatingUserDTO
            {
                Email = "error@example.com",
                CurrentPassword = "currentPassword",
                NewPassword = "newPassword"
            };
            var expectedErrorMessage = "Error updating user.";

            A.CallTo(() => _mediator.Send(A<UpdateUserCommand>.That.Matches(u => u.UpdatingUserInfo.Email == userDto.Email), A<CancellationToken>.Ignored))
                .Throws(new Exception(expectedErrorMessage));

            // Act
            var result = await _controller.UpdateUser(userDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(expectedErrorMessage));
        }
    }
}
