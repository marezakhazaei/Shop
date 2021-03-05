using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Shop.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            var userStoreConnectionString = configuration.GetConnectionString("ShopDb");

            services.AddDbContext<ShopDbContext>(r => r.UseSqlServer(userStoreConnectionString));
            return services;
        }
    }
}
