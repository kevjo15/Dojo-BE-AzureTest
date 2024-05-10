using Application_Layer.AutoMaper;
using Application_Layer.PipelineBehaviour;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application_Layer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly))
                 .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>))
                 .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddValidatorsFromAssembly(assembly);


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            services.AddSingleton(config.CreateMapper());



            return services;
        }
    }
}
