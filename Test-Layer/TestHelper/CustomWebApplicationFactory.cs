using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;

namespace Test_Layer.TestHelper
{
    internal class CustomWebApplicationFactory<T> : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<DojoDBContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<DojoDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                });

            });
        }
    }
}
