using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Request;
using Contracts.Responses;
using Responses;

namespace UserContract
{
    public interface IUserService
    {
        Task<UserLoginResponse> RegisterUser(UserRegisterRequestDto req);

        Task<UserLoginResponse> UserLogin(UserLoginRequestDto req);

        Task<BaseResponse> UpdateUser(UserUpdateRequestDto req);

        Task<BaseResponse> CheckIfNameExist(string name);

        Task<BaseResponse> CheckIfEmailExist(string email);

        Task<BaseResponse> CheckIfCellphoneExist(string cellphone);

        Task<InsertTransferRecordResponse> InsertTransferRecord(InsertTransferRequestDto req);


    }

}
