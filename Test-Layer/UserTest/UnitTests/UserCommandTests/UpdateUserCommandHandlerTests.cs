using Application_Layer.Commands.UpdateUser;
using AutoFixture;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;
using Test_Layer.TestHelper;

namespace Test_Layer.UserTest.UnitTests.UserCommandTests
{
    [TestFixture]
    public class UpdateUserCommandHandlerTests
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private UpdateUserCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _handler = new UpdateUserCommandHandler(_userRepository, _mapper);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_UserExists_UpdatesUserSuccessfully()
        {
            // Arrange
            var command = _fixture.Create<UpdateUserCommand>();
            var user = _fixture.Create<UserModel>();
            var updatedUser = _fixture.Create<UserModel>();

            A.CallTo(() => _userRepository.GetUserByEmailAsync(command.UpdatingUserInfo.Email))
                .Returns(user);
            A.CallTo(() => _mapper.Map(command.UpdatingUserInfo, user))
                .Returns(updatedUser);
            A.CallTo(() => _userRepository.UpdateUserAsync(A<UserModel>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(updatedUser);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.AreEqual(updatedUser, result);
            A.CallTo(() => _userRepository.UpdateUserAsync(user, command.UpdatingUserInfo.CurrentPassword, command.UpdatingUserInfo.NewPassword))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Handle_UserDoesNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            var command = _fixture.Create<UpdateUserCommand>();

            A.CallTo(() => _userRepository.GetUserByEmailAsync(command.UpdatingUserInfo.Email))
                .Returns(Task.FromResult<UserModel>(null));

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, default));
        }
    }
}
