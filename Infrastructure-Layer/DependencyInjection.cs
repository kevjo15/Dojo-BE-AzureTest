using Infrastructure_Layer.Database;
using Infrastructure_Layer.DatabaseHelper;
using Infrastructure_Layer.Repositories.Content;
using Infrastructure_Layer.Repositories.Course;
using Infrastructure_Layer.Repositories.Module;
using Infrastructure_Layer.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLAZURECONNSTR_DOJO_PROD_DB");
            if (connectionString is null)
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }
            services.AddDbContext<DojoDBContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<DatabaseSeedHelper>();

            return services;
        }
    }
}
