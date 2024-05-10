using Application_Layer.Commands.ModuleCommands.UpdateModule;
using Application_Layer.DTO_s.Module;
using AutoMapper;
using Domain_Layer.Models.Module;
using FakeItEasy;
using FluentAssertions;
using Infrastructure_Layer.Repositories.Module;
using Microsoft.AspNetCore.Mvc;


namespace Test_Layer.ModuleTest.UnitTest.ModuleCommandTest
{
    [TestFixture]
    public class UpdateModuleCommandHandlerTests
    {
        private UpdateModuleCommandHandler _handler;
        private IModuleRepository _moduleRepository;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _moduleRepository = A.Fake<IModuleRepository>();
            _mapper = A.Fake<IMapper>();

            _handler = new UpdateModuleCommandHandler(_moduleRepository, _mapper);
        }

        [Test]
        public async Task Handle_ModuleExists_UpdatesModule()
        {
            // Arrange
            var moduleId = Guid.NewGuid().ToString();
            var updateModuleDto = new UpdateModuleDTO
            {
                ModulTitle = "New Title",
                Description = "New Description",
                OrderInCourse = 2,
                ResourceURL = "http://example.com/newresource"
            };
            var command = new UpdateModuleCommand(moduleId, updateModuleDto);

            var existingModule = new ModuleModel
            {
                ModuleId = moduleId,
                ModuleTitle = "Old Title",
                Description = "Old Description",
                OrderInCourse = 1,
                ResourceURL = "http://example.com/oldresource"
            };

            A.CallTo(() => _moduleRepository.GetModuleByIdAsync(moduleId)).Returns(existingModule);
            A.CallTo(() => _moduleRepository.UpdateModuleAsync(A<ModuleModel>.Ignored)).Returns(Task.FromResult(true));

            A.CallTo(() => _mapper.Map(updateModuleDto, A<ModuleModel>.Ignored))
                .Invokes((UpdateModuleDTO src, ModuleModel dest) =>
                {
                    dest.ModuleTitle = src.ModulTitle;
                    dest.Description = src.Description;
                    dest.OrderInCourse = src.OrderInCourse;
                    dest.ResourceURL = src.ResourceURL;
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());

            var okResult = result as OkObjectResult;
            var updatedModule = okResult.Value as ModuleModel;

            Assert.IsNotNull(updatedModule);
            updatedModule.ModuleTitle.Should().Be(updateModuleDto.ModulTitle);
            updatedModule.Description.Should().Be(updateModuleDto.Description);
            updatedModule.OrderInCourse.Should().Be(updateModuleDto.OrderInCourse);
            updatedModule.ResourceURL.Should().Be(updateModuleDto.ResourceURL);

            A.CallTo(() => _moduleRepository.UpdateModuleAsync(A<ModuleModel>.That.Matches(
                m => m.ModuleId == moduleId &&
                     m.ModuleTitle == updateModuleDto.ModulTitle &&
                     m.Description == updateModuleDto.Description &&
                     m.OrderInCourse == updateModuleDto.OrderInCourse &&
                     m.ResourceURL == updateModuleDto.ResourceURL
            ))).MustHaveHappenedOnceExactly();
        }
        [Test]
        public async Task Handle_WhenModuleNotFound_ShouldReturnNotFoundResult()
        {
            // Arrange
            var moduleId = "nonExistentId";
            var moduleUpdateDTO = new UpdateModuleDTO();
            A.CallTo(() => _moduleRepository.GetModuleByIdAsync(moduleId)).Returns(Task.FromResult<ModuleModel>(null));

            var command = new UpdateModuleCommand(moduleId, moduleUpdateDTO);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
        }
        [Test]
        public async Task Handle_WhenExceptionOccurs_ShouldReturnBadRequest()
        {
            // Arrange
            var moduleId = "existingId";
            var moduleUpdateDTO = new UpdateModuleDTO();
            var module = new ModuleModel { ModuleId = moduleId };

            A.CallTo(() => _moduleRepository.GetModuleByIdAsync(moduleId)).Returns(module);
            A.CallTo(() => _moduleRepository.UpdateModuleAsync(module)).ThrowsAsync(new Exception("Database error"));

            var command = new UpdateModuleCommand(moduleId, moduleUpdateDTO);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult?.Value, Is.EqualTo("An error occurred while updating the module: Database error"));
        }

    }
}
