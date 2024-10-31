using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Responses.Dtos;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Jwt;

namespace PiggyBankAuthenApi.Extentions
{
    public static class UserExt
    {
        public static PiggyBankUserEntity ToUserEntity(this UserRegisterRequestDto dto)
        {
            PiggyBankUserEntity entity = new()
            {
                UserName = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                AvatarUrl = dto.AvatarUrl??string.Empty,
                Password = dto.Password,
                CreateDate = DateTime.UtcNow,
                LastUpdateTime = DateTime.UtcNow,
                PairedGroupId = -1,
                Role = "User",
                IsDeleted = false,
            };

            return entity;
        }


        public static BaseUserInfoResponseData ToBaseUserInfoResponseData(this PiggyBankUserEntity entity)
        {
            BaseUserInfoResponseData dto = new BaseUserInfoResponseData();
            dto.AvatarUrl = entity.AvatarUrl;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Gender = entity.Gender;
            dto.AvatarUrl = entity.AvatarUrl;
            dto.Name = entity.UserName;
            dto.Email = entity.Email;
            dto.Id = entity.Id;
            dto.CreateDate = entity.CreateDate;
            dto.LastUpdateTime = entity.LastUpdateTime;
            dto.PairedGroupId = entity.PairedGroupId;
            dto.Role = entity.Role;
            return dto;
        }


        public static UserLoginResponseData ToUserLoginResponseData(this PiggyBankUserEntity entity,IJwtGenerator tokenGenerator)
        {
            UserLoginResponseData dto = new UserLoginResponseData();
            dto.AvatarUrl = entity.AvatarUrl;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Gender = entity.Gender;
            dto.AvatarUrl = entity.AvatarUrl;
            dto.Name = entity.UserName;
            dto.Email = entity.Email;
            dto.Id = entity.Id;
            dto.PairedGroupId = entity.PairedGroupId;
            dto.Token = tokenGenerator.GenerateJwtToken(entity);
            dto.Role = entity.Role;
            dto.CreateDate = entity.CreateDate;
            dto.LastUpdateTime = entity.LastUpdateTime;
            return dto;
        }
    }
}
