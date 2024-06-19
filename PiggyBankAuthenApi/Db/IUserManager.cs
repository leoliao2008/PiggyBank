using Contract.Dtos;
using Contracts.Dtos;
using Microsoft.Data.SqlClient;

namespace PiggyBankAuthenApi.Db
{
    public interface IUserManager
    {
        
        Task<PiggyBankUserEntity> CreateUserAsync(UserRequestDto dto);

        Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(string userName, string hashPw);

        Task<bool> CheckIfUserExistAsync(string userName,string? email, string phoneNumber);

        Task<bool> UpdateUser(UserUpdateDto dto);
    }
}
