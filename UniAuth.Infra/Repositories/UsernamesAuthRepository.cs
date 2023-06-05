using MongoDB.Driver;
using System.Security.Authentication;
using UniAuth.Domain.UsernamesAuth;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class UsernamesAuthRepository : Repository<UsernameAuth>, IUsernamesAuthRepository
    {
        public UsernamesAuthRepository(IMongoContext mongoContext) : base(mongoContext, "UsernamesAuth")
        {
        }

        public async Task Create(UsernameAuth usernameAuth, CancellationToken cancellationToken = default)
        {
            try
            {
                await collection.InsertOneAsync(usernameAuth, cancellationToken: cancellationToken);
            }
            catch (MongoWriteException e)
            {
                if (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
                    throw new ArgumentException($"Username {usernameAuth.Username} already exist.");

                throw;
            }
        }

        public Task<UsernameAuth> Get(string username, CancellationToken cancellationToken = default) =>
            collection.Find(f => f.Username == username).SingleOrDefaultAsync(cancellationToken);

        // Ensure index for username is unique.
        // Need proper implementation for this.
        public async Task EnsureIndexes()
        {
            const string indexName = "unique_Username";
            var cursor = await collection.Indexes.ListAsync();
            var indexes = await cursor.ToListAsync();

            bool indexExists = false;
            foreach (var index in indexes)
            {
                if (index["name"] == indexName)
                {
                    indexExists = true;
                    break;
                }
            }

            if (!indexExists)
            {
                var options = new CreateIndexOptions() { Unique = true };
                var field = new StringFieldDefinition<UsernameAuth>(indexName);
                var indexDefinition = new IndexKeysDefinitionBuilder<UsernameAuth>().Ascending(field);
                await collection.Indexes.CreateOneAsync(new CreateIndexModel<UsernameAuth>(indexDefinition, options));
            }
        }
    }
}
