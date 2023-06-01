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

        public async Task Register(string username, string password, CancellationToken cancellationToken = default)
        {
            // Ensure username is not banned.
            if (Usernames.Banned.Contains(username.ToLower()))
                throw new ArgumentException($"Username {username} already exist.");

            // Create a usernames authentication for a user.
            var usernameAuth = new UsernameAuth
            {
                // Ensure username is lowercase.
                Username = username.ToLower(),
                // Hash the password.
                Password = BC.HashPassword(password)
            };
            await usernamesAuthRepository.Create(usernameAuth, cancellationToken);
            if (usernameAuth.Id is null)
                throw new InvalidOperationException("Unable to create user authentication.");

            // Create a user with usernames authentication.
            var user = await usersService.Create(usernameAuth.Id, cancellationToken);
            if (user?.Id is null)
                throw new InvalidOperationException("Unable to create user.");
        }
    }
}
