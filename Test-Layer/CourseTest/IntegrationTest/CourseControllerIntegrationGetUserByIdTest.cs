using API_Layer.Controllers;
using Application_Layer.Queries.CourseQueries.GetCourseById;
using Domain_Layer.Models.Course;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationGetUserByIdTest
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
        public async Task GetCourseById_ReturnsOk_WhenCourseExists()
        {
            // Arrange
            var courseId = "test-id";
            var course = new CourseModel { CourseId = courseId, CategoryOrSubject = "ASP.NET", Language = "English" };

            A.CallTo(() => _mediator.Send(A<GetCourseByIdQuery>.That.Matches(q => q.CourseId == courseId), default))
                .Returns(course);

            // Act
            var result = await _controller.GetCourseById(courseId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(course));
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            var returnedCourse = okResult.Value as CourseModel;
            Assert.NotNull(returnedCourse);
            Assert.That(returnedCourse.CourseId, Is.EqualTo(courseId));
            Assert.That(returnedCourse.CategoryOrSubject, Is.EqualTo("ASP.NET"));
            Assert.That(returnedCourse.Language, Is.EqualTo("English"));
        }
        [Test]
        public async Task GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            var courseId = "non-existent-id";

            A.CallTo(() => _mediator.Send(A<GetCourseByIdQuery>.That.Matches(q => q.CourseId == courseId), default))
                .Returns(await Task.FromResult<CourseModel>(null)); // Simulate the course not being found

            // Act
            var result = await _controller.GetCourseById(courseId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo($"Course with ID {courseId} was not found."));
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }
    }
}
