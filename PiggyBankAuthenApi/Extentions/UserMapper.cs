using Contract.Dtos;
using PiggyBankAuthenApi.Db;

namespace PiggyBankAuthenApi.Extentions
{
    public static class UserMapper
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
                CreateDate = DateTime.Now,

            };

            return entity;
        }
    }
}
