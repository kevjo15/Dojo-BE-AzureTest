using Application_Layer.Queries.GetAllUsers;
using Domain_Layer.Models.UserModel;
using FakeItEasy;
using Infrastructure_Layer.Repositories.User;

namespace Test_Layer.UserTests.QueryTests
{
    [TestFixture]
    public class GetAllUserQueryHandlerTests
    {
        [Test]
        public async Task Handle_GetAllUsers_Returns_ListOfUsers()
        {
            // Arrange
            var userRepository = A.Fake<IUserRepository>();
            var fakeUsers = new List<UserModel>
            {
                new UserModel { Id = "1", UserName = "User1" },
                new UserModel { Id = "2", UserName = "User2" }
            };
            A.CallTo(() => userRepository.GetAllUsersAsync()).Returns(Task.FromResult<IEnumerable<UserModel>>(fakeUsers));

            var handler = new GetAllUsersQueryHandler(userRepository);
            var command = new GetAllUsersQuery();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<IEnumerable<UserModel>>());
            Assert.That(result.Count(), Is.EqualTo(fakeUsers.Count));
        }
    }
}
