using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Dapper;
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
            if (email != string.Empty || email != null)
            {
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

        private SqlConnection CreateConnection()
        {
            return opt.Value.GetDbConnection();
        }

        public async Task<PiggyBankUserEntity> CreateUserAsync(UserRegisterRequestDto dto)
        {
            if (TextUtils.IsEmpty(dto.Name))
            {
                throw new Exception("User Name Cannot Be Empty");
            }

            if (TextUtils.IsEmpty(dto.Password))
            {
                throw new Exception("Password Cannot Be Empty");
            }

            if (!PhoneNumberValidator.IsValidPhoneNumber(dto.PhoneNumber!))
            {
                throw new Exception("Invalid Phone Number");
            }

            if (!TextUtils.IsEmpty(dto.Email) && !EmailValidator.IsValidEmail(dto.Email!))
            {
                throw new Exception("Invalid Email");
            }


            if (await CheckIfUserExistAsync(dto.Name, dto.Email, dto.PhoneNumber!))
            {
                throw new Exception("User Already Exist");
            }

            using SqlConnection con = CreateConnection();
            var entity = dto.ToUserEntity();
            string cmd = """
                INSERT INTO "UserTable" (
                    UserName, 
                    PhoneNumber, 
                    Email, 
                    Gender, 
                    Password, 
                    CreateDate, 
                    PairedGroupId, 
                    Role, 
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
                    @PairedGroupId, 
                    @Role, 
                    @AvatarUrl,
                    @IsDeleted,
                    @LastUpdateTime
                    );
                """;
            int rowsEffected = await con.ExecuteAsync(cmd, entity);
            if (rowsEffected == 1)
            {
                entity = await FindUserByNameAndPasswordAsync(dto.Name, dto.Password!);
                return entity;
            }
            else
            {
                throw new Exception("Create user failed");
            }
        }

        public async Task<PiggyBankUserEntity> FindUserByNameAndPasswordAsync(string userName, string pw)
        {
            if (TextUtils.IsEmpty(userName) || TextUtils.IsEmpty(pw))
            {
                throw new Exception("Incorrect Username or Password");
            }
            string cmd = """
                    SELECT * FROM "UserTable" WHERE UserName=@userName AND Password=@pw
                """;
            using SqlConnection con = CreateConnection();
            PiggyBankUserEntity? entity = await con.QueryFirstOrDefaultAsync<PiggyBankUserEntity>(cmd, new { userName, pw });
            if (entity == null)
            {
                throw new Exception("Incorrect Username or Password");
            }
            else if (entity.IsDeleted)
            {
                throw new Exception("User is in Deactivated State");
            }
            else
            {
                return entity;
            }
        }

        public async Task<bool> UpdateUser(UserUpdateRequestDto dto)
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
            StringBuilder sb = new StringBuilder();
            sb.Append("""
                SELECT * FROM "UserTable" 
                WHERE Id !=@id AND (UserName = @userName OR PhoneNumber = @phoneNumber
                """);
            if (!TextUtils.IsEmpty(dto.Email))
            {
                sb.Append(" OR Email = @email");
            }
            sb.Append(");");

            string cmd = sb.ToString();
            PiggyBankUserEntity? entity =  await con.QueryFirstOrDefaultAsync<PiggyBankUserEntity>(
                    cmd, new
                    {
                        userName = dto.Name,
                        phoneNumber = dto.PhoneNumber,
                        email = dto.Email,
                        id = dto.Id
                    }
                );
            if (entity != null) {
                throw new Exception("Update Fails: Phone Number or User Name or Email Already Taken by Other Users");
            }

             cmd = """
                   UPDATE "UserTable" 
                   SET UserName =@userName,
                       PhoneNumber =@phoneNumber,
                       Email =@email,
                       Gender =@gender,
                       AvatarUrl =@avatarUrl,
                       LastUpdateTime =@lastUpdateTime
                   WHERE 
                       Id =@id AND IsDeleted=0
                """;
            int rowsEffected = await con.ExecuteAsync(
                cmd, new
                {
                    userName = dto.Name,
                    phoneNumber = dto.PhoneNumber,
                    email = dto.Email,
                    gender = dto.Gender,
                    avatarUrl = dto.AvatarUrl,
                    lastUpdateTime = DateTime.UtcNow,
                    id = dto.Id
                });
            return rowsEffected > 0;
        }
    }
}
