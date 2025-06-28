using Burguer404.Configurations.MapperConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Burguer404.Configurations.DependecyInjections
{
    public static class ServicesConfigurations
    {
        public static IServiceCollection AddJsonSerializerConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        public static IServiceCollection AddOutputCacheConfiguration(this IServiceCollection services)
        {
            services.AddOutputCache(options =>
            {
                options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(30);//Cachea por 30 segundos
            });

            return services;
        }
        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            return app;
        }
        public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
        public static WebApplication UseHealthChecksConfiguration(this WebApplication app, IConfiguration config)
        {
            app.UseHealthChecks(config["Uri"]);
            return app;
        }
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
