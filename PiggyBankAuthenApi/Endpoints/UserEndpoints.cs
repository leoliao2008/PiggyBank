using Carter;
using Contract.Dtos;
using Contracts.Dtos;
using Contracts.Request;
using UserContract;

namespace PiggyBankAuthenApi.Endpoints
{
    public class UserEndpoints() : CarterModule("user")
    {
        //RequireAuthorization();

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("helloworld", () => { return "HelloWorld"; });

            app.MapPost("register",
                async (UserRegisterRequestDto dto, IUserService userService) =>
                {
                    return await userService.RegisterUser(dto);
                });

            app.MapPost("login",
                async (UserLoginRequestDto dto, IUserService userService) =>
                {
                    return await userService.UserLogin(dto);
                });

            app.MapPost("update",
                async (UserUpdateRequestDto dto, IUserService userService) =>
                {
                    return await userService.UpdateUser(dto);
                }).RequireAuthorization();

            app.MapGet("checkIfNameExist/{name}",
                async (string name, IUserService userService) => { return await userService.CheckIfNameExist(name); });

            app.MapGet("checkIfCellphoneExist/{phone}",
                async (string phone, IUserService userService) =>
                {
                    return await userService.CheckIfCellphoneExist(phone);
                });

            app.MapGet("checkIfEmailExist/{email}",
                async (string email, IUserService userService) =>
                {
                    return await userService.CheckIfEmailExist(email);
                });
            app.MapPost("newTransferRecord",
                async (InsertTransferRequestDto dto, IUserService userService) =>
                    await userService.InsertTransferRecord(dto));
        }
    }
}