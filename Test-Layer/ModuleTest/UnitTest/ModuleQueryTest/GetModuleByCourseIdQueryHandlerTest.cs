using Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse;
using Domain_Layer.Models.Module;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Module;

namespace Test_Layer.ModuleTest.UnitTest.ModuleQueryTest
{
    [TestFixture]
    public class GetModuleByCourseIdQueryHandlerTest
    {
        private IModuleRepository _moduleRepository;
        private GetAllModulesByCourseIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _moduleRepository = A.Fake<IModuleRepository>();
            _handler = new GetAllModulesByCourseIdQueryHandler(_moduleRepository);
        }
        [Test]
        public async Task Handle_ValidCourseId_ReturnsCourse()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var listOfModules = new List<ModuleModel>
            {
                new ModuleModel{ OrderInCourse = 1},
                new ModuleModel{ OrderInCourse = 2},
                new ModuleModel{ OrderInCourse = 3},
            };
            var query = new GetAllModulesByCourseIdQuery(courseId);

            A.CallTo(() => _moduleRepository.GetAllModulesByCourseId(courseId))
              .Returns(listOfModules);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(listOfModules));
        }
        [Test]
        public void Handle_EmptyCourseId_ThrowsException()
        {
            var courseId = "";
            // Arrange
            var query = new GetAllModulesByCourseIdQuery(courseId);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
            StringAssert.Contains("An error occurred while fetching modules:", exception.Message);
        }
        [Test]
        public async Task Handle_NoModulesForCourseId_ThrowsInvalidOperationException()
        {
            // Arrange
            var courseId = new Guid().ToString();
            var emptyListOfModules = new List<ModuleModel>();
            var query = new GetAllModulesByCourseIdQuery(courseId);

            A.CallTo(() => _moduleRepository.GetAllModulesByCourseId(courseId))
              .Returns(emptyListOfModules);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
            Assert.That(ex.InnerException, Is.TypeOf<InvalidOperationException>());
            Assert.That(ex.InnerException.Message, Is.EqualTo($"Course with ID {courseId} was not found!"));
        }
    }
}
