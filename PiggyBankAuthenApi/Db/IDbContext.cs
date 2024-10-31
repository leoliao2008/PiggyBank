using Contract.Dtos;
using Contracts.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbContext
    {
        public Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto);

        public Task<PiggyBankUserEntity> GetUserByNameAndPasswordAsync(string name, string pw);

        public Task<bool> Update(UserUpdateRequestDto dto);
    }
}
