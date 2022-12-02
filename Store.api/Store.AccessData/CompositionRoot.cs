using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Store.AccessData
{
    public static class CompositionRoot
    {
        public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDC>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:default"], o => o.EnableRetryOnFailure()));
            return services;
        }
    }
}
