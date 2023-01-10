using Microsoft.Extensions.DependencyInjection;
using Store.Service.Interfaces;
using Store.Service.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Store.Service
{
    public static class CompositionRoot
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Services
            services.AddTransient<IPurchaseOrderService, PurchaseOrderService>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ISalesOrderService, SalesOrderService>();
            services.AddTransient<IBussinesAccountService, BussinesAccountService>();
            services.AddTransient<IIncomingPaymentService, IncomingPaymentService>();

            return services;
        }
    }
}
