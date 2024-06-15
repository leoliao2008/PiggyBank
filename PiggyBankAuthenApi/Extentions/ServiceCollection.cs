using PiggyBankAuthenApi.Db;

namespace PiggyBankAuthenApi.Extentions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDapperDbContext(this IServiceCollection services, Action<IDbConnectionBuilder> opt)
        {
            services.Configure<PiggyBankDbConnectionBuilder>(opt);
            services.AddScoped<IDbContext, PiggyBankDbContext>();
            return services;
        }
    }
}
