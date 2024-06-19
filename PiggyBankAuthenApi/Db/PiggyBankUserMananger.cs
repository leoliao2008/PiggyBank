using Contract.Dtos;
using Dapper;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PiggyBankAuthenApi.Extentions;
using System.Text;

namespace PiggyBankAuthenApi.Db
{
    public class PiggyBankUserMananger(IOptions<PiggyBankDbConnectionBuilder> opt) : IUserManager
    {
        public async Task<bool> CheckIfUserExistAsync(string userName, string? email, string phoneNumber)
        {
            using SqlConnection con = CreateConnection();
            StringBuilder sb = new StringBuilder();
            sb.Append("""
                SELECT * FROM "UserTable" 
                WHERE UserName = @userName OR PhoneNumber = @phoneNumber
                """);
            if (email != string.Empty || email != null) {
                sb.Append(" OR Email = @email");
            }
            sb.Append(";");
            PiggyBankUserEntity? result = await con.QueryFirstOrDefaultAsync<PiggyBankUserEntity>(sb.ToString(), new { userName, email, phoneNumber });
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private SqlConnection CreateConnection() { 
            return opt.Value.GetDbConnection();
        }

        public async Task<PiggyBankUserEntity> CreateUserAsync(UserRequestDto dto)
        {
            if (await CheckIfUserExistAsync(dto.Name,dto.Email,dto.PhoneNumber!)) {
                throw new Exception("User Already Exist");
            }
            using SqlConnection con = CreateConnection();
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
