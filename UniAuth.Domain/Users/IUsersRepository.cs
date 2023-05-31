namespace UniAuth.Domain.Users
{
    public interface IUsersRepository
    {
        Task Create(User user, CancellationToken cancellationToken = default);
    }
}
