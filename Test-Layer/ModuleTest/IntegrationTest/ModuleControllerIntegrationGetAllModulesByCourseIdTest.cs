using API_Layer.Controllers;
using Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse;
using Domain_Layer.Models.Module;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.IntegrationTest
{
    [TestFixture]
    public class ModuleControllerIntegrationGetAllModulesByCourseIdTest
    {
        private IMediator _mediator;
        private ModuleController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();

            _controller = new ModuleController(_mediator);
        }

        [Test]
        public async Task GetModulesByCourseId_ReturnsOk_WhenCourseExists()
        {
            // Arrange
            var courseId = "test-id";
            var modulesByCourseId = new List<ModuleModel>
            {
              new ModuleModel  { OrderInCourse = 1, ModuleTitle = "Introduction"}
            };

            A.CallTo(() => _mediator.Send(A<GetAllModulesByCourseIdQuery>.That.Matches(q => q.CourseId == courseId), default))
                .Returns(modulesByCourseId);

            // Act
            var result = await _controller.GetAllModulesByCourseId(courseId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(modulesByCourseId));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var returnedCourse = okResult.Value as List<ModuleModel>;
            Assert.NotNull(returnedCourse);
            Assert.That(returnedCourse.Count, Is.EqualTo(1));
            Assert.That(returnedCourse[0].ModuleTitle, Is.EqualTo("Introduction"));
        }

        [Test]
        public async Task GetModulesByCourseId_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            var courseId = "non-existent-id";

            A.CallTo(() => _mediator.Send(A<GetAllModulesByCourseIdQuery>.That.Matches(q => q.CourseId == courseId), default))
                .Returns(await Task.FromResult<List<ModuleModel>>(null)); // Simulate the course not being found

            // Act
            var result = await _controller.GetAllModulesByCourseId(courseId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo($"Course with ID {courseId} was not found."));
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task GetAllModulesByCourseId_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var courseId = "fakeId";
            A.CallTo(() => _mediator.Send(A<GetAllModulesByCourseIdQuery>.That.Matches(q => q.CourseId == courseId), default)).ThrowsAsync(new Exception("Unhandled exception"));

            // Act
            var result = await _controller.GetAllModulesByCourseId(courseId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Unhandled exception"));
        }
    }
}
