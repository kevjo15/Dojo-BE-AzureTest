using System.Reflection.Emit;
using Domain_Layer.Models.ContentModel;
using Domain_Layer.Models.CourseModel;
using Domain_Layer.Models.ModulModel;
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
        public DbSet<CourseModel> CourseModel { get; set; }
        public DbSet<ModulModel> ModuleModel { get; set; }
        public DbSet<ContentModel> ContentModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseModel>().HasKey(c => c.CourseId);
            _databaseSeedHelper.SeedData(builder);
            base.OnModelCreating(builder);
        }
    }
}
