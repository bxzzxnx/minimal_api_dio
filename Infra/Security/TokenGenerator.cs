using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Minimal01.Domain.Entities;
using Minimal01.Domain.Interfaces;

namespace Minimal01.Infra.Security;

public class TokenGenerator : ITokenGenerator
{
    private readonly  IConfiguration _configuration;
    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(Admin admin)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claim = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, admin.Email),
            new("Profile", admin.Profile)
        };
    
        var handler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = new ClaimsIdentity(claim),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}