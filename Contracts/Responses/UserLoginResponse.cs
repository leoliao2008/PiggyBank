using Contracts.Responses.Dtos;
using Responses;

namespace Contracts.Responses
{
    public class UserLoginResponse : BaseResponse
    {
        public UserLoginResponseData? Data { get; set; }
    }
}
