using Contract.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using PiggyBankAuthenApi.Extentions;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserMananger : IUserManager
    {
        public Task<bool> CheckIfUserExistAsync(SqlConnection con, string userName, string? email, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<PiggyBankUserEntity> CreateUserAsync(SqlConnection con, UserRequestDto dto)
        {
            var entity  = dto.ToUserEntity();
            string cmd = """
                INSERT INTO "UserTable" (
                    UserName, 
                    PhoneNumber, 
                    Email, 
                    Gender, 
                    Password, 
                    CreateDate, 
                    PairedId, 
                    Role, 
                    Claims,
                    AvatarUrl,
                    IsDeleted
                    ) 
                VALUES (
                    @UserName, 
                    @PhoneNumber, 
                    @Email, 
                    @Gender, 
                    @Password, 
                    @CreateDate, 
                    @PairedId, 
                    @Role, 
                    @Claims,
                    @AvatarUrl,
                    @IsDeleted
                    );
                """;
            int rowsEffected = await con.ExecuteAsync( cmd, entity);
            if (rowsEffected == 1)
            {
                return await Task.FromResult(entity);
            }
            else 
            {
                throw new Exception("Create user failed");
            }
        }

        public Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(SqlConnection con, string userName, string hashPw)
        {
            throw new NotImplementedException();
        }
    }
}
