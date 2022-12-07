using event_store_api.Config;
using event_store_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace event_store_api.Repository
{
    public class GenericEventEntityRepository
    {
        private readonly IMongoCollection<EventEntity> _eventsCollection;

        public GenericEventEntityRepository(
            IOptions<EventStoreDatabaseConfig> eventStoreDatabaseConfig)
        {

            MongoCredential credential = MongoCredential.CreateCredential("admin", eventStoreDatabaseConfig.Value.Username, eventStoreDatabaseConfig.Value.Password);
            var settings = new MongoClientSettings
            {
                Credential = credential,
                Server = new MongoServerAddress(eventStoreDatabaseConfig.Value.Host, eventStoreDatabaseConfig.Value.Port)
            };
            var mongoClient = new MongoClient(settings);

            var mongoDatabase = mongoClient.GetDatabase(
                eventStoreDatabaseConfig.Value.DatabaseName);

            _eventsCollection = mongoDatabase.GetCollection<EventEntity>(
                eventStoreDatabaseConfig.Value.EventsCollectionName);
        }

        public async Task<List<EventEntity>> GetAsync() =>
            await _eventsCollection.Find(_ => true).ToListAsync();

        public async Task<EventEntity?> GetAsync(string id) =>
            await _eventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(EventEntity newBook) =>
            await _eventsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, EventEntity updatedBook) =>
            await _eventsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _eventsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
