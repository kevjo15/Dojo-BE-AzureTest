using Application_Layer.Queries.ModuleQueries.GetModuleById;
using Domain_Layer.Models.Module;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Module;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.UnitTest.ModuleQueryTest
{
    [TestFixture]
    public class GetModuleByIdQueryHandlerTest
    {
        private IModuleRepository _moduleRepository;
        private GetModuleByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _moduleRepository = A.Fake<IModuleRepository>();
            _handler = new GetModuleByIdQueryHandler(_moduleRepository);
        }

        [Test]
        public async Task GetModuleByIdQueryHandler_Should_ReturnOkObjectResult_When_ModuleExists()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();
            var courseId = Guid.NewGuid().ToString();
            string moduleTitle = "Test Module";
            string description = "This is a test module";
            int orderInCource = 0;

            var expectedModule = new ModuleModel
            {
                ModuleId = moduleId,
                ModuleTitle = moduleTitle,
                Description = description,
                OrderInCourse = orderInCource
            };

            A.CallTo(() => _moduleRepository.GetModuleByIdAsync(moduleId)).Returns(Task.FromResult(expectedModule));

            // Act
            var actionResult = await _handler.Handle(new GetModuleByIdQuery(moduleId), default);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);
            var moduleResult = okResult.Value as ModuleModel;
            Assert.IsNotNull(moduleResult);
            Assert.That(moduleResult.ModuleId, Is.EqualTo(expectedModule.ModuleId));
            Assert.That(moduleResult.ModuleTitle, Is.EqualTo(expectedModule.ModuleTitle));
            Assert.That(moduleResult.Description, Is.EqualTo(expectedModule.Description));
            Assert.That(moduleResult.OrderInCourse, Is.EqualTo(expectedModule.OrderInCourse));
        }

        [Test]
        public async Task GetModuleByIdQueryHandler_Should_ReturnNotFoundResult_When_ModuleDoesNotExist()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();

            A.CallTo(() => _moduleRepository.GetModuleByIdAsync(moduleId)).Returns(Task.FromResult<ModuleModel>(null));

            // Act
            var actionResult = await _handler.Handle(new GetModuleByIdQuery(moduleId), default);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
            var notFoundResult = actionResult as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.Value, Is.EqualTo($"No module found with ID {moduleId}"));
        }
    }
}
