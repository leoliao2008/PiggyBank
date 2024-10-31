using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Responses;
using Contracts.Responses.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Extentions;
using PiggyBankAuthenApi.Jwt;
using Responses;
using UserContract;

namespace PiggyBankAuthenApi.Services
{
    public class PiggyBankUserService(IDbContext db, IJwtGenerator tokenGenerator, ILogger<PiggyBankUserService> logger) : IUserService
    {

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequestDto user)
        {
            UserRegisterResponse response = new UserRegisterResponse();
            try
            {
                PiggyBankUserEntity entity = await db.Insert(user);
                BaseUserInfoResponseData dto = entity.ToBaseUserInfoResponseData();
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Code = 400;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;

        }

        public async Task<UserLoginResponse> UserLogin(UserLoginRequestDto req)
        {
            UserLoginResponse response = new UserLoginResponse();
            try
            {
                PiggyBankUserEntity entity = await db.GetUserByNameAndPasswordAsync(req.Name, req.Password);
                UserLoginResponseData dto = entity.ToUserLoginResponseData(tokenGenerator);
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Code = 400;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateUser(UserUpdateRequestDto user)
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
                if (TextUtils.IsEmpty(response.Message))
                {
                    response.Message = "Update Fail";
                }
            }

            return response;
        }
    }
}
