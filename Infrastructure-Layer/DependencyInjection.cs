using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DojoDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            return services;
        }
    }
}
