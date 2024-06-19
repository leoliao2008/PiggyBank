using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Jwt;
using PiggyBankAuthenApi.Services;
using UserContract;

namespace PiggyBankAuthenApi.Extentions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDapperDbContext(this IServiceCollection services, Action<IDbConnectionBuilder> opt)
        {
            services.Configure<PiggyBankDbConnectionBuilder>(opt);
            services.AddScoped<IUserManager, PiggyBankUserMananger>();
            services.AddScoped<IDbContext, PiggyBankDbContext>();
            return services;
        }

        public static IServiceCollection AddJwtTokenGenerator(this IServiceCollection services, Action<JwtSetup> opt)
        {
            services.Configure<JwtSetup>(opt);
            services.AddScoped<IJwtGenerator, PiggyBankJwtGenerator>();
            return services;
        }

        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, PiggyBankUserService>();
            return services;
        }
    }
}
