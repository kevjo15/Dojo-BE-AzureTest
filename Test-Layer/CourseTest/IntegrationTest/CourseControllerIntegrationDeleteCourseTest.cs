using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands.DeleteCourse;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationDeleteCourseTest
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
        public async Task DeleteCourse_ReturnsOk_WhenCourseSuccessfullyDeleted()
        {
            // Arrange
            var courseId = "existing-course-id";
            A.CallTo(() => _mediator.Send(A<DeleteCourseCommand>.That.Matches(c => c.CourseId == courseId), default))
                .Returns(new DeleteCourseResult { Success = true, Message = "Course and related modules successfully deleted" });

            // Act
            var result = await _controller.DeleteCourse(courseId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Course and related modules successfully deleted"));
        }

        [Test]
        public async Task DeleteCourse_ReturnsBadRequest_WhenCourseDeletionFails()
        {
            // Arrange
            var courseId = "non-existing-course-id";
            A.CallTo(() => _mediator.Send(A<DeleteCourseCommand>.That.Matches(c => c.CourseId == courseId), default))
                .Returns(new DeleteCourseResult { Success = false, Message = "Failed to delete the course." });

            // Act
            var result = await _controller.DeleteCourse(courseId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to delete the course."));
        }
        [Test]
        public async Task DeleteCourse_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var courseId = "course-id-that-causes-exception";
            var exceptionMessage = "An error occurred";

            A.CallTo(() => _mediator.Send(A<DeleteCourseCommand>.That.Matches(c => c.CourseId == courseId), default))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.DeleteCourse(courseId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
        }
    }
}
