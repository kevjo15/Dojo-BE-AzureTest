using Application_Layer.Commands.CourseCommands.UpdateCourse;
using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.CourseModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.UnitTest.CourseCommandTests
{
    [TestFixture]
    public class UpdateCourseCommandHandlerTest
    {
        private ICourseRepository _courseRepository;
        private IMapper _mapper;
        private UpdateCourseCommandHandler _handler;
        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _mapper = A.Fake<IMapper>();
            _handler = new UpdateCourseCommandHandler(_courseRepository, _mapper);
        }

        [Test]
        public async Task Given_ValidCourseId_ShouldReturnOkResult()
        {
            // Arrange
            var courseId = Guid.NewGuid().ToString();
            var courseUpdateDTO = new CourseUpdateDTO
            {
                Title = "New Title",
                CategoryOrSubject = "New Category",
                LevelOfDifficulty = "Intermediate",
                PriceOrPriceModel = "Free",
                EnrolmentStatus = "Open",
                Language = "French",
                Duration = TimeSpan.FromHours(1),
                ThumbnailOrImageUrl = "http://example.com/newimage.jpg",
                ContentUrl = "http://example.com/newcontent",
                Tags = "New, Course, Tags",
                Prerequisites = "None",
                CourseIsPublic = true,
                CourseIsCompleted = false,
                IssueCertificate = true
            };
            var courseModel = new CourseModel { CourseId = courseId };

            A.CallTo(() => _courseRepository.GetCourseByIdAsync(courseId)).Returns(Task.FromResult(courseModel));
            A.CallTo(() => _courseRepository.UpdateCourseAsync(A<CourseModel>.That.Matches(m => m.CourseId == courseId))).Returns(Task.FromResult(true));

            A.CallTo(() => _mapper.Map(A<CourseUpdateDTO>.That.IsEqualTo(courseUpdateDTO), A<CourseModel>.Ignored))
               .Invokes((CourseUpdateDTO source, CourseModel destination) => {
                   destination.Title = source.Title;
                   destination.CategoryOrSubject = source.CategoryOrSubject;
                   destination.LevelOfDifficulty = source.LevelOfDifficulty;
                   destination.PriceOrPriceModel = source.PriceOrPriceModel;
                   destination.EnrolmentStatus = source.EnrolmentStatus;
                   destination.Language = source.Language;
                   destination.Duration = source.Duration;
                   destination.ThumbnailOrImageUrl = source.ThumbnailOrImageUrl;
                   destination.ContentUrl = source.ContentUrl;
                   destination.Tags = source.Tags;
                   destination.Prerequisites = source.Prerequisites;
                   destination.CourseIsPublic = source.CourseIsPublic;
                   destination.CourseIsCompleted = source.CourseIsCompleted;
                   destination.IssueCertificate = source.IssueCertificate;
               });

            var command = new UpdateCourseCommand(courseUpdateDTO, courseId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedCourse = okResult.Value as CourseModel;
            Assert.IsNotNull(updatedCourse);

            Assert.That(updatedCourse.Title, Is.EqualTo(courseUpdateDTO.Title));
            Assert.That(updatedCourse.CategoryOrSubject, Is.EqualTo(courseUpdateDTO.CategoryOrSubject));
            Assert.That(updatedCourse.LevelOfDifficulty, Is.EqualTo(courseUpdateDTO.LevelOfDifficulty));
            Assert.That(updatedCourse.PriceOrPriceModel, Is.EqualTo(courseUpdateDTO.PriceOrPriceModel));
            Assert.That(updatedCourse.EnrolmentStatus, Is.EqualTo(courseUpdateDTO.EnrolmentStatus));
            Assert.That(updatedCourse.Language, Is.EqualTo(courseUpdateDTO.Language));
            Assert.That(updatedCourse.Duration, Is.EqualTo(courseUpdateDTO.Duration));
            Assert.That(updatedCourse.ThumbnailOrImageUrl, Is.EqualTo(courseUpdateDTO.ThumbnailOrImageUrl));
            Assert.That(updatedCourse.ContentUrl, Is.EqualTo(courseUpdateDTO.ContentUrl));
            Assert.That(updatedCourse.Tags, Is.EqualTo(courseUpdateDTO.Tags));
            Assert.That(updatedCourse.Prerequisites, Is.EqualTo(courseUpdateDTO.Prerequisites));
            Assert.That(updatedCourse.CourseIsPublic, Is.EqualTo(courseUpdateDTO.CourseIsPublic));
            Assert.That(updatedCourse.CourseIsCompleted, Is.EqualTo(courseUpdateDTO.CourseIsCompleted));
            Assert.That(updatedCourse.IssueCertificate, Is.EqualTo(courseUpdateDTO.IssueCertificate));

        }

        [Test]
        public async Task Given_InvalidCourseId_ShouldReturnNotFoundResult()
        {
            // Arrange
            var invalidCourseId = Guid.NewGuid().ToString();
            var courseUpdateDTO = new CourseUpdateDTO();

            A.CallTo(() => _courseRepository.GetCourseByIdAsync(invalidCourseId)).Returns(Task.FromResult<CourseModel>(null));

            var command = new UpdateCourseCommand(courseUpdateDTO, invalidCourseId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Given_ValidCourseId_And_RepositoryThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            var courseId = Guid.NewGuid().ToString();
            var courseUpdateDTO = new CourseUpdateDTO();

            A.CallTo(() => _courseRepository.GetCourseByIdAsync(courseId)).ThrowsAsync(new Exception("Database error"));

            var command = new UpdateCourseCommand(courseUpdateDTO, courseId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
