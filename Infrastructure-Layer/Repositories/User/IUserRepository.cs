using Domain_Layer.Models.UserModel;

namespace Infrastructure_Layer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUserAsync(UserModel newUser);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
    }
}
