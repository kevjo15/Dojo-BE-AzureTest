using API_Layer.Controllers;
using Application_Layer.Commands.ModuleCommands.DeleteModule;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.IntegrationTest
{
    [TestFixture]
    public class ModuleControllerIntegrationDeleteModuleTest
    {
        private IMediator _mediator;
        private ModuleController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();
            _controller = new ModuleController(_mediator);
        }

        [Test]
        public async Task DeleteModule_ReturnsOk_WhenModuleSuccessfullyDeleted()
        {
            // Arrange
            var moduleId = "1408b448-83a4-4158-9a96-f5aa02d3cdac";
            A.CallTo(() => _mediator.Send(A<DeleteModuleCommand>.That.Matches(x => x.ModuleId == moduleId), default))
                .Returns(new DeleteModuleResult { Success = true, Message = "Modules successfully deleted" });

            // Act
            var result = await _controller.DeleteModule(moduleId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Modules successfully deleted"));
        }

        [Test]
        public async Task DeleteModule_ReturnsBadRequest_WhenModuleDeletionFails()
        {
            // Arrange
            var moduleId = "f8e9cbb3-4a9a-46a5-8c27-a3a4b254954a";
            A.CallTo(() => _mediator.Send(A<DeleteModuleCommand>.That.Matches(x => x.ModuleId == moduleId), default)).Returns(new DeleteModuleResult { Success = false, Message = "Failed to delete modules" });

            // Act
            var result = await _controller.DeleteModule(moduleId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to delete modules"));
        }

        [Test]
        public async Task DeleteModule_ReturnsBadRequest_WhenExceptionOccurs()
        {

            // Arrange
            var moduleId = "ce212c9a-40ae-475c-89a1-387c575a9b6c";
            A.CallTo(() => _mediator.Send(A<DeleteModuleCommand>.That.Matches(x => x.ModuleId == moduleId), default)).Throws(new Exception("Unhandled exception"));

            // Act
            var result = await _controller.DeleteModule(moduleId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Unhandled exception"));
        }
    }
}
