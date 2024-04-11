using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands.UpdateCourse;
using Application_Layer.DTO_s;
using Domain_Layer.Models.CourseModel;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationUpdateCourseTest
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
        public async Task UpdateCourse_ReturnsOk_WhenCourseSuccessfullyUpdated()
        {
            // Arrange
            var courseId = "existing-course-id";
            var courseUpdateDTO = new CourseUpdateDTO
            {
                Title = "Updated Title",
            };
            var updatedCourseResult = new CourseModel
            {
                CourseId = courseId,
                Title = courseUpdateDTO.Title,
            };

            A.CallTo(() => _mediator.Send(A<UpdateCourseCommand>.That.Matches(c => c.CourseId == courseId && c.CourseUpdateDTO == courseUpdateDTO), A<CancellationToken>.Ignored))
                .Returns(new OkObjectResult(updatedCourseResult));

            // Act
            var result = await _controller.UpdateCourse(courseId, courseUpdateDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            //var model = okResult.Value as CourseModel;
            //Assert.IsNotNull(model);
            //Assert.That(model.Title, Is.EqualTo(courseUpdateDTO.Title));
        }




        [Test]
        public async Task UpdateCourse_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            // Arrange
            var nonExistingCourseId = "non-existing-course-id";
            var courseUpdateDTO = new CourseUpdateDTO
            {

            };

            A.CallTo(() => _mediator.Send(A<UpdateCourseCommand>.That.Matches(c => c.CourseId == nonExistingCourseId), A<CancellationToken>._))
     .Returns(Task.FromResult<IActionResult>(null));


            // Act
            var result = await _controller.UpdateCourse(nonExistingCourseId, courseUpdateDTO);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult.Value, Is.EqualTo($"Course with ID {nonExistingCourseId} not found."));
        }
    }
}
