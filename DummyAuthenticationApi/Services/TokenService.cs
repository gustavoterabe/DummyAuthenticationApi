using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DummyAuthenticationApi.Models;
using DummyAuthenticationApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DummyAuthenticationApi.Services;

public class TokenService : ITokenService
{
    public string GenerateTokem(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new ();
        byte[] key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[] 
            {
                new (ClaimTypes.Name, user.Name),
                new (ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}