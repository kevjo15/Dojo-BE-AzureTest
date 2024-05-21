using Application_Layer.Commands.ModuleCommands.DeleteModule;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Module;

namespace Test_Layer.ModuleTest.UnitTest.ModuleCommandTest
{
    [TestFixture]
    public class DeleteModuleCommandHandlerTest
    {
        private IModuleRepository _moduleRepository;
        private DeleteModuleCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _moduleRepository = A.Fake<IModuleRepository>();
            _handler = new DeleteModuleCommandHandler(_moduleRepository);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccessResult_WhenModuleIsSuccessfullyDeleted()
        {
            // Arrange
            var command = new DeleteModuleCommand("f92dd897-800e-4628-9dd5-22e806626a96");
            A.CallTo(() => _moduleRepository.DeleteModuleByModuleIdAsync(command.ModuleId)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("Modules successfully deleted"));
        }

        [Test]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            // Arrange
            var command = new DeleteModuleCommand("7b87847b-29d5-47cb-a882-e876fc6489fa");
            A.CallTo(() => _moduleRepository.DeleteModuleByModuleIdAsync(command.ModuleId)).Throws(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("An error occurred: Database error"));
        }
    }
}
