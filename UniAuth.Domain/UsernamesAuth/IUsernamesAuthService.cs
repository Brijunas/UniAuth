namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthService
    {
        Task Register(string username, string password, CancellationToken cancellationToken = default);
    }
}
