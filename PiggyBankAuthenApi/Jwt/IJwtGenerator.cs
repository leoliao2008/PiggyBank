using PiggyBankAuthenApi.Db;

namespace PiggyBankAuthenApi.Jwt
{
    public interface IJwtGenerator
    {
        public string GenerateJwtToken(PiggyBankUserEntity entity);
    }
}
