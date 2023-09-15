namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthService
    {
        Task<AuthenticatedUser> Register(string username, string password, CancellationToken cancellationToken = default);
        Task<AuthenticatedUser> Login(string username, string password, CancellationToken cancellationToken = default);
    }
}
