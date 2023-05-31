using MongoDB.Driver;
using UniAuth.Infra.Database;

namespace UniAuth.Infra.Repositories
{
    internal class Repository<TModel> where TModel : class
    {
        private protected readonly IMongoCollection<TModel> collection;

        public Repository(IMongoContext mongoContext)
        {
            collection = mongoContext.Database.GetCollection<TModel>($"{typeof(TModel).Name}s");
        }

        public Repository(IMongoContext mongoContext, string collectionName)
        {
            collection = mongoContext.Database.GetCollection<TModel>(collectionName);
        }
    }
}
