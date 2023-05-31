namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthService
    {
        Task<bool> Register(UsernameAuth usernameAuth, CancellationToken cancellationToken = default);
    }
}
