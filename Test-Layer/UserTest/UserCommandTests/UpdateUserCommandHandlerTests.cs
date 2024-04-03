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
    }
}
