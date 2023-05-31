using MongoDB.Driver;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class Repository<T>
    {
        protected readonly IMongoCollection<T> collection;

        public Repository(IMongoContext mongoContext)
        {
            collection = mongoContext.Database.GetCollection<T>($"{typeof(T).Name}s");
        }

        public Repository(IMongoContext mongoContext, string collectionName)
        {
            collection = mongoContext.Database.GetCollection<T>(collectionName);
        }
    }
}
