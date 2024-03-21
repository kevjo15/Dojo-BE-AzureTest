using Domain_Layer.Models.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.DatabaseHelper
{
    public class DatabaseSeedHelper
    {
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeedHelper(IPasswordHasher<UserModel> passwordHasher, RoleManager<IdentityRole> roleManager)
        {
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        public void SeedData(ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
            SeedRoles().Wait();
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel { Id = "08260479-52a0-4c0e-a588-274101a2c3be", FirstName = "Bojan", LastName = "Mirkovic", Email = "bojan@infinet.com", PasswordHash = _passwordHasher.HashPassword(new UserModel(), "Bojan123!"), Role = "User" },
                new UserModel { Id = "047425eb-15a5-4310-9d25-e281ab036868", FirstName = "Elliot", LastName = "Dahlin", Email = "elliot@infinet.com", PasswordHash = _passwordHasher.HashPassword(new UserModel(), "Elliot123!"), Role = "User" },
                new UserModel { Id = "047425eb-15a5-4310-9d25-e281ab036869", FirstName = "Kevin", LastName = "Jorgensen", Email = "kevin@infinet.com", PasswordHash = _passwordHasher.HashPassword(new UserModel(), "Kevin123!"), Role = "User" }
            );
        }

        private async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }
            if (!await _roleManager.RoleExistsAsync("Teacher"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Teacher"));
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
