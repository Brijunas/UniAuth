namespace UniAuth.Infra.Database
{
    public class MongoContextSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
