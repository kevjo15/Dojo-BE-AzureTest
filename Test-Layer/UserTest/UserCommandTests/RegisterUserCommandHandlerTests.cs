using Application_Layer.Commands.RegisterNewUser;
using Application_Layer.DTO_s;
using AutoMapper;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.CommandTests
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidUser_ReturnsCreatedUser()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var newUser = new RegisterUserDTO { Email = "testUser@yahoo.com", Password = "testPassword1!", ConfirmPassword = "testPassword1!", FirstName = "Bojan", LastName = "Mirkovic", Role = "Student" };
            var registerCommand = new RegisterUserCommand(newUser);

            // AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserDTO, UserModel>();
            });

            var mapper = mapperConfig.CreateMapper();

            var expectedUser = mapper.Map<UserModel>(registerCommand.NewUser);

            A.CallTo(() => userRepository.RegisterUserAsync(A<UserModel>._, A<string>._, A<string>._))
                .WithAnyArguments()
                .Returns(Task.FromResult(expectedUser));

            var handler = new RegisterUserCommandHandler(userRepository, mapper);

            // Act
            var result = await handler.Handle(registerCommand, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.FirstName, Is.EqualTo(expectedUser.FirstName));
            Assert.That(result.Email, Is.EqualTo(expectedUser.Email));
            Assert.That(result.PasswordHash, Is.EqualTo(expectedUser.PasswordHash));
            Assert.That(result.Role, Is.EqualTo(expectedUser.Role));
        }


        [Test]
        public void Handle_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var registerCommand = new RegisterUserCommand(null!); // Passing null user

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserDTO, UserModel>();
            });

            var mapper = mapperConfig.CreateMapper();

            var handler = new RegisterUserCommandHandler(userRepository, mapper);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(registerCommand, CancellationToken.None);
            });
        }
        [Test]
        public void Handle_ExceptionThrown_RethrowsException()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var newUser = new RegisterUserDTO { Email = "testUser@yahoo.com", Password = "testPassword1!", ConfirmPassword = "testPassword1!", FirstName = "Bojan", LastName = "Mirkovic", Role = "Student" };
            var registerCommand = new RegisterUserCommand(newUser);

            // AutoMapper Configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserDTO, UserModel>();
            });

            var mapper = mapperConfig.CreateMapper();

            // Configure the fake to throw an exception when RegisterUserAsync is called
            A.CallTo(() => userRepository.RegisterUserAsync(A<UserModel>._, A<string>._, A<string>._))
                .ThrowsAsync(new Exception("Test exception"));

            var handler = new RegisterUserCommandHandler(userRepository, mapper);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(registerCommand, CancellationToken.None);
            });

            // Assert that the exception message is as expected
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
        }

    }
}
