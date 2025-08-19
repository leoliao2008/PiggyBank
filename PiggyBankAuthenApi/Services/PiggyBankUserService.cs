using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Request;
using Contracts.Responses;
using Contracts.Responses.Dtos;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Extentions;
using PiggyBankAuthenApi.Jwt;
using Responses;
using UserContract;

namespace PiggyBankAuthenApi.Services
{
    public class PiggyBankUserService(IDbContext db, IJwtGenerator tokenGenerator, ILogger<PiggyBankUserService> logger) : IUserService
    {

        public async Task<UserLoginResponse> RegisterUser(UserRegisterRequestDto user)
        {
            UserLoginResponse response = new UserLoginResponse();
            try
            {
                PiggyBankUserEntity entity = await db.Insert(user);
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

        public async Task<BaseResponse> CheckIfNameExist(string name)
        {
            BaseResponse response = new BaseResponse();
            var isExist = await db.CheckIfNameExist(name);
            if (isExist == true)
            {
                response.IsSuccess = false;
                response.Code = 501;
                response.Message = "User Name Exist.";
            }
            else { 
                response.IsSuccess = true;
                response.Code = 200;  
                response.Message = "OK";
            }

            return response;
        }

        public async Task<BaseResponse> CheckIfEmailExist(string email)
        {
            BaseResponse response = new BaseResponse();
            if (await db.CheckIfEmailExist(email))
            {
                response.IsSuccess = false;
                response.Code = 501;
                response.Message = "Email Exist.";
            }
            else
            {
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
            }

            return response;
        }

        public async Task<BaseResponse> CheckIfCellphoneExist(string cellphone)
        {
            BaseResponse response = new BaseResponse();
            if (await db.CheckIfCellphoneExist(cellphone))
            {
                response.IsSuccess = false;
                response.Code = 501;
                response.Message = "Cellphone Exist.";
            }
            else
            {
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
            }

            return response;
        }

        public async Task<InsertTransferRecordResponse> InsertTransferRecord(InsertTransferRequestDto req)
        {
            var response = new InsertTransferRecordResponse();
            try
            {
                var entity = await db.AddTransferRecord(req);
                var data = new InsertTransferResponseData
                {
                    Id = entity.Id,
                    UserId = entity.UserId,
                    Subject = entity.Subject,
                    Direction = entity.Direction,
                    Amount = entity.Amount,
                    Comment = entity.Comment,
                    PicUrl = entity.PicUrl,
                    CreateDate = entity.CreateDate,
                    LastUpdateTime = entity.LastUpdateTime
                };
                response.IsSuccess = true;
                response.Code = 200;
                response.Message = "OK";
                response.Data = data;
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
    }
}
