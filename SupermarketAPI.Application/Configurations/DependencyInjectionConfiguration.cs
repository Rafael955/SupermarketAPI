using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Domain.Interfaces.Services;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Infra.Data.Repositories;

namespace SupermarketAPI.Application.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        // Add your dependency injection configuration methods here
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IProdutosService, ProdutosService>();
            services.AddTransient<ICategoriasService, CategoriasService>();
            services.AddTransient<IProdutosRepository, ProdutosRepository>();
            services.AddTransient<ICategoriasRepository, CategoriasRepository>();
        }
    }
}