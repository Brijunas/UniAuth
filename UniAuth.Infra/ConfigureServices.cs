using Microsoft.Extensions.DependencyInjection;
using UniAuth.Domain.Auth;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Domain.Users;
using UniAuth.Infra.Auth;
using UniAuth.Infra.Database;
using UniAuth.Infra.Repositories;

namespace UniAuth.Infra
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IUsernamesAuthRepository, UsernamesAuthRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
