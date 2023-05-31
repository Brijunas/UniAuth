using UniAuth.Domain.UsernamesAuth;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class UsernamesAuthRepository : Repository<UsernameAuth>, IUsernamesAuthRepository
    {
        public UsernamesAuthRepository(IMongoContext mongoContext) : base(mongoContext, "UsernamesAuth")
        {
        }

        public Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken = default) =>
            collection.InsertOneAsync(usernameAuth, cancellationToken: cancellationToken);
    }
}
