using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Request;
using Contracts.Responses.Dtos;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbContext(IUserManager userManager) : IDbContext
    {
        public async Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto)
        {
            return await userManager.CreateUserAsync(dto);
        }

        public async Task<PiggyBankUserEntity> GetUserByNameAndPasswordAsync(string name, string password)
        {
            return await userManager.FindUserByNameAndPasswordAsync(name, password);
        }

        public async Task<bool> Update(UserUpdateRequestDto dto)
        {
            return await userManager.UpdateUser(dto);
        }

        public async Task<bool> CheckIfNameExist(string name)
        {
            var isExist = await userManager.CheckIfNameExist(name);
            return isExist;
        }

        public async Task<bool> CheckIfCellphoneExist(string cellphone)
        {
            return await userManager.CheckIfCellphoneExist(cellphone);
        }

        public async Task<bool> CheckIfEmailExist(string email)
        {
            return await userManager.CheckIfEmailExist(email);
        }

        public async Task<TransferEntity> AddTransferRecord(InsertTransferRequestDto dto)
        {
            return await userManager.AddTransferAsync(dto);
        }
    }
}