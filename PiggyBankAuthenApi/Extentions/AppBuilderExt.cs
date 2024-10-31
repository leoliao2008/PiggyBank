using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PiggyBankAuthenApi.Jwt;
using System.Text;

namespace PiggyBankAuthenApi.Extentions
{
    public static class AppBuilderExt
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

        public static void AddJwtAuthServices(this WebApplicationBuilder builder)
        {
            JwtSetup setup = new JwtSetup();
            builder.Configuration.GetSection("Jwt").Bind(setup);

            builder.Services.AddJwtTokenGenerator(opt =>
            {
                opt.Issuer = setup.Issuer;
                opt.Audience = setup.Audience;
                opt.Key = setup.Key;
            });
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setup.Key ??throw new Exception("Jwt key not found!")));
                opt.RequireHttpsMetadata = true;
                opt.SaveToken = true;
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
                //this is the handler for jwt authentication
                opt.Events = new JwtBearerEvents()
                {

                };
            });
            builder.Services.AddAuthorizationPolicyEvaluator();
            builder.Services.AddAuthorization();
        }
    }
}
