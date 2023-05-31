using Microsoft.Extensions.DependencyInjection;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Domain.Users;

namespace UniAuth.Domain
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUsernamesAuthService, UsernamesAuthService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
