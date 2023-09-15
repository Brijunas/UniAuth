using UniAuth.Domain.Users;

namespace UniAuth.Domain.Auth
{
    public interface IJwtService
    {
        JwtToken CreateToken(User user);
    }
}
