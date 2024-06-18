using Contract.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbContext(IUserManager userManager) : IDbContext
    {

        public Task<PiggyBankUserEntity> Insert(UserRequestDto dto)
        {
          
            return userManager.CreateUserAsync(dto);
        }

        public Task<UserResponseDto> QueryByNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
