using System.Security.Authentication;
using UniAuth.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace UniAuth.Domain.UsernamesAuth
{
    internal class UsernamesAuthService : IUsernamesAuthService
    {
        private readonly IUsernamesAuthRepository usernamesAuthRepository;
        private readonly IUsersRepository usersRepository;

        public UsernamesAuthService(IUsernamesAuthRepository usernamesAuthRepository, IUsersRepository usersRepository)
        {
            this.usernamesAuthRepository = usernamesAuthRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<User> Login(string username, string password, CancellationToken cancellationToken = default)
        {
            var lowerUsername = username.ToLower();

            // get the user authentication
            var usernameAuth = await usernamesAuthRepository.Get(lowerUsername, cancellationToken);
            if (usernameAuth is null || usernameAuth.Id is null || !BC.Verify(password, usernameAuth.Password))
                throw new InvalidCredentialException();

            // get the user
            var user = await usersRepository.Get(usernameAuth.Id, cancellationToken);
            if (user?.Id is null)
                throw new KeyNotFoundException("User not found.");

            return user;
        }

        public async Task<User> Register(string username, string password, CancellationToken cancellationToken = default)
        {
            var lowerUsername = username.ToLower();

            // Ensure username is not banned.
            if (Usernames.Banned.Contains(lowerUsername))
                throw new ArgumentException($"Username {username} already exist.");

            // Create a usernames authentication for a user.
            var usernameAuth = new UsernameAuth
            {
                // Ensure username is lowercase.
                Username = lowerUsername,
                // Hash the password.
                Password = BC.HashPassword(password)
            };
            await usernamesAuthRepository.Create(usernameAuth, cancellationToken);
            if (usernameAuth.Id is null)
                throw new InvalidOperationException("Unable to create user authentication.");

            // Create a user with usernames authentication.
            var user = await usersRepository.Create(usernameAuth.Id, cancellationToken);
            if (user?.Id is null)
                throw new InvalidOperationException("Unable to create user.");

            return user;
        }
    }
}
