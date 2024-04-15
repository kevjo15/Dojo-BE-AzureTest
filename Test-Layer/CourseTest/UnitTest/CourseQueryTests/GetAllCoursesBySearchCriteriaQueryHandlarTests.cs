using Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria;
using Domain_Layer.Models.CourseModel;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTest.CourseQueryTests
{
    [TestFixture]
    public class GetAllCoursesBySearchCriteriaQueryHandlarTests
    {

        private ICourseRepository _courseRepository;
        private GetAllCoursesBySearchCriteriaQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _courseRepository = A.Fake<ICourseRepository>();
            _handler = new GetAllCoursesBySearchCriteriaQueryHandler(_courseRepository);
        }

        [Test]
        public async Task Handle_SearchCriteria_ValidCourseId_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var courseId2 = "08260480-52a0-4c0e-a588-274101a2c3be";
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = courseId, CategoryOrSubject = "ASP.NET", CourseIsCompleted = true, Language = "English" },
              new CourseModel { CourseId = courseId2 , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.CourseId == courseId).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(courseId);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(A<string>.Ignored))
                .Returns(allCourses.Where(c => c.CourseId == courseId).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of CourseModel objects
            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(expectedCourse, result); //compare both lists directly for equality
        }
        [Test]
        public async Task Handle_SearchCriteria_ValidCategoryOrSubject_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var courseId2 = "08260480-52a0-4c0e-a588-274101a2c3be";
            var category = "ASP.NET";
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = courseId, CategoryOrSubject = category, CourseIsCompleted = true, Language = "English" },
              new CourseModel { CourseId = courseId2 , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.CategoryOrSubject == category).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(category);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(A<string>.Ignored))
                .Returns(allCourses.Where(c => c.CategoryOrSubject == category).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of CourseModel objects
            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(expectedCourse, result); //compare both lists directly for equality
        }
        [Test]
        public async Task Handle_SearchCriteria_ValidLanguage_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var courseId2 = "08260480-52a0-4c0e-a588-274101a2c3be";
            var category = "ASP.NET";
            var language = "English";
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = courseId, CategoryOrSubject = category, CourseIsCompleted = true, Language = language },
              new CourseModel { CourseId = courseId2 , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.Language == language).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(language);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(A<string>.Ignored))
                .Returns(allCourses.Where(c => c.Language == language).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of CourseModel objects
            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(expectedCourse, result); //compare both lists directly for equality
        }
        [Test]
        public async Task Handle_SearchCriteria_ValidFullName_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var courseId2 = "08260480-52a0-4c0e-a588-274101a2c3be";
            var category = "ASP.NET";
            var language = "English";
            var userId = "08260479-52a0-4c0e-a588-274101a2c3be";
            var firstName = "Bojan";
            var lastName = "Mirkovic";
            var allCourses = new List<CourseModel>()
            {
                new CourseModel { CourseId = courseId, CategoryOrSubject = category, CourseIsCompleted = true, Language = language, UserId = userId },
                new CourseModel { CourseId = courseId2, CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German", UserId = "08260481-52a0-4c0e-a588-274101a2c3be" }
            };

            var allUsers = new List<UserModel>()
            {
                new UserModel { Id = userId, FirstName = firstName, LastName = lastName, Email = "boj@mail.com" },
                new UserModel { Id = "08260481-52a0-4c0e-a588-274101a2c3be", FirstName = "Test", LastName = "Testovic", Email = "test@mail.com" }
            };

            // Simulate the join operation and filter courses based on the full name
            var expectedCourse = allCourses.Where(c => allUsers.Any(u => u.Id == c.UserId && u.FirstName + " " + u.LastName == firstName + " " + lastName)).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(firstName + " " + lastName);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(A<string>.Ignored))
                .Returns(expectedCourse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of CourseModel objects
            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(expectedCourse, result); //compare both lists directly for equality
        }
        [Test]
        public void Handle_SearchCriteria_EmptyOrWhitespace_ThrowsArgumentException()
        {
            // Arrange
            var searchCriteria = "  ";
            var query = new GetAllCoursesBySearchCriteriaQuery(searchCriteria);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(query, CancellationToken.None));

            // Assert
            Assert.That(ex.Message, Does.Contain($"Course with searched criteria: {searchCriteria}, was not found!"));
        }
        [Test]
        public void Handle_SearchCriteria_NotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var searchCriteria = "NonExistentCourseId";
            var query = new GetAllCoursesBySearchCriteriaQuery(searchCriteria);

            // Set up the mock repository to return null for the given search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(searchCriteria)).Returns(Task.FromResult<List<CourseModel>>(null)); ;

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(query, CancellationToken.None));

            // Assert
            Assert.That(ex.Message, Does.Contain($"Course with searched criteria: {searchCriteria}, was not found!"));
        }

    }
}
