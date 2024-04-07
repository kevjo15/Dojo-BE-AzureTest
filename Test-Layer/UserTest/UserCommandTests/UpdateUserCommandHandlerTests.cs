using Application_Layer.Commands.UpdateUser;
using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.CommandTests
{
    [TestFixture]
    public class UpdateUserCommandHandlerTests
    {
        [Test]
        public async Task Handel_UpdateUser_Corect_Email_Return_UpdatedUser()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();

            var updatingUserInfo = new UpdatingUserDTO
            {
                FirstName = "Bojan",
                LastName = "Mirkovic",
                Email = "test@yahoo.com",
                CurrentPassword = "testpassword1",
                NewPassword = "newTestPassword",
                Role = "Student"
            };
            var updateCommand = new UpdateUserCommand(updatingUserInfo);
            // AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatingUserDTO, UserModel>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            var updatedUser = mapper.Map<UserModel>(updateCommand.UpdatingUserInfo);
            A.CallTo(() => userRepository.UpdateUserAsync(A<UserModel>._, A<string>._, A<string>._))
               .WithAnyArguments()
               .Returns(Task.FromResult(updatedUser));
            var handler = new UpdateUserCommandHandler(userRepository, mapper);
            // Act
            var result = await handler.Handle(updateCommand, CancellationToken.None);
            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.FirstName, Is.EqualTo("Bojan"));
            Assert.That(result.Role, Is.EqualTo("Student"));
            Assert.That(result, Is.TypeOf<UserModel>());
        }
        [Test]
        public void Handle_UserDoesNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var updatingUserInfo = new UpdatingUserDTO { Email = "nonexistentUser@example.com", CurrentPassword = "oldPassword", NewPassword = "newPassword" };
            var updateCommand = new UpdateUserCommand(updatingUserInfo);

            // Configure the fake to return null when GetUserByEmailAsync is called with the specified email
            A.CallTo(() => userRepository.GetUserByEmailAsync(A<string>.That.Matches(email => email == updatingUserInfo.Email)))
                .Returns(Task.FromResult<UserModel>(null));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatingUserDTO, UserModel>();
            });

            var mapper = mapperConfig.CreateMapper();

            var handler = new UpdateUserCommandHandler(userRepository, mapper);

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(updateCommand, CancellationToken.None);
            });

            // Assert that the exception message is as expected
            StringAssert.Contains("User with E-mail nonexistentUser@example.com does not exist in the system!", ex.Message);
        }
        [Test]
        public void Handle_ExceptionThrown_RethrowsException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var updatingUserInfo = new UpdatingUserDTO { Email = "testUser@yahoo.com", CurrentPassword = "oldPassword", NewPassword = "newPassword" };
            var updateCommand = new UpdateUserCommand(updatingUserInfo);

            // Configure the fake to return a user when GetUserByEmailAsync is called
            var existingUser = new UserModel { Email = updatingUserInfo.Email };
            A.CallTo(() => userRepository.GetUserByEmailAsync(A<string>.That.Matches(email => email == updatingUserInfo.Email)))
                .Returns(Task.FromResult(existingUser));

            // AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdatingUserDTO, UserModel>();
            });

            var mapper = mapperConfig.CreateMapper();

            // Configure the fake to throw an exception when UpdateUserAsync is called
            A.CallTo(() => userRepository.UpdateUserAsync(A<UserModel>._, A<string>._, A<string>._))
                .ThrowsAsync(new Exception("Test exception"));

            var handler = new UpdateUserCommandHandler(userRepository, mapper);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(updateCommand, CancellationToken.None);
            });

            // Assert that the exception message is as expected
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
        }
    }
}
