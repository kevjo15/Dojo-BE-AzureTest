using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection;
using Domain_Layer.CommandOperationResult;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationCreateCourseHasModulesConnectionTest
    {
        private IMediator _mediator;
        private CourseController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();
            _controller = new CourseController(_mediator);
        }

        [Test]
        public async Task CreateCourseHasModulesConnection_ReturnsOk_WhenOperationSucceeds()
        {
            // Arrange
            var courseId = "validCourseId";
            var moduleId = "validModuleId";
            var successMessage = "Course successfully connected to module.";
            A.CallTo(() => _mediator.Send(A<CreateCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
           .Returns(new OperationResult<bool> { Success = true, Message = successMessage });

            // Act
            var result = await _controller.CreateCourseHasModulesConnection(courseId, moduleId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            // Directly compare the entire result object if the method returns the entire OperationResult<bool>
            Assert.That(okResult.Value, Is.EqualTo(successMessage));
        }

        [Test]
        public async Task CreateCourseHasModulesConnection_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var courseId = "validCourseId";
            var moduleId = "validModuleId";
            var exceptionMessage = "An unexpected error occurred.";
            // Simulate an exception being thrown by the mediator
            A.CallTo(() => _mediator.Send(A<CreateCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
             .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.CreateCourseHasModulesConnection(courseId, moduleId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
        }
    }
}

