using Application_Layer.Commands.UpdateUser;
using Application_Layer.DTO_s;
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
            var updatingUserInfo = new UpdatingUserDTO
            {
                Email = "updatedEmail@example.com",
                CurrentPassword = "ValidCurrentPassword123",
                NewPassword = "NewValidPassword123",
                Role = "Teacher",
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName"
            };
            var command = new UpdateUserCommand(updatingUserInfo);

            var existingUser = A.Fake<UserModel>();
            existingUser.Email = "originalEmail@example.com";
            existingUser.FirstName = "OriginalFirstName";
            existingUser.LastName = "OriginalLastName";
            existingUser.Role = "Student";

            A.CallTo(() => _userRepository.GetUserByEmailAsync(command.UpdatingUserInfo.Email)).Returns(existingUser);

            // Simulerar mappningens effekter
            A.CallTo(() => _mapper.Map(updatingUserInfo, existingUser)).Invokes(() =>
            {
                existingUser.Email = updatingUserInfo.Email;
                existingUser.FirstName = updatingUserInfo.FirstName;
                existingUser.LastName = updatingUserInfo.LastName;
                existingUser.Role = updatingUserInfo.Role;
            });

            A.CallTo(() => _userRepository.UpdateUserAsync(existingUser, updatingUserInfo.CurrentPassword, updatingUserInfo.NewPassword)).Returns(existingUser);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Email, Is.EqualTo(updatingUserInfo.Email));
            Assert.That(result.FirstName, Is.EqualTo(updatingUserInfo.FirstName));
            Assert.That(result.LastName, Is.EqualTo(updatingUserInfo.LastName));
            Assert.That(result.Role, Is.EqualTo(updatingUserInfo.Role));

            // Verifiera att UpdateUserAsync anropats med den förväntade användaren och lösenorden
            A.CallTo(() => _userRepository.UpdateUserAsync(
                A<UserModel>.That.Matches(u => u.Email == updatingUserInfo.Email && u.FirstName == updatingUserInfo.FirstName && u.LastName == updatingUserInfo.LastName),
                updatingUserInfo.CurrentPassword,
                updatingUserInfo.NewPassword
            )).MustHaveHappenedOnceExactly();
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
