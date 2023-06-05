namespace UniAuth.Domain.Users
{
    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public Task<User> Create(string usernameAuthId, CancellationToken cancellationToken = default)
        {
            var user = new User { UsernameAuthId = usernameAuthId };
            usersRepository.Create(user, cancellationToken);
            return Task.FromResult(user);
        }

        public Task<User> Get(string usernameAuthId, CancellationToken cancellationToken = default)
        {
            var user = usersRepository.Get(usernameAuthId, cancellationToken);
            return user is not null ? user : throw new KeyNotFoundException("User not found.");
        }
    }
}
