using Microsoft.Extensions.DependencyInjection;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Domain.Users;
using UniAuth.Infra.Database;
using UniAuth.Infra.Repositories;

namespace UniAuth.Infra
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IUsernamesRepository, UsernamesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
