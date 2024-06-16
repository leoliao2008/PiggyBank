using Contract.Dtos;
using Microsoft.AspNetCore.Identity;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Extentions;
using Responses;
using UserContract;

namespace PiggyBankAuthenApi.Services
{
    public class UserServiceImp : IUserService
    {
        private UserManager<PiggyBankUserEntity> _userManager;

        private Logger<UserServiceImp> _logger;

        public UserServiceImp(Logger<UserServiceImp> logger, UserManager<PiggyBankUserEntity> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequestDto user)
        {
            UserRegisterResponse resonse = new UserRegisterResponse();
            PiggyBankUserEntity entity = user.ToUserEntity();
            string phoneNumber = await _userManager.GetPhoneNumberAsync(entity)??string.Empty;
            if (phoneNumber != string.Empty) {
                resonse.Code = 404;
                resonse.Message = "Phone Number Already Exist";
                return resonse;
            }
            await _userManager.GetUserNameAsync(entity);
            throw new NotImplementedException();
        }
    }
}
