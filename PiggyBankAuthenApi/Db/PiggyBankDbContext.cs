using Contract.Dtos;
using Contracts.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PiggyBankAuthenApi.Extentions;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbContext(IUserManager userManager) : IDbContext
    {

        public Task<PiggyBankUserEntity> Insert(UserRequestDto dto)
        {
          
            return userManager.CreateUserAsync(dto);
        }

        public async Task<PiggyBankUserEntity> QueryByNameAndPassword(string userName, string password)
        {
            return await userManager.FindUserByNameAndPasswordAsync(userName, password);
        }

        public async Task<bool> Update(UserUpdateDto dto)
        {
            return await userManager.UpdateUser(dto);
        }
    }
}
