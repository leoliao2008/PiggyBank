using Contract.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankDbContext(IOptionsMonitor<PiggyBankDbConnectionBuilder> opt) : IDbContext
    {

        public IDbConnectionBuilder GetConnectionBuilder()
        {
            return opt.CurrentValue;
        }

        public Task<PiggyBankUserEntity> Insert(UserRegisterRequestDto dto)
        {
            using (SqlConnection conn = GetConnectionBuilder().GetDbConnection()) { 
                
            
            }

            return null;
        }

        public Task<UserRegisterResponseDto> QueryByNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
