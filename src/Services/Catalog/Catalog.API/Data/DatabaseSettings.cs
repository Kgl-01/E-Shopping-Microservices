namespace Catalog.API.Data
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
        public string CollectionName { get; init; }
    }
}
