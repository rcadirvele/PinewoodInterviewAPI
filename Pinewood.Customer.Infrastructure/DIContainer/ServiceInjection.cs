using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pinewood.Customer.Application.Core.Interfaces;
using Pinewood.Customer.Infrastructure.Repositories;

namespace Pinewood.Customer.Infrastructure.DIContainer
{
    public static class ServiceInjection
	{
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICustomerInfoRepository, CustomerInfoRepository>();

            return services;

        }
    }
}

