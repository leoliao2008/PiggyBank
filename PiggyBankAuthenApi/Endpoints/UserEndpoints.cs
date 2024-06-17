using Carter;
using Contract.Dtos;
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
        }
    }
}
