﻿using Domain_Layer.Models.UserModel;

namespace Infrastructure_Layer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUserAsync(UserModel newUser, string password, string role);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task<UserModel> UpdateUserAsync(UserModel userToUpdate);
        Task<bool> DeleteUserByIdAsync(string userId);
    }
}
