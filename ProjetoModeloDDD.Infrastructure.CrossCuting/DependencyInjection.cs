using Microsoft.Extensions.DependencyInjection;
using ProjetoModeloDDD.Application;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using ProjetoModeloDDD.Domain.Services;
using ProjetoModeloDDD.Infrastructure.Data.Repositories;

namespace ProjetoModeloDDD.Infrastructure.CrossCuting
{
    public class DependencyInjection
    {
        public static void SetUp(IServiceCollection services)
        {
            SetUpDiApplication(services);
            SetUpDiDomain(services);
            SetUpDiRepositories(services);
        }

        private static void SetUpDiApplication(IServiceCollection services)
        {
            services.AddTransient(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddTransient<IProdutoAppService, ProdutoAppService>();
        }

        private static void SetUpDiDomain(IServiceCollection services)
        {
            services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IProdutoService, ProdutoService>();
        }

        private static void SetUpDiRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
        }
    }
}