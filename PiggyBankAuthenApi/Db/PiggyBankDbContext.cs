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

        public async Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto)
        {
          
            return await userManager.CreateUserAsync(dto);
        }

        public async Task<PiggyBankUserEntity> GetUserByNameAndPasswordAsync(string name, string password)
        {
            return await userManager.FindUserByNameAndPasswordAsync(name,password);
        }

        public async Task<bool> Update(UserUpdateRequestDto dto)
        {
            return await userManager.UpdateUser(dto);
        }
    }
}
