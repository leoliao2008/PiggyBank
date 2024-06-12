using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserManager : UserManager<PiggyBankUserEntity>
    {
        public PiggyBankUserManager(IUserStore<PiggyBankUserEntity> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<PiggyBankUserEntity> passwordHasher, IEnumerable<IUserValidator<PiggyBankUserEntity>> userValidators, IEnumerable<IPasswordValidator<PiggyBankUserEntity>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<PiggyBankUserEntity>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
