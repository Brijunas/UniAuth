namespace UniAuth.Domain.UsernamesAuth
{
    public interface IUsernamesAuthRepository
    {
        /// <summary>
        /// Create a username authentication.
        /// </summary>
        /// <param name="usernameAuth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If authentication exist.</exception>
        Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken = default);
        Task<UsernameAuth> Get(string username, CancellationToken cancellationToken = default);
    }
}
