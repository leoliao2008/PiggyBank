using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Microsoft.AspNetCore.Identity;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Extentions;
using PiggyBankAuthenApi.Jwt;
using Responses;
using System.Text.Json.Serialization;
using UserContract;
using static Dapper.SqlMapper;

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
            return response;

        }

        public async Task<UserRegisterResponse> UserLogin(string userName, string pw)
        {
            UserRegisterResponse response = new UserRegisterResponse();
            try
            {
                PiggyBankUserEntity entity = await db.QueryByNameAndPassword(userName, CrytographyUtils.HashPassword(pw));
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
            return response;
        }

        public async Task<BaseResponse> UpdateUser(UserUpdateDto user)
        {
            BaseResponse response = new BaseResponse();
            bool success = false;
            try 
            {
                success = await db.Update(user);
            } 
            catch (Exception ex) 
            {
                response.Message = ex.Message;
                logger.LogError(ex.Message);
            }

            if (success)
            {
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
            }
            else 
            {
                response.IsSuccess = false;
                response.Code = 400;
                if (TextUtils.IsEmpty(response.Message)) {
                    response.Message = "Update Fail";
                }
            }

            return response;
        }
    }
}
