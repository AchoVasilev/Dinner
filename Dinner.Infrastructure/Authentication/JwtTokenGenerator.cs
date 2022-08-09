namespace Dinner.Infrastructure.Authentication;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings options;
    private readonly IDateTimeProvider dateTimeProvider;

    public JwtTokenGenerator(IOptions<JwtSettings> options, IDateTimeProvider dateTimeProvider)
    {
        this.options = options.Value;
        this.dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: this.options.Issuer,
            audience: this.options.Audience,
            expires: this.dateTimeProvider.UtcNow.AddMinutes(this.options.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}