using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Database
{
    public class DojoDBContext : IdentityDbContext
    {
        public DojoDBContext()
        {

        }

        public DojoDBContext(DbContextOptions<DojoDBContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
