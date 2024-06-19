using CommonLib.Utils;
using Contract.Dtos;
using Contracts.Dtos;
using PiggyBankAuthenApi.Db;

namespace PiggyBankAuthenApi.Extentions
{
    public static class UserMapper
    {
        public static PiggyBankUserEntity ToUserEntity(this UserRequestDto dto)
        {
            PiggyBankUserEntity entity = new()
            {
                UserName = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                AvatarUrl = dto.AvatarUrl??string.Empty,
                Password = CrytographyUtils.HashPassword(dto.Password!),
                CreateDate = DateTime.UtcNow,
                LastUpdateTime = DateTime.UtcNow,
                PairedId = -1,
                Role = "User",
                Claims = "",
                IsDeleted = false,
            };

            return entity;
        }

        public static UserResponseDto ToUserResponseDto(this PiggyBankUserEntity entity)
        {
            UserResponseDto dto = new UserResponseDto();
            dto.AvatarUrl = entity.AvatarUrl;
            dto.PhoneNumber = entity.PhoneNumber;
            dto.Gender = entity.Gender;
            dto.AvatarUrl = entity.AvatarUrl;
            dto.Name = entity.UserName;
            dto.Email = entity.Email;
            dto.Id = entity.Id;
            dto.Token = string.Empty;
            return dto;
        }
    }
}
