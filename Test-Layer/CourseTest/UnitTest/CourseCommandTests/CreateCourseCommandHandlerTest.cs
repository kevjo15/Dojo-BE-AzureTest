using Application_Layer.Commands.CourseCommands;
using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.Course;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTest.CourseCommandTests
{
    [TestFixture]
    public class CreateCourseCommandHandlerTest
    {
        private ICourseRepository _courseRepository;
        private CreateCourseCommandHandler _handler;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new CreateCourseCommandHandler(_courseRepository, _mapper);
            _mapper = A.Fake<IMapper>();
        }

        [Test]
        public async Task Handler_GivenValidCourseDTO_ShouldCallAddCourseAsync()
        {
            // Arrange
            var courseDTO = new CreateCourseDTO
            {
                Title = "Test Course",
                UserId = "456808ed-883a-44dd-9c3d-6bf60469d168",
                CategoryOrSubject = "Mathematics",
                LevelOfDifficulty = "5/10",

            };

            // Act
            var result = await _handler.Handle(new CreateCourseCommand(courseDTO), CancellationToken.None);

            // Assert
            A.CallTo(() => _courseRepository.AddCourseAsync(A<CourseModel>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("Course successfully created"));
        }

        [Test]
        public async Task Handler_GivenInvalidCourseDTO_ShouldReturnFailureResult()
        {
            // Arrange
            var courseDTO = new CreateCourseDTO();
            A.CallTo(() => _mapper.Map<CourseModel>(A<CreateCourseDTO>.That.IsEqualTo(courseDTO))).Throws(new Exception("Error mapping course DTO"));

            // Act
            var result = await _handler.Handle(new CreateCourseCommand(courseDTO), CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains("An error occurred", result.Message);
        }
    }
}
