using Application_Layer.Commands.CourseCommands.DeleteCourse;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTest.CourseCommandTests
{
    [TestFixture]
    public class DeleteCourseCommandHandlerTests
    {
        private ICourseRepository _courseRepository;
        private DeleteCourseCommandHandler _handler;
        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new DeleteCourseCommandHandler(_courseRepository);
        }

        [Test]
        public async Task Handler_GivenValidCourseId_ShouldCallDeleteCourseByIdAsync()
        {
            // Arrange
            var courseId = Guid.NewGuid().ToString();
            // Act
            var result = await _handler.Handle(new DeleteCourseCommand(courseId), CancellationToken.None);
            // Assert
            A.CallTo(() => _courseRepository.DeleteCourseByIdAsync(courseId)).MustHaveHappenedOnceExactly();
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("Course and related modules successfully deleted"));
        }
        [Test]
        public async Task Handler_GivenInvalidCourseId_ShouldReturnFailureResult()
        {
            // Arrange
            var courseId = "InvalidId";
            A.CallTo(() => _courseRepository.DeleteCourseByIdAsync(courseId)).Throws(new Exception("Course not found"));
            // Act
            var result = await _handler.Handle(new DeleteCourseCommand(courseId), CancellationToken.None);
            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains("An error occurred", result.Message);
        }
        [Test]
        public void Handler_GivenEmptyCourseId_shouldReturnArgumentException()
        {
            // Arrange
            var courseId = string.Empty;
            A.CallTo(() => _courseRepository.DeleteCourseByIdAsync(courseId)).Throws(new ArgumentException("CourseId cannot be empty!"));
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(new DeleteCourseCommand(courseId), CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("CourseId cannot be empty!"));
        }
    }
}
