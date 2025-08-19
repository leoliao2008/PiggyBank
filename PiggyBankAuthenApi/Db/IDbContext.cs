using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Request;
using Contracts.Responses.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public interface IDbContext
    {
        public Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto);

        public Task<PiggyBankUserEntity> GetUserByNameAndPasswordAsync(string name, string pw);

        public Task<bool> Update(UserUpdateRequestDto dto);

        public Task<bool> CheckIfNameExist(string name);

        public Task<bool> CheckIfCellphoneExist(string cellphone);

        public Task<bool> CheckIfEmailExist(string email);

        public Task<TransferEntity> AddTransferRecord(InsertTransferRequestDto dto);
    }
}
