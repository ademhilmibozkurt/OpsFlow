using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Models;
using OpsFlow.Infrastructure.Settings;

namespace OpsFlow.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        // dependency injection
        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public TokenResultModel GenerateTokens(AppUser user)
        {
            // createClaims
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            // createKey
            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            // createCredentials
            SigningCredentials credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);
            
            // setExpireDate
            DateTime expires = DateTime.UtcNow.AddMinutes(15);

            // createToken
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            // accessToken 
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // refreshToken
            string refreshToken = Guid.NewGuid().ToString();

            // returnResult
            return new TokenResultModel(
                accessToken,
                refreshToken,
                expires);
        }
    }
}