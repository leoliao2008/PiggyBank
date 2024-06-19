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
            
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("register", async (UserRequestDto dto, IUserService userService) =>
            {
                return await userService.RegisterUser(dto);

            });

            app.MapPost("login",async (UserLoginDto dto, IUserService userService) => 
            {
                return await userService.UserLogin(dto.Name!,dto.Password!);
            });

            app.MapPost("update", async (UserUpdateDto dto, IUserService userService)=>
            { 
                return await userService.UpdateUser(dto);
            });
        }
    }
}
