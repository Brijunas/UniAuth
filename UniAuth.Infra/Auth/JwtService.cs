using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniAuth.Domain.Users;

namespace UniAuth.Infra.Auth
{
    internal class JwtService : IJwtService
    {
        private readonly JwtSettings settings;

        public JwtService(IOptions<JwtSettings> settings)
        {
            this.settings = settings.Value;
        }

        public JwtToken CreateToken(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentException();

            var key = Encoding.UTF8.GetBytes(settings.Key);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = settings.Issuer,
                Audience = settings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtToken
            {
                Key = tokenHandler.WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
