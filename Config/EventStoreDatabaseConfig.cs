namespace event_store_api.Config
{
    public class EventStoreDatabaseConfig
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string EventsCollectionName { get; set; } = null!;
    }
}
