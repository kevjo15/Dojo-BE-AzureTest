using API_Layer.Controllers;
using Application_Layer.Commands.ContentCommands;
using Application_Layer.DTO_s.Content;
using Domain_Layer.CommandOperationResult;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test_Layer.ContentTest.IntegrationTest
{
    [TestFixture]
    internal class ContentControllerIntegrationCreateContentTest
    {
        private IMediator _mediator;
        private ContentController _controller;

        [SetUp]
        public void Setup()
        {
            _mediator = A.Fake<IMediator>();
            _controller = new ContentController(_mediator);
        }

        [Test]
        public async Task CreateContent_ReturnsOk_WhenContentSuccessfullyCreated()
        {
            // Arrange
            var contentDTO = new CreateContentDTO { ContentTitle = "New Content" };
            var command = new CreateContentCommand(contentDTO);
            A.CallTo(() => _mediator.Send(A<CreateContentCommand>.That.Matches(x => x.ContentDTO == contentDTO), default))
                .Returns(new OperationResult<bool> { Success = true, Message = "Content successfully created" });

            // Act
            var result = await _controller.CreateContent(command.ContentDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo("Content successfully created"));
        }

        [Test]
        public async Task CreateContent_ReturnsBadRequest_WhenModuleCreationFails()
        {
            // Arrange
            var contentDTO = new CreateContentDTO();
            A.CallTo(() => _mediator.Send(A<CreateContentCommand>.That.Matches(x => x.ContentDTO == contentDTO), default)).Returns(new OperationResult<bool> { Success = false, Message = "Failed to create content" });

            // Act
            var result = await _controller.CreateContent(contentDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Failed to create content"));
        }

        [Test]
        public async Task CreateContent_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var contentDTO = new CreateContentDTO();
            A.CallTo(() => _mediator.Send(A<CreateContentCommand>.That.Matches(x => x.ContentDTO == contentDTO), default)).Throws(new Exception("Unhandled exception"));

            // Act
            var result = await _controller.CreateContent(contentDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
            Assert.That(badRequestResult.Value, Is.EqualTo("Unhandled exception"));
        }
    }
}
