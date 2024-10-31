using Carter;
using Microsoft.OpenApi.Models;
using PiggyBankAuthenApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.AddSecurityDefinition("JwtBearer999", new OpenApiSecurityScheme()
    {
        Description = "请求头中输入Jwt口令",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"

    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference()
        { 
            Id = "JwtBearer999",
            Type = ReferenceType.SecurityScheme
        }
    };
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { scheme,Array.Empty<string>()}
    });
});
builder.Services.AddCarter();
builder.AddDbContext();
builder.AddUserServics();
builder.AddJwtAuthServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();


app.Run();


