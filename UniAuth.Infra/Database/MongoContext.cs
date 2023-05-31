using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace UniAuth.Infra.Database
{
    internal class MongoContext : IMongoContext
    {
        public IMongoDatabase Database { get; }
        public IMongoClient Client { get; }

        public MongoContext(IOptions<MongoContextSettings> settings)
        {
            // conventions
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, _ => true);
            var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
            ConventionRegistry.Register("StringIdStoredAsObjectId", pack, _ => true);

            // get settings
            var clientSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);

            // create client and database
            Client = new MongoClient(clientSettings);
            Database = Client.GetDatabase(settings.Value.DatabaseName);
        }
    }
}
