namespace event_store_api.Config
{
    public class EventStoreDatabaseConfig
    {

        public string DatabaseName { get; set; } = null!;

        public string EventsCollectionName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Host { get; set; } = null!;

        public int Port { get; set; } = 27017;
    }
}
