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

        public async Task<User> Create(string usernameAuthId, CancellationToken cancellationToken = default)
        {
            var newUser = new User { UsernameAuthId = usernameAuthId };
            await collection.InsertOneAsync(newUser, cancellationToken: cancellationToken);
            return newUser;
        }

        public Task<User> Get(string usernameAuthId, CancellationToken cancellationToken = default) =>
            collection.Find(f => f.UsernameAuthId == usernameAuthId).SingleOrDefaultAsync(cancellationToken);
    }
}
