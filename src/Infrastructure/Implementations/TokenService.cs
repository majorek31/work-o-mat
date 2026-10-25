using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Domain.User;
using WorkOMat.Domain.ValueObjects;
using WorkOMat.Domain.ValueObjects.Auth;

namespace WorkOMat.Infrastructure.Implementations;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public AccessToken GenerateAccessToken(UserId userId, Email email)
    {
        var secretKey = configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT secret key is not configured.");
        var issuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is not configured.");
        var audience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT audience is not configured.");
        var expirationMinutes = int.TryParse(configuration["Jwt:ExpirationMinutes"], out var minutes) ? minutes : 60;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return AccessToken.Create(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public RefreshToken GenerateRefreshToken(UserId userId, Email email)
    {
        // TODO: Implement more secure refresh token generation logic.
        // for now just a simple implementation, return a refresh token from random 32 characters and expiry of 7 days
        var randomBytes = new byte[32];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
            var token = Convert.ToBase64String(randomBytes);
            var expiry = DateTime.UtcNow.AddDays(7);
            return RefreshToken.Create(token, expiry);
        }
    }
}