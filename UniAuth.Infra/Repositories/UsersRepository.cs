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
    }
}
