using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Store.Service
{
    public static class CompositionRoot
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Services
            //services.AddTransient<IBussinesAccountService, BussinesAccountService>();

            return services;
        }
    }
}
