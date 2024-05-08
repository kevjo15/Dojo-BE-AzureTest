using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands;
using Application_Layer.Commands.CourseCommands.CreateCourse;
using Application_Layer.DTO_s;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationCreateCourseTest
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
        public async Task CreateCourse_ReturnsOk_WhenCourseSuccessfullyCreated()
        {
            // Arrange
            var courseDTO = new CreateCourseDTO
            {
                Title = "Test Course",
                UserId = "456808ed-883a-44dd-9c3d-6bf60469d168",
                CategoryOrSubject = "Mathematics",
                LevelOfDifficulty = "5/10",

            };
            A.CallTo(() => _mediator.Send(A<CreateCourseCommand>.That.Matches(c => c.CreateCourseDTO.Title == courseDTO.Title), default))
                .Returns(new CreateCourseResult { Success = true, Message = "Course successfully created" });

            // Act
            var result = await _controller.CreateCourse(courseDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Course successfully created"));
        }

        [Test]
        public async Task CreateCourse_ReturnsBadRequest_WhenCourseCreationFails()
        {
            // Arrange
            var courseDTO = new CreateCourseDTO();
            A.CallTo(() => _mediator.Send(A<CreateCourseCommand>.That.Matches(c => c.CreateCourseDTO == courseDTO), default))
                .Returns(new CreateCourseResult { Success = false, Message = "Failed to create the course." });

            // Act
            var result = await _controller.CreateCourse(courseDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to create the course."));
        }

        [Test]
        public async Task CreateCourse_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var courseDTO = new CreateCourseDTO();
            var exceptionMessage = "An error occurred";

            A.CallTo(() => _mediator.Send(A<CreateCourseCommand>.That.Matches(c => c.CreateCourseDTO == courseDTO), default))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.CreateCourse(courseDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
        }
    }
}
