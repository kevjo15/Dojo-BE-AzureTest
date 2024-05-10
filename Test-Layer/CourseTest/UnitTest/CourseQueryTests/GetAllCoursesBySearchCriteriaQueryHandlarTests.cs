using Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria;
using Domain_Layer.Models.Course;
using Domain_Layer.Models.User;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Course;

namespace Test_Layer.CourseTest.UnitTest.CourseQueryTests
{
    [TestFixture]
    public class GetAllCoursesBySearchCriteriaQueryHandlarTests
    {

        private ICourseRepository _courseRepository;
        private GetAllCoursesBySearchCriteriaQueryHandler _handler;
        private SearchCriteria? _searchCriteria;

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
            _searchCriteria = new SearchCriteria() { CourseId = "08260479-52a0-4c0e-a588-274101a2c3be" };
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = "08260479-52a0-4c0e-a588-274101a2c3be", CategoryOrSubject = "ASP.NET", CourseIsCompleted = true, Language = "English" },
              new CourseModel { CourseId = "08260480-52a0-4c0e-a588-274101a2c3be" , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.CourseId == _searchCriteria.CourseId).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(_searchCriteria))
                .Returns(allCourses.Where(c => c.CourseId == _searchCriteria.CourseId).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of Course objects
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
            _searchCriteria = new SearchCriteria() { CategoryOrSubject = category };
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = courseId, CategoryOrSubject = category, CourseIsCompleted = true, Language = "English" },
              new CourseModel { CourseId = courseId2 , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.CategoryOrSubject == category).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(_searchCriteria))
                .Returns(allCourses.Where(c => c.CategoryOrSubject == category).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of Course objects
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
            _searchCriteria = new SearchCriteria() { Language = language };
            var allCourses = new List<CourseModel>()
            {
              new CourseModel { CourseId = courseId, CategoryOrSubject = category, CourseIsCompleted = true, Language = language },
              new CourseModel { CourseId = courseId2 , CategoryOrSubject = "React", CourseIsCompleted = true, Language = "German" }
            };
            var expectedCourse = allCourses.Where(c => c.Language == language).ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(_searchCriteria))
                .Returns(allCourses.Where(c => c.Language == language).ToList());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of Course objects
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
            _searchCriteria = new SearchCriteria() { FirstName = firstName, LastName = lastName };
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
            var query = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);

            // Adjust the mock setup to return a filtered list based on the search criteria
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(_searchCriteria))
                .Returns(expectedCourse);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourse));
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<List<CourseModel>>()); // result is a list of Course objects
            Assert.That(result.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(expectedCourse, result); //compare both lists directly for equality
        }
        [Test]
        public async Task Handle_WithoutSearchCriteria_ReturnsAllCourses()
        {
            // Arrange
            _searchCriteria = new SearchCriteria() { SearchBySearchTerm = false };

            var request = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);
            var expectedCourses = new List<CourseModel> { new CourseModel { CourseId = "08260480-52a0-4c0e-a588-274101a2c3be", Title = "Test Course" } };

            A.CallTo(() => _courseRepository.GetAllCourses()).Returns(expectedCourses);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedCourses));
        }
        [Test]
        public void Handle_NoCoursesFound_ThrowsExceptionWithInnerInvalidOperationException()
        {
            // Arrange
            _searchCriteria = new SearchCriteria()
            {
                CourseId = "NonExistentCourseId",
                Title = "NonExistentCourseTitle",
                SearchBySearchTerm = true
            };
            var request = new GetAllCoursesBySearchCriteriaQuery(_searchCriteria);
            A.CallTo(() => _courseRepository.GetCoursesBySearchCriteria(_searchCriteria)).Returns(new List<CourseModel>());

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.That(ex.InnerException, Is.TypeOf<InvalidOperationException>());
            Assert.That(ex.InnerException.Message, Is.EqualTo("No courses found matching the search term."));
        }
    }
}
