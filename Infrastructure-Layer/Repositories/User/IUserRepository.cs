using Domain_Layer.Models.UserModel;

namespace Infrastructure_Layer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUser(UserModel newUser);
    }
}
