using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DotNetMulakat.App;

public class JwtProvider
{
    public string CreateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123 MySecretKey123"));
        JwtSecurityToken securityToken = new(
            issuer:"Kadir Gökçe",
            audience:"Mulakat",
            signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512));
        JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
        return securityTokenHandler.WriteToken(securityToken);
    }
}
