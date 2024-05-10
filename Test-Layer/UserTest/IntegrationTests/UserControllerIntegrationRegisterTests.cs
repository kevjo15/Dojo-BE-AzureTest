using Application_Layer.Commands.RegisterNewUser;
using Application_Layer.Controllers;
using Application_Layer.DTO_s;
using Domain_Layer.Models.User;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    [TestFixture]
    public class UserControllerIntegrationRegisterTests
    {
        private IMediator _mediator;
        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _userController = new UserController(_mediator);
        }

        [Test]
        public async Task Register_ReturnsOk_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                Password = "Password123",
                ConfirmPassword = "Password123",
                Role = "Student"
            };

            var expectedUser = new UserModel
            {
                UserName = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Role = registerUserDTO.Role
            };

            A.CallTo(() => _mediator.Send(A<RegisterUserCommand>._, A<CancellationToken>._))
                .Returns(expectedUser);

            // Act
            var actionResult = await _userController.Register(registerUserDTO) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            Assert.NotNull(actionResult);
            var resultUser = actionResult.Value as UserModel;
            Assert.NotNull(resultUser);
            Assert.That(resultUser.Email, Is.EqualTo(expectedUser.Email));
            Assert.That(resultUser.FirstName, Is.EqualTo(expectedUser.FirstName));
            Assert.That(resultUser.LastName, Is.EqualTo(expectedUser.LastName));
            Assert.That(resultUser.Role, Is.EqualTo(expectedUser.Role));
        }
        [Test]
        public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                Email = "invalid-email",
                FirstName = "Test",
                LastName = "User",
                Password = "Password123",
                ConfirmPassword = "Password123",
                Role = "Student"
            };

            A.CallTo(() => _mediator.Send(A<RegisterUserCommand>._, A<CancellationToken>._))
                .Throws(new Exception("Registration failed due to invalid email."));

            // Act
            var actionResult = await _userController.Register(registerUserDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);

            var badRequestResult = actionResult as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.That(badRequestResult.Value, Is.EqualTo("Registration failed due to invalid email."));
        }

    }
}
