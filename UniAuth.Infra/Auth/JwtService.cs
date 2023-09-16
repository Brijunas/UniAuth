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

        public string GenerateToken(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentException();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("sub_id", user.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
