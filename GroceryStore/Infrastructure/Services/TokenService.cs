namespace GroceryStore.Infrastructure.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.Entities.User;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration configuration)
{
    public string GenerateAccesToken(AppUser user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new ("firstName", user.FirstName),
            new ("lastName", user.LastName),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var keyString = configuration["JWT_TOKEN_KEY"]
                        ?? throw new InvalidOperationException("JWT Key is missing!");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "GroceryStore",
            audience: "GroceryStore",
            claims: claims,
            expires: DateTime.Now.AddHours(6),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}