using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Store.AccessData.Interfaces;
using Store.AccessData.Repositories;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Store.AccessData
{
    public static class CompositionRoot
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPurchaseOrderItemRepository, PurchaseOrderItemRepository>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ISalesOrderRepository, SalesOrderRepository>();
            services.AddTransient<ISalesOrderItemRepository, SalesOrderItemRepository>();
            services.AddTransient<IBussinesAccountRepository, BussinesAccountRepository>();
            services.AddTransient<IIncomingPaymentRepository, IncomingPaymentRepository>();


            services.AddDbContext<StoreDC>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:default"], o => o.EnableRetryOnFailure()));
            return services;
        }
    }
}
