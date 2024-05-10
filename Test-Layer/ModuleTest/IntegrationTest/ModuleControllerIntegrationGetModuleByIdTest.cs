using API_Layer.Controllers;
using Application_Layer.Queries.ModuleQueries.GetModuleById;
using Domain_Layer.Models.Module;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.IntegrationTest
{
    [TestFixture]
    public class ModuleControllerIntegrationGetModuleByIdTest
    {
        private IMediator _mediator;
        private ModuleController _moduleController;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _moduleController = new ModuleController(_mediator);
        }

        [Test]
        public async Task GetModuleById_ReturnsOk_WhenModuleExists()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();
            string moduleTitle = "Test Module";
            string description = "Description";
            var expectedModule = new ModuleModel { ModuleId = moduleId, ModuleTitle = moduleTitle, Description = description };

            A.CallTo(() => _mediator.Send(A<GetModuleByIdQuery>.That.Matches(q => q.ModuleId == moduleId), default))
                .Returns(Task.FromResult<IActionResult>(new OkObjectResult(expectedModule)));

            // Act
            var result = await _moduleController.GetModuleById(moduleId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetModuleById_ReturnsNotFound_WhenModuleDoesNotExist()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();

            A.CallTo(() => _mediator.Send(A<GetModuleByIdQuery>.That.Matches(q => q.ModuleId == moduleId), default))
                .Returns(Task.FromResult<IActionResult>(null));

            // Act
            var result = await _moduleController.GetModuleById(moduleId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo($"Module with ID {moduleId} was not found."));
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }
    }
}
