using UniAuth.Domain.Users;

namespace UniAuth.Infra.Auth
{
    public interface IJwtService
    {
        JwtToken CreateToken(User user);
    }
}
