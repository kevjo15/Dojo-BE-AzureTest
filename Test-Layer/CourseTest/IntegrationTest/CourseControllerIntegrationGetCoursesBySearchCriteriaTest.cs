using API_Layer.Controllers;
using Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria;
using Domain_Layer.Models.CourseModel;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.CourseTest.IntegrationTest
{
    [TestFixture]
    public class CourseControllerIntegrationGetCoursesBySearchCriteriaTest
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
        public async Task GetAllCourses_ReturnsOkResult_WithCourses()
        {
            // Arrange
            var searchCriteria = "English";
            var courseId = Guid.NewGuid().ToString();
            var allCourses = new List<CourseModel>
            {
                new CourseModel { CourseId = courseId, CategoryOrSubject = "ASP.NET", Title = "Test Course 1", Language = "English" },
                new CourseModel { CourseId = "2", CategoryOrSubject = "ASP.NET", Title = "Test Course 2", Language = "Spanish" }
            };
            var expectedCourses = allCourses.Where(c => c.Language == "English").ToList();
            var query = new GetAllCoursesBySearchCriteriaQuery(searchCriteria);

            // Setup the mediator to return the expected courses when the query is sent
            A.CallTo(() => _mediator.Send(A<GetAllCoursesBySearchCriteriaQuery>.That.Matches(q => q.SearchCriteria == searchCriteria), A<CancellationToken>.Ignored))
                .Returns(expectedCourses);

            // Act
            var result = await _controller.GetAllCourses(searchCriteria);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            // Use CollectionAssert for a list comparison
            var returnedCourses = okResult.Value as List<CourseModel>;
            CollectionAssert.AreEqual(expectedCourses, returnedCourses);
        }
        [Test]
        public async Task GetAllCourses_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var searchCriteria = "test-criteria";
            var exceptionMessage = "An error occurred.";

            A.CallTo(() => _mediator.Send(A<GetAllCoursesBySearchCriteriaQuery>.That.Matches(q => q.SearchCriteria == searchCriteria), default))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _controller.GetAllCourses(searchCriteria);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        }
    }
}
