using Application_Layer.Commands.RegisterNewUser;
using AutoFixture;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTest.UnitTests.UserCommandTests
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private RegisterUserCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _handler = new RegisterUserCommandHandler(_userRepository, _mapper);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_ValidNewUser_ReturnsRegisteredUser()
        {
            // Arrange
            var command = _fixture.Create<RegisterUserCommand>();
            var newUser = _fixture.Create<UserModel>();
            var registeredUser = _fixture.Create<UserModel>();

            A.CallTo(() => _mapper.Map<UserModel>(command.NewUser))
                .Returns(newUser);
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role))
                .Returns(registeredUser);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.That(result, Is.EqualTo(registeredUser));
            A.CallTo(() => _userRepository.RegisterUserAsync(newUser, command.NewUser.Password, command.NewUser.Role))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Handle_InvalidNewUser_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new RegisterUserCommand(null);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, default));
        }
    }
}
