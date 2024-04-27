using API_Layer.Controllers;
using Application_Layer.Commands.ModuleCommands.CreateModule;
using Application_Layer.DTO_s.Module;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.IntegrationTest
{
    [TestFixture]
    public class ModuleControllerIntegrationTest
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
        public async Task CreateModule_ReturnsOk_WhenModuleSuccessfullyCreated()
        {
            // Arrange
            var moduleDTO = new CreateModuleDTO { ModulTitle = "New Module" };
            var command = new CreateModuleCommand(moduleDTO);
            A.CallTo(() => _mediator.Send(A<CreateModuleCommand>.That.Matches(x => x.ModuleDTO == moduleDTO), default))
                .Returns(new CreateModuleResult { Success = true, Message = "Module successfully created" });

            // Act
            var result = await _controller.CreateModule(command.ModuleDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Module successfully created"));
        }

        [Test]
        public async Task CreateModule_ReturnsBadRequest_WhenModuleCreationFails()
        {
            // Arrange
            var moduleDTO = new CreateModuleDTO();
            A.CallTo(() => _mediator.Send(A<CreateModuleCommand>.That.Matches(x => x.ModuleDTO == moduleDTO), default)).Returns(new CreateModuleResult { Success = false, Message = "Failed to create module" });

            // Act
            var result = await _controller.CreateModule(moduleDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to create module"));
        }

        [Test]
        public async Task CreateModule_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var moduleDTO = new CreateModuleDTO();
            A.CallTo(() => _mediator.Send(A<CreateModuleCommand>.That.Matches(x => x.ModuleDTO == moduleDTO), default)).Throws(new Exception("Unhandled exception"));

            // Act
            var result = await _controller.CreateModule(moduleDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Unhandled exception"));
        }
    }
}
