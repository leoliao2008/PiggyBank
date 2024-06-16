using Contract.Dtos;
using Responses;

namespace UserContract
{
    public interface IUserService
    {
        Task<UserRegisterResponse> RegisterUser(UserRegisterRequestDto user);
    }
}
