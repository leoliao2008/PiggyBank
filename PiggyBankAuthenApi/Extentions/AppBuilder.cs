using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PiggyBankAuthenApi.Db;
using System.Text;

namespace PiggyBankAuthenApi.Extentions
{
    public static class AppBuilder
    {
        public static void AddDbContext(this WebApplicationBuilder builder)
        {
            //builder.Services.AddDbContext<PiggyBankUserDbContext>(opt =>
            //{
            //    string sql;
            //    if (builder.Environment.IsDevelopment())
            //    {
            //        sql = builder.Configuration.GetConnectionString("Develope") ?? throw new Exception("Connection string is not set");
            //    }
            //    else
            //    {
            //        sql = builder.Configuration.GetConnectionString("Production") ?? throw new Exception("Connection string is not set");
            //    }
            //    opt.UseSqlServer(sql);
            //});
        }

        public static void AddIdentityService(this WebApplicationBuilder builder)
        {
            //builder.Services.AddIdentity<PiggyBankUserEntity, IdentityRole>(opt =>
            //{
            //    opt.User.RequireUniqueEmail = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireLowercase = false;
            //    opt.Password.RequiredLength = 8;
            //    opt.Password.RequiredUniqueChars = 0;
            //    opt.Password.RequireDigit = false;

            //})
            //.AddEntityFrameworkStores<PiggyBankUserDbContext>()
            //.AddApiEndpoints();

        }

        public static void AddJwtAuthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                            {
                                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"] ??
                                    throw new Exception("Jwt key not found!")));
                                opt.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                    ValidAudience = builder.Configuration["Jwt:audience"],
                                    IssuerSigningKey = key
                                };
                            });
            builder.Services.AddAuthorization();
        }
    }
}
