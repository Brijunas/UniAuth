namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthRepository
    {
        Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken);
        Task<UsernameAuth> Get(string username, CancellationToken cancellationToken);
    }
}
