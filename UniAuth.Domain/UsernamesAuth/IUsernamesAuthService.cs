namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthService
    {
        Task<bool> Register(string username, string password, CancellationToken cancellationToken = default);
    }
}
