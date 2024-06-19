using Contract.Dtos;
using Contracts.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbContext
    {
        public Task<PiggyBankUserEntity> Insert(UserRequestDto dto);

        public Task<PiggyBankUserEntity> QueryByNameAndPassword(string userName, string password);

        public Task<bool> Update(UserUpdateDto dto);
    }
}
