using MongoDB.Driver;
using UniAuth.Domain.Users;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<User> usersCollection;

        public UsersRepository(IMongoContext mongoContext)
        {
            usersCollection = mongoContext.Database.GetCollection<User>($"{nameof(User)}s");
        }

        public Task Create(User user, CancellationToken cancellationToken = default) =>
            usersCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
    }
}
