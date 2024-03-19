using Domain_Layer.Models.UserModel;
using Infrastructure_Layer.DatabaseHelper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Database
{
    public class DojoDBContext : IdentityDbContext
    {
        private readonly DatabaseSeedHelper _databaseSeedHelper;
        public DojoDBContext(DbContextOptions<DojoDBContext> options, DatabaseSeedHelper databaseSeedHelper) : base(options)
        {
            _databaseSeedHelper = databaseSeedHelper;
        }
        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _databaseSeedHelper.SeedData(builder);
            base.OnModelCreating(builder);
        }
    }
}
