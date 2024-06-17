using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Jwt;

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
    }
}
