using Contract.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbContext(IOptions<PiggyBankDbConnectionBuilder> opt,IUserManager userManager) : IDbContext
    {

        public IDbConnectionBuilder GetConnectionBuilder()
        {
            return opt.Value;
        }

        public Task<PiggyBankUserEntity> Insert(UserRequestDto dto)
        {
            using SqlConnection conn = GetConnectionBuilder().GetDbConnection();
            conn.Open();
            return userManager.CreateUserAsync(conn, dto);
        }

        public Task<UserResponseDto> QueryByNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
