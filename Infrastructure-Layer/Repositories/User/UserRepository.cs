
using Domain_Layer.Models.UserModel;

namespace Infrastructure_Layer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        public Task<UserModel> RegisterUser(UserModel newUser)
        {
            throw new NotImplementedException();
        }
    }
}
