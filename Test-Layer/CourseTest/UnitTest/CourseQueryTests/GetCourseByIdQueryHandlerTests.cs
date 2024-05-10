using Application_Layer.Queries.CourseQueries.GetCourseById;
using Domain_Layer.Models.Course;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTests.CourseQueryTests
{
    [TestFixture]
    public class GetCourseByIdQueryHandlerTests
    {
        private ICourseRepository _courseRepository;
        private GetCourseByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new GetCourseByIdQueryHandler(_courseRepository);
        }

        [Test]
        public async Task Handle_ValidCourseId_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var expectedCourse = new CourseModel { CourseId = courseId, CategoryOrSubject = "ASP.NET", CourseIsCompleted = true, Language = "English" };
            var query = new GetCourseByIdQuery(courseId);

            A.CallTo(() => _courseRepository.GetCourseByIdAsync(query.CourseId))
                .Returns(expectedCourse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.That(result.CourseId, Is.EqualTo(expectedCourse.CourseId));
            Assert.That(result.CategoryOrSubject, Is.EqualTo(expectedCourse.CategoryOrSubject));
            Assert.That(result.Language, Is.EqualTo(expectedCourse.Language));
            A.CallTo(() => _courseRepository.GetCourseByIdAsync(query.CourseId)).MustHaveHappenedOnceExactly();
        }
        [Test]
        public void Handle_NonExistentCourseId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var query = new GetCourseByIdQuery(courseId);

            A.CallTo(() => _courseRepository.GetCourseByIdAsync(query.CourseId))
                .Returns(Task.FromResult<CourseModel>(null));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            StringAssert.Contains(query.CourseId, exception.Message);
        }
        [Test]
        public void Handle_EmptyCourseId_ThrowsArgumentException()
        {
            var courseId = "";
            // Arrange
            var query = new GetCourseByIdQuery(courseId);

            // Act & Assert
            var exception = Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
            StringAssert.Contains($"Course with ID {courseId} was not found!", exception.Message);
        }
    }
}
