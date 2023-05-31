using MongoDB.Driver;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class UsernamesAuthRepository : IUsernamesAuthRepository
    {
        private readonly IMongoCollection<UsernameAuth> usernamesAuthCollection;

        public UsernamesAuthRepository(IMongoContext mongoContext)
        {
            usernamesAuthCollection = mongoContext.Database.GetCollection<UsernameAuth>("UsernamesAuth");
        }

        public Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken = default) =>
            usernamesAuthCollection.InsertOneAsync(usernameAuth, cancellationToken: cancellationToken);
    }
}
