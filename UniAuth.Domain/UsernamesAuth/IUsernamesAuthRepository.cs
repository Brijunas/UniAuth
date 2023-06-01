namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthRepository
    {
        Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken = default);
        Task<UsernameAuth?> Get(string username, CancellationToken cancellationToken = default);
    }
}
