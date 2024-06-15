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
        


        public UserServiceImp()
        {
           
        }

        public async Task<UserRegisterResponse> RegisterUser(UserRegisterRequestDto user)
        {
            throw new NotImplementedException();
        }
    }
}
