using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly DojoDBContext _dojoDBContext;
        public UserRepository(UserManager<UserModel> userManager, DojoDBContext dojoDBContext)
        {
            _userManager = userManager;
            _dojoDBContext = dojoDBContext;
        }
        public async Task<UserModel> RegisterUserAsync(UserModel newUser, string password)
        {
            var result = await _userManager.CreateAsync(newUser, password);
            return newUser;

        }
        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user!;
        }

        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword)
        {
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(userToUpdate, currentPassword);

            var changePasswordResult = await _userManager.ChangePasswordAsync(userToUpdate, currentPassword, newPassword);

            userToUpdate.FirstName = userToUpdate.FirstName;
            userToUpdate.LastName = userToUpdate.LastName;

            var updateResult = await _userManager.UpdateAsync(userToUpdate);

            return userToUpdate;
        }

        public async Task<bool> DeleteUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsDeleted = true;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            return false;
        }
    }
}
