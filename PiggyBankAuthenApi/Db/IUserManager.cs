using Contract.Dtos;
using Microsoft.Data.SqlClient;

namespace PiggyBankAuthenApi.Db
{
    public interface IUserManager
    {
        Task<PiggyBankUserEntity> CreateUserAsync(SqlConnection con, UserRequestDto dto);

        Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(SqlConnection con, string userName, string hashPw);

        Task<bool> CheckIfUserExistAsync(SqlConnection con, string userName,string? email, string phoneNumber);
    }
}
