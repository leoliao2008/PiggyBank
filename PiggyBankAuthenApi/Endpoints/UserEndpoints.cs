using Carter;
using Contract.Dtos;
using Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UserContract;

namespace PiggyBankAuthenApi.Endpoints
{
    public class UserEndpoints: CarterModule
    {
        public UserEndpoints() :base("user"){
            //RequireAuthorization();
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("register", async (UserRegisterRequestDto dto, IUserService userService) =>
            {
                return await userService.RegisterUser(dto);

            }).AllowAnonymous();

            app.MapPost("login",async (UserLoginRequestDto dto, IUserService userService) => 
            {
                return await userService.UserLogin(dto);
            }).AllowAnonymous();

            app.MapPost("update", async (UserUpdateRequestDto dto, IUserService userService) =>
            {
                return await userService.UpdateUser(dto);
            }).RequireAuthorization();
        }
    }
}
