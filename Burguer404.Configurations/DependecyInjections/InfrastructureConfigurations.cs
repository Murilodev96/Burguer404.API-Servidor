using Burguer404.Domain.Ports.Repositories.Cliente;
using Burguer404.Domain.Ports.Repositories.Pedido;
using Burguer404.Domain.Ports.Repositories.Produto;
using Burguer404.Infrastructure.Data.ContextDb;
using Burguer404.Infrastructure.Data.Repositories.Cliente;
using Burguer404.Infrastructure.Data.Repositories.Pedido;
using Burguer404.Infrastructure.Data.Repositories.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Burguer404.Configurations.DependecyInjections
{
    public static class InfrastructureConfigurations
    {
        public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information);
            });

            services.AddScoped<IRepositoryCliente, RepositoryCliente>();
            services.AddScoped<IRepositoryPedido, RepositoryPedido>();
            services.AddScoped<IRepositoryProduto, RepositoryProduto>();

            return services;
        }
    }
}
