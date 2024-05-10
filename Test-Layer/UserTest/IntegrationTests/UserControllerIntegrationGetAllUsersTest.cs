using Application_Layer.Controllers;
using Application_Layer.Queries.GetAllUsers;
using Domain_Layer.Models.User;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.UserTest.IntegrationTests
{
    public class UserControllerIntegrationGetAllUsersTest
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
        public async Task GetAllUsers_ReturnsOk_WhenUsersExist()
        {
            // Arrange
            var users = new List<UserModel>
            {
                new UserModel { Id = "1", FirstName = "Test", LastName = "User" },
                new UserModel { Id = "2", FirstName = "Another", LastName = "User" }
            };

            A.CallTo(() => _mediator.Send(A<GetAllUsersQuery>.Ignored, default))
                .Returns(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EquivalentTo(users));

            var returnedUsers = okResult.Value as List<UserModel>;
            Assert.IsNotNull(returnedUsers);
            Assert.That(returnedUsers.Count, Is.EqualTo(users.Count));
            for (int i = 0; i < returnedUsers.Count; i++)
            {
                Assert.That(returnedUsers[i].Id, Is.EqualTo(users[i].Id));
                Assert.That(returnedUsers[i].FirstName, Is.EqualTo(users[i].FirstName));
                Assert.That(returnedUsers[i].LastName, Is.EqualTo(users[i].LastName));
            }
        }

        [Test]
        public async Task GetAllUsers_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            A.CallTo(() => _mediator.Send(A<GetAllUsersQuery>.Ignored, default))
                .Throws(new System.Exception("Internal Server Error"));

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Internal Server Error"));
        }
    }
}
