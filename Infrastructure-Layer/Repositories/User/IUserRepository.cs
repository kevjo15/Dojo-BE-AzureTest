using Domain_Layer.Models.UserModel;

namespace Infrastructure_Layer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUserAsync(UserModel newUser, string password);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword);
        Task<bool> DeleteUserByIdAsync(string userId);
    }
}
