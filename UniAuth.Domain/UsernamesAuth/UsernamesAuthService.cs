using UniAuth.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace UniAuth.Domain.UsernamesAuth
{
    internal class UsernamesAuthService : IUsernamesAuthService
    {
        private readonly IUsernamesAuthRepository usernamesAuthRepository;
        private readonly IUsersService usersService;

        public UsernamesAuthService(IUsernamesAuthRepository usernamesAuthRepository, IUsersService usersService)
        {
            this.usernamesAuthRepository = usernamesAuthRepository;
            this.usersService = usersService;
        }

        public Task<bool> Register(string username, string password, CancellationToken cancellationToken = default)
        {
            // Create a usernames authentication for a user.
            var usernameAuth = new UsernameAuth
            {
                // Ensure username is lowercase.
                Username = username.ToLower(),
                // Hash the password.
                Password = BC.HashPassword(password)
            };
            usernamesAuthRepository.Create(usernameAuth, cancellationToken);
            if (usernameAuth.Id is null)
                return Task.FromResult(false);

            // Create a user with usernames authentication.
            var user = usersService.Create(usernameAuth.Id, cancellationToken);
            return Task.FromResult(user?.Id is not null);
        }
    }
}
