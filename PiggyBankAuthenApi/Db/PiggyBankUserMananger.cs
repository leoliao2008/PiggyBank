using Contract.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PiggyBankAuthenApi.Extentions;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserMananger(IOptions<PiggyBankDbConnectionBuilder> opt) : IUserManager
    {
        public Task<bool> CheckIfUserExistAsync(string userName, string? email, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<PiggyBankUserEntity> CreateUserAsync(UserRequestDto dto)
        {
            using SqlConnection con = opt.Value.GetDbConnection();
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

        public Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(string userName, string hashPw)
        {
            throw new NotImplementedException();
        }
    }
}
