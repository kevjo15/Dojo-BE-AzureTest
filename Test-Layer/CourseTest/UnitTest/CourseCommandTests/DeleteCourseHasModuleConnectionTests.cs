using System.Reflection.Metadata;
using API_Layer.Controllers;
using Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.UnitTest.CourseCommandTests
{
    public class DeleteCourseHasModuleConnectionTests
    {
        //private IMediator _mediator;
        //private CourseController _controller;

        //[SetUp]
        //public void Setup()
        //{
        //    _mediator = A.Fake<IMediator>();
        //    _controller = new CourseController(_mediator);

        //}
        //[Test]
        //public async Task Handle_Successful_Deletion_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var courseId = "valid-course-id";
        //    var moduleId = "valid-module-id";
        //    var expectedResult = new DeleteCourseHasModuleConnectionResult { Success = true, Message = "Connection is successfully deleted" };

        //    var mockRepo = A.Fake<ICourseRepository>();
        //    A.CallTo(() => mockRepo.DeleteCourseHasModuleConnection(courseId, moduleId)).Returns(Task.CompletedTask);

        //    var handler = new DeleteCourseHasModuleConnectionCommandHandler(mockRepo);

        //    // Act
        //    var result = await handler.Handle(new DeleteCourseHasModuleConnectionCommand(courseId, moduleId), CancellationToken.None);

        //    // Assert
        //    Assert.True(result.Success);
        //    Assert.That(result.Message, Is.EqualTo(expectedResult.Message));
        //}


        //[Test]
        //public async Task DeleteCourse_ReturnsBadRequest_WhenInvalidInputProvided()
        //{
        //    //// Arrange
        //    //var courseId = "";
        //    //var moduleId = "existing-module-id";

        //    //// Act
        //    //var result = await _controller.DeleteCourseHasModuleConnection(courseId, moduleId);

        //    //// Assert
        //    //Assert.IsInstanceOf<BadRequestObjectResult>(result);
        //    //var badRequestResult = result as BadRequestObjectResult;
        //    //Assert.That(badRequestResult.Value, Is.EqualTo("CourseId or ModuleId cannot be empty"));
        //}

        //[Test]
        //public async Task DeleteCourse_ReturnsInternalServerError_WhenExceptionOccursDuringDeletion()
        //{
        //    // Arrange
        //    var courseId = "existing-course-id";
        //    var moduleId = "existing-module-id";
        //    var exceptionMessage = "An unexpected error occurred.";
        //    A.CallTo(() => _mediator.Send(A<DeleteCourseHasModuleConnectionCommand>.That.Matches(c => c.CourseId == courseId && c.ModuleId == moduleId), default))
        //   .Throws(new Exception(exceptionMessage));

        //    // Act
        //    var result = await _controller.DeleteCourseHasModuleConnection(courseId, moduleId);

        //    // Assert
        //    Assert.IsInstanceOf<StatusCodeResult>(result);
        //    var statusResult = result as StatusCodeResult;
        //    Assert.That(statusResult.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
        //}
        private DeleteCourseHasModuleConnectionCommandHandler _handler;
        private ICourseRepository _courseRepository;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new DeleteCourseHasModuleConnectionCommandHandler(_courseRepository);
        }

        [Test]
        public async Task Handle_Successful_Deletion_ReturnsExpectedResult()
        {
            // Arrange
            var expectedResult = new DeleteCourseHasModuleConnectionResult { Success = true, Message = "Connection is successfully deleted" };

            var command = new DeleteCourseHasModuleConnectionCommand("validCourseId", "validModuleId");
            A.CallTo(() => _courseRepository.DeleteCourseHasModuleConnection(command.CourseId, command.ModuleId)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo(expectedResult.Message));
        }

        [Test]
        public async Task Handle_EmptyIds_ReturnsFailure()
        {
            // Arrange
            var command = new DeleteCourseHasModuleConnectionCommand("", "");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("CourseId or ModuleId cannot be empty!"));
        }
        [Test]
        public async Task Handle_DeletionFailure_ReturnsFailure()
        {
            // Arrange
            var command = new DeleteCourseHasModuleConnectionCommand("validCourseId", "validModuleId");
            A.CallTo(() => _courseRepository.DeleteCourseHasModuleConnection(command.CourseId, command.ModuleId)).ThrowsAsync(new Exception("Deletion failed"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("An error occurred: Deletion failed"));
        }
    }
}
