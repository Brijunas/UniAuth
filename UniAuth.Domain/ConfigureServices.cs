using Microsoft.Extensions.DependencyInjection;
using UniAuth.Domain.UsernamesAuth;

namespace UniAuth.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUsernamesAuthService, UsernamesAuthService>();

            return services;
        }
    }
}
