using UniAuth.Domain.Users;

namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthService
    {
        Task<User> Register(string username, string password, CancellationToken cancellationToken);
        Task<User> Login(string username, string password, CancellationToken cancellationToken);
    }
}
