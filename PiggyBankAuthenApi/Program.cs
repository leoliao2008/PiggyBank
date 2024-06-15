using PiggyBankAuthenApi.Db;
using PiggyBankAuthenApi.Endpoints;
using PiggyBankAuthenApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.ConfigureOptions
builder.AddDbContext();
builder.AddIdentityService();
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

UserEndpoints.Map(app);

app.Run();


