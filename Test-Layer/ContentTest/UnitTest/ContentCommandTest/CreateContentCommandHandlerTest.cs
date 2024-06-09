using Application_Layer.Commands.ContentCommands;
using Application_Layer.Commands.ModuleCommands.CreateModule;
using Application_Layer.DTO_s.Content;
using Application_Layer.DTO_s.Module;
using AutoMapper;
using Domain_Layer.Models.Content;
using Domain_Layer.Models.Module;
using FakeItEasy;
using Infrastructure_Layer.Repositories.Content;


namespace Test_Layer.ContentTest.UnitTest.ContentCommandTest
{
    [TestFixture]
    public class CreateContentCommandHandlerTest
    {
        private IContentRepository _contentRepository;
        private IMapper _mapper;
        private CreateContentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contentRepository = A.Fake<IContentRepository>();
            _mapper = A.Fake<IMapper>();
            _handler = new CreateContentCommandHandler(_contentRepository, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccessResult_WhenModuleIsSuccessfullyCreated()
        {
            // Arrange
            var contentDTO = new CreateContentDTO
            {
                ContentTitle = "Title",
                ContentType = "Video",
                Description = "Description",
                ContentURL = "http://test.com"

            };
            var contentModel = new ContentModel();
            A.CallTo(() => _mapper.Map<ContentModel>(contentDTO)).Returns(contentModel);
            A.CallTo(() => _contentRepository.CreateContentAsync(contentModel)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(new CreateContentCommand(contentDTO), default);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("Content successfully created"));
        }
        [Test]
        public async Task Handle_ShouldReturnFailureResult_WhenModuleCreationFails()
        {
            // Arrange
            var contentDTO = new CreateContentDTO
            {
                ContentTitle = "Title",
                ContentType = "Video",
                Description = "Description",
                ContentURL = "http://test.com"
            };
            var contentModel = new ContentModel();
            A.CallTo(() => _mapper.Map<ContentModel>(contentDTO)).Returns(contentModel);
            A.CallTo(() => _contentRepository.CreateContentAsync(contentModel)).Throws(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(new CreateContentCommand(contentDTO), default);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("An error occurred: Database error"));
        }

        [Test]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            // Arrange
            var contentDTO = new CreateContentDTO();
            A.CallTo(() => _mapper.Map<ContentModel>(A<CreateContentDTO>.Ignored)).Throws(new Exception("Mapping failed"));

            // Act
            var result = await _handler.Handle(new CreateContentCommand(contentDTO), default);

            // Assert
            Assert.IsFalse(result.Success);
            StringAssert.Contains("An error occurred: Mapping failed", result.Message);
        }
    }
}
