using MongoDB.Driver;

namespace UniAuth.Infra.Database
{
    internal interface IMongoContext
    {
        public IMongoDatabase Database { get; }
        public IMongoClient Client { get; }
    }
}
