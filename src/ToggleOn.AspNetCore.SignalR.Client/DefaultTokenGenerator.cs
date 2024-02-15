using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ToggleOn.AspNetCore.SignalR.Client;

internal class DefaultTokenGenerator : ITokenGenerator
{
    private static readonly JwtSecurityTokenHandler JwtTokenHandler = new();
     
    public string Generate(string audience, string accessKey)
    {
        var expire = DateTime.UtcNow.Add(TimeSpan.FromHours(1));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = JwtTokenHandler.CreateJwtSecurityToken(
            issuer: null,
            audience: audience,
            subject: null,
            expires: expire,
            signingCredentials: credentials);

        return JwtTokenHandler.WriteToken(token);
    }
}
