using Contract.Dtos;
using Microsoft.AspNetCore.Identity;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Extentions;
using PiggyBankAuthenApi.Jwt;
using Responses;
using System.Text.Json.Serialization;
using UserContract;

namespace PiggyBankAuthenApi.Services
{
    public class PiggyBankUserService(IDbContext db, IJwtGenerator tokenGenerator, ILogger<PiggyBankUserService> logger) : IUserService
    {

        public async Task<UserRegisterResponse> RegisterUser(UserRequestDto user)
        {
            UserRegisterResponse response = new UserRegisterResponse();
            try
            {
                PiggyBankUserEntity entity = await db.Insert(user);
                UserResponseDto dto = entity.ToUserResponseDto();
                dto.Token = tokenGenerator.GenerateJwtToken(entity);
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
                response.Content = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Code = 400;
                response.Message = ex.Message;
                response.Content = null;
            }
            return await Task.FromResult(response);

        }
    }
}
