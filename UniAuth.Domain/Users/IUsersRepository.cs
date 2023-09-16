namespace UniAuth.Domain.Users
{
    public interface IUsersRepository
    {
        Task<User> Create(string usernameAuthId, CancellationToken cancellationToken);
        Task<User> Get(string usernameAuthId, CancellationToken cancellationToken);
    }
}
