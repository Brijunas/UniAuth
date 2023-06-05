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

        public Task<User> Get(string usernameAuthId, CancellationToken cancellationToken = default) =>
            collection.Find(f => f.UsernameAuthId == usernameAuthId).SingleOrDefaultAsync(cancellationToken);
    }
}
