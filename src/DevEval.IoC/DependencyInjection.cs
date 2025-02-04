using Microsoft.Extensions.DependencyInjection;
using DevEval.Domain.Repositories;
using DevEval.ORM.Repositories;
using DevEval.Application.Sales.Services;
using DevEval.Common.Services;

namespace DevEval.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddScoped<ISaleEventPublisher, SaleEventPublisher>();

            return services;
        }
    }
}