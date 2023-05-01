using Pricat.Application.Interfaces;
using Pricat.Application.Services;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Repositories;

namespace Pricat.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddApplicationModules(this IServiceCollection services)
    {
        // Services / Use Cases
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }

    public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}