using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PiggyBankAuthenApi.Db;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace PiggyBankAuthenApi.Jwt
{
    public class PiggyBankJwtGenerator(IOptions<JwtSetup> options) : IJwtGenerator
    {
        public string GenerateJwtToken(PiggyBankUserEntity entity)
        {
            JwtSetup setup = options.Value;
            Claim[] claims = [
                    new Claim(ClaimTypes.NameIdentifier, $"{entity.Id}"),
                    new Claim(ClaimTypes.Role,entity.Role??"User"),
                ];
            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setup.Key!)),
                SecurityAlgorithms.HmacSha256
                );
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: setup.Issuer,
                audience: setup.Audience,
                expires: DateTime.Now.AddDays(30),
                claims: claims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
