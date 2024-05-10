using Domain_Layer.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.DatabaseHelper
{
    public class DatabaseSeedHelper
    {
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public DatabaseSeedHelper(IPasswordHasher<UserModel> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public void SeedData(ModelBuilder modelBuilder)
        {
            SeedUsers(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            var user1 = new UserModel { Id = "08260479-52a0-4c0e-a588-274101a2c3be", FirstName = "Bojan", LastName = "Mirkovic", UserName = "bojan@infinet.com", NormalizedUserName = "BOJAN@INFINET.COM", Email = "bojan@infinet.com", NormalizedEmail = "BOJAN@INFINET.COM", Role = "Admin" };
            user1.PasswordHash = _passwordHasher.HashPassword(user1, "Bojan123!");

            modelBuilder.Entity<UserModel>().HasData(user1);
        }

    }
}
