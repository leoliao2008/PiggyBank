using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Dapper;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PiggyBankAuthenApi.Extentions;
using System.Data;
using System.Reflection;
using System.Security.Claims;
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
            if (!PhoneNumberValidator.IsValidPhoneNumber(dto.PhoneNumber!)) {
                throw new Exception("Invalid Phone Number");
            }

            if (dto.Email != null && !EmailValidator.IsValidEmail(dto.Email)) { 
                throw new Exception("Invalid Email");
            }


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
                    IsDeleted,
                    LastUpdateTime
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
                    @IsDeleted,
                    @LastUpdateTime
                    );
                """;
            int rowsEffected = await con.ExecuteAsync( cmd, entity);
            if (rowsEffected == 1)
            {
                return entity;
            }
            else 
            {
                throw new Exception("Create user failed");
            }
        }

        public async Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(string userName, string hashPw)
        {
            if (TextUtils.IsEmpty(userName) || TextUtils.IsEmpty(hashPw)) {
                throw new Exception("Incorrect Username or Password");
            }
            string cmd = """
                    SELECT * FROM "UserTable" WHERE UserName=@userName AND Password=@hashPw AND IsDeleted=0
                """;
            using SqlConnection con =  CreateConnection();
            PiggyBankUserEntity? entity = await con.QueryFirstOrDefaultAsync<PiggyBankUserEntity>(cmd, new { userName, hashPw });
            if (entity == null) 
            {
                throw new Exception("Incorrect Username or Password");    
            }
            else
            {
                return entity;
            }
        }

        public async Task<bool> UpdateUser(UserUpdateDto dto)
        {
            if (!PhoneNumberValidator.IsValidPhoneNumber(dto.PhoneNumber!))
            {
                throw new Exception("Invalid Phone Number");
            }

            if (dto.Email != null && !EmailValidator.IsValidEmail(dto.Email))
            {
                throw new Exception("Invalid Email");
            }
            using SqlConnection con = CreateConnection();
            string cmd = """
                   UPDATE "UserTable" 
                   SET UserName =@userName,
                       PhoneNumber =@phoneNumber,
                       Email =@email,
                       Gender =@gender,
                       Password =@password,
                       AvatarUrl =@avatarUrl,
                       LastUpdateTime =@lastUpdateTime
                   WHERE 
                       Id =@id;
                """;
            int rowsEffected =  await con.ExecuteAsync(
                cmd,
                new {
                    userName = dto.Name,
                    phoneNumber = dto.PhoneNumber,
                    email = dto.Email,
                    gender = dto.Gender,
                    password = dto.Password,
                    avatarUrl = dto.AvatarUrl,
                    lastUpdateTime = DateTime.UtcNow,
                    id = dto.Id
                });
            return rowsEffected > 0;     
        }
    }
}
