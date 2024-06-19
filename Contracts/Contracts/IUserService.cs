using Contract.Dtos;
using Contracts.Dtos;
using Responses;

namespace UserContract
{
    public interface IUserService
    {
        Task<UserRegisterResponse> RegisterUser(UserRequestDto user);

        Task<UserRegisterResponse> UserLogin(string userName, string pw);

        Task<BaseResponse> UpdateUser(UserUpdateDto user);


    }

}
