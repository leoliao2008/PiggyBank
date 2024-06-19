using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Jwt;
using PiggyBankAuthenApi.Services;
using System.Text;
using UserContract;

namespace PiggyBankAuthenApi.Extentions
{
    public static class AppBuilder
    {
        public static void AddDbContext(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddDapperDbContext(opt => 
            {
                opt.SetConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")!);
            });
        }

        public static void AddUserServics(this WebApplicationBuilder builder)
        {
            builder.Services.AddUserService();
        }

        //public static void AddIdentityService(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddIdentity<PiggyBankUserEntity, IdentityRole>(opt =>
        //    {
        //        opt.User.RequireUniqueEmail = false;
        //        opt.Password.RequireNonAlphanumeric = false;
        //        opt.Password.RequireUppercase = false;
        //        opt.Password.RequireLowercase = false;
        //        opt.Password.RequiredLength = 8;
        //        opt.Password.RequiredUniqueChars = 0;
        //        opt.Password.RequireDigit = false;

        //    })
        //    .AddEntityFrameworkStores<PiggyBankUserDbContext>()
        //    .AddApiEndpoints();

        //}

        public static void AddJwtAuthServices(this WebApplicationBuilder builder)
        {
            JwtSetup setup = new JwtSetup();
            builder.Configuration.GetSection("Jwt").Bind(setup);

            builder.Services.AddJwtTokenGenerator(opt => {
                opt.Issuer = setup.Issuer;
                opt.Audience = setup.Audience;
                opt.Key = setup.Key;
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                            {
                                
                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setup.Key ??
                                    throw new Exception("Jwt key not found!")));
                                opt.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = setup.Issuer,
                                    ValidAudience = setup.Audience,
                                    IssuerSigningKey = key
                                };
                            });
            builder.Services.AddAuthorization();
        }
    }
}
