using Carter;
using Contract.Dtos;
using Contracts.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
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
            app.MapGet("helloworld", () => {

                return "HelloWorld";
            });

            app.MapPost("register", async (UserRegisterRequestDto dto, IUserService userService) =>
            {
                return await userService.RegisterUser(dto);

            });

            app.MapPost("login", async (UserLoginRequestDto dto, IUserService userService) =>
            {
                return await userService.UserLogin(dto);
            });

            app.MapPost("update", async (UserUpdateRequestDto dto, IUserService userService) =>
            {
                return await userService.UpdateUser(dto);
            }).RequireAuthorization();

        }
    }
}
