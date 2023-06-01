using MongoDB.Driver;
using UniAuth.Domain.Users;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(IMongoContext mongoContext) : base(mongoContext)
        {
        }

        public Task Create(User user, CancellationToken cancellationToken = default) =>
            collection.InsertOneAsync(user, cancellationToken: cancellationToken);

        public async Task<User> Get(string usernameAuthId, CancellationToken cancellationToken = default)
        {
            var users = await collection.FindAsync(f => f.UsernameAuthId == usernameAuthId, cancellationToken: cancellationToken);
            var singleUser = await users.SingleOrDefaultAsync(cancellationToken: cancellationToken);
            return singleUser is not null ? singleUser : throw new KeyNotFoundException("User not found.");
        }
    }
}
