using UniAuth.Domain.Users;

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

        public Task<bool> Register(UsernameAuth usernameAuth, CancellationToken cancellationToken = default)
        {
            // Create a usernames authentication for a user.
            usernamesAuthRepository.Create(usernameAuth, cancellationToken);
            if (usernameAuth.Id is null)
                return Task.FromResult(false);

            // Create a user with usernames authentication.
            var user = usersService.Create(usernameAuth.Id, cancellationToken);
            return Task.FromResult(user?.Id is not null);
        }
    }
}
