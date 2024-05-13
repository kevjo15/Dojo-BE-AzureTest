using Domain_Layer.Models.Content;
using Domain_Layer.Models.Course;
using Domain_Layer.Models.CourseHasModule;
using Domain_Layer.Models.CourseHasTag;
using Domain_Layer.Models.Module;
using Domain_Layer.Models.ModuleHasContent;
using Domain_Layer.Models.User;
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
        public DbSet<ModuleModel> ModuleModel { get; set; }
        public DbSet<ContentModel> ContentModel { get; set; }
        public DbSet<CourseHasModuleModel> CourseHasModules { get; set; }
        public DbSet<ModuleHasContentModel> ModuleHasContents { get; set; }
        public DbSet<CourseHasTagModel> CourseHasTags { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CourseHasModuleModel>()
                 .HasKey(cm => new { cm.CourseId, cm.ModuleId });

            builder.Entity<CourseHasModuleModel>()
               .HasOne(cm => cm.Course)
               .WithMany(c => c.CourseHasModules)
               .HasForeignKey(cm => cm.CourseId);

            builder.Entity<CourseHasModuleModel>()
               .HasOne(cm => cm.Module)
               .WithMany(m => m.CourseHasModules)
               .HasForeignKey(cm => cm.ModuleId);
            //
            builder.Entity<ModuleHasContentModel>()
               .HasKey(mc => new { mc.ModuleId, mc.ContentId });

            builder.Entity<ModuleHasContentModel>()
               .HasOne(mc => mc.Module)
               .WithMany(m => m.ModuleHasContents)
               .HasForeignKey(mc => mc.ModuleId);

            builder.Entity<ModuleHasContentModel>()
               .HasOne(mc => mc.Content)
               .WithMany(m => m.ModuleHasContents)
               .HasForeignKey(mc => mc.ContentId);
            //
            builder.Entity<CourseHasTagModel>()
                .HasKey(ct => new { ct.CourseId, ct.TagId });

            builder.Entity<CourseHasTagModel>()
                .HasOne(ct => ct.Course)
                .WithMany(c => c.CourseHasTags)
                .HasForeignKey(ct => ct.CourseId);

            builder.Entity<CourseHasTagModel>()
               .HasOne(ct => ct.Tag)
               .WithMany(t => t.CourseHasTags)
               .HasForeignKey(ct => ct.TagId);

            _databaseSeedHelper.SeedData(builder);

        }
    }
}
