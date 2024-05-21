using Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection;
using Domain_Layer.CommandOperationResult;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class CreateCourseHasModuleConnectionCommandHandlerTests
    {
        private CreateCourseHasModuleConnectionCommandHandler _handler;
        private ICourseRepository _courseRepository;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new CreateCourseHasModuleConnectionCommandHandler(_courseRepository);
        }

        [Test]
        public async Task Handle_ValidIds_ReturnsSuccess()
        {
            // Arrange
            var command = new CreateCourseHasModuleConnectionCommand("validCourseId", "validModuleId");
            A.CallTo(() => _courseRepository.ConnectCourseWithModuleAsync(command.CourseId, command.ModuleId)).Returns(new OperationResult<bool> { Success = true });

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("Module is successfully connected to Course"));
        }

        [Test]
        public async Task Handle_InvalidIds_ReturnsFailure()
        {
            // Arrange
            var command = new CreateCourseHasModuleConnectionCommand("", "");
            A.CallTo(() => _courseRepository.ConnectCourseWithModuleAsync(command.CourseId, command.ModuleId)).ThrowsAsync(new Exception("Invalid IDs"));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("Invalid course or module ID."));
        }

        [Test]
        public async Task Handle_RepositoryFailure_ReturnsFailure()
        {
            // Arrange
            var command = new CreateCourseHasModuleConnectionCommand("validCourseId", "validModuleId");

            A.CallTo(() => _courseRepository.ConnectCourseWithModuleAsync(command.CourseId, command.ModuleId))
              .Returns(new OperationResult<bool> { Success = false, Message = "Failed to connect course with module" });

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("Failed to connect course with module"));
        }
    }
}
