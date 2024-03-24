using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure_Layer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly DojoDBContext _dojoDBContext;
        private readonly IConfiguration _configuration;
        public UserRepository(UserManager<UserModel> userManager, DojoDBContext dojoDBContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _dojoDBContext = dojoDBContext;
            _configuration = configuration;
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

        public async Task<string> GenerateJwtTokenAsync(UserModel user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.Role, user.Role ?? ""));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
