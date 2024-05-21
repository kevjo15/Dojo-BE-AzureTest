using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{

    internal class CourseControllerIntegrationDeleteCourseHasModuleConnectionTest
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
        public async Task DeleteCourseHasModuleConnection_ReturnsOk_WhenCourseAndModuleConnectionIsSuccessfullyDeleted()
        {
            // Arrange
            var courseId = "existing-course-id";
            var moduleId = "existing-module-id";
            A.CallTo(() => _mediator.Send(A<DeleteCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
               .Returns(new DeleteCourseHasModuleConnectionResult { Success = true, Message = "Course and related modules successfully deleted" });

            // Act
            var result = await _controller.DeleteCourseHasModuleConnection(courseId, moduleId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Course and related modules successfully deleted"));
        }

        [Test]
        public async Task DeleteCourseHasModuleConnection_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var courseId = "existing-course-id";
            var moduleId = "existing-module-id";
            var exceptionMessage = "An unexpected error occurred.";

            A.CallTo(() => _mediator.Send(A<DeleteCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
               .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.DeleteCourseHasModuleConnection(courseId, moduleId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
        }
        [Test]
        public async Task DeleteCourseHasModuleConnection_ReturnsBadRequest_WhenSuccessIsFalse()
        {
            // Arrange
            var courseId = "existing-course-id";
            var moduleId = "existing-module-id";
            var errorMessage = "Course and module deletion failed.";

            A.CallTo(() => _mediator.Send(A<DeleteCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
               .Returns(new DeleteCourseHasModuleConnectionResult { Success = false, Message = errorMessage });

            // Act
            var result = await _controller.DeleteCourseHasModuleConnection(courseId, moduleId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(errorMessage));
        }

    }
}
