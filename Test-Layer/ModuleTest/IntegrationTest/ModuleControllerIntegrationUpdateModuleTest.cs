using API_Layer.Controllers;
using Application_Layer.Commands.ModuleCommands.UpdateModule;
using Application_Layer.DTO_s.Module;
using Domain_Layer.Models.Module;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ModuleTest.IntegrationTest
{
    [TestFixture]
    public class ModuleControllerIntegrationUpdateModuleTest
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
        public async Task UpdateModule_ReturnsOk_WhenModuleSuccessfullyUpdated()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();
            var moduleUpdateDTO = new UpdateModuleDTO
            {
                ModulTitle = "New Title",
                Description = "New Description",
                OrderInCourse = 2,
                ResourceURL = "http://example.com/newresource"
            };
            var updatedModuleResult = new ModuleModel
            {
                ModuleId = moduleId,
                ModuleTitle = moduleUpdateDTO.ModulTitle,
                Description = moduleUpdateDTO.Description,
                OrderInCourse = moduleUpdateDTO.OrderInCourse,
                ResourceURL = moduleUpdateDTO.ResourceURL
            };

            A.CallTo(() => _mediator.Send(A<UpdateModuleCommand>.That.Matches(
                c => c.ModuleId == moduleId && c.ModuleUpdateDTO == moduleUpdateDTO), A<CancellationToken>._))
                .Returns(new OkObjectResult(updatedModuleResult));

            // Act
            var result = await _controller.UpdateModule(moduleId, moduleUpdateDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(updatedModuleResult));
        }

        [Test]
        public async Task UpdateModule_ReturnsNotFound_WhenModuleDoesNotExist()
        {
            // Arrange
            var nonExistingModuleId = Guid.NewGuid().ToString();
            var moduleUpdateDTO = new UpdateModuleDTO();

            A.CallTo(() => _mediator.Send(A<UpdateModuleCommand>.That.Matches(
                c => c.ModuleId == nonExistingModuleId), A<CancellationToken>._))
                .Returns(new NotFoundObjectResult($"Module with ID {nonExistingModuleId} not found."));

            // Act
            var result = await _controller.UpdateModule(nonExistingModuleId, moduleUpdateDTO);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
            Assert.That(notFoundResult.Value, Is.EqualTo($"Module with ID {nonExistingModuleId} not found."));
        }
        [Test]
        public async Task UpdateModule_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();
            var moduleUpdateDTO = new UpdateModuleDTO { /* Initialize properties */ };
            var exceptionMessage = "An error occurred while updating the module.";

            A.CallTo(() => _mediator.Send(A<UpdateModuleCommand>.That.Matches(
                c => c.ModuleId == moduleId && c.ModuleUpdateDTO == moduleUpdateDTO), A<CancellationToken>._))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.UpdateModule(moduleId, moduleUpdateDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo(exceptionMessage));
        }
        [Test]
        public async Task UpdateModule_ReturnsBadRequest_WhenNoDataProvided()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();

            // Act
            var result = await _controller.UpdateModule(moduleId, null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Invalid request: No data provided."));
        }

    }
}
