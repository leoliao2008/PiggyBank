using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Responses;
using Responses;

namespace UserContract
{
    public interface IUserService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequestDto req);

        Task<UserLoginResponse> UserLogin(UserLoginRequestDto req);

        Task<BaseResponse> UpdateUser(UserUpdateRequestDto req);


    }

}
