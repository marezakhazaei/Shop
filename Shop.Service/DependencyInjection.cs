using Microsoft.Extensions.DependencyInjection;
using Shop.Common.ServiceContracts;
using Shop.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBasketService, BasketService>();
            return services;
        }
    }
}
