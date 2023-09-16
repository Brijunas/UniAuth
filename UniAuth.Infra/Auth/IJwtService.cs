using UniAuth.Domain.Users;

namespace UniAuth.Infra.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
