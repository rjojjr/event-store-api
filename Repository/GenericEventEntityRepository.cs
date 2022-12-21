using System.Xml.Linq;
using event_store_api.Config;
using event_store_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SharpCompress.Common;
using static MongoDB.Driver.WriteConcern;

namespace event_store_api.Repository
{
    public class GenericEventEntityRepository
    {
        private readonly IMongoCollection<EventEntity> _eventsCollection;

        public GenericEventEntityRepository(
            IOptions<EventStoreDatabaseConfig> eventStoreDatabaseConfig)
        {

            MongoCredential credential = MongoCredential.CreateCredential(
                "admin", 
                eventStoreDatabaseConfig.Value.Username,
                eventStoreDatabaseConfig.Value.Password
            );
            var settings = new MongoClientSettings
            {
                Credential = credential,
                Server = new MongoServerAddress(
                    eventStoreDatabaseConfig.Value.Host, 
                    eventStoreDatabaseConfig.Value.Port
                ),
                SocketTimeout = new TimeSpan(0, 3, 0),
                WaitQueueTimeout = new TimeSpan(0, 3, 0),
                ConnectTimeout = new TimeSpan(0, 3, 0)
            };
            var mongoClient = new MongoClient(settings);

            var mongoDatabase = mongoClient.GetDatabase(
                eventStoreDatabaseConfig.Value.DatabaseName);

            _eventsCollection = mongoDatabase.GetCollection<EventEntity>(
                eventStoreDatabaseConfig.Value.EventsCollectionName);
        }

        public List<EventEntity> FindByEventAttributeValue(string name, string value)
        {
            return _eventsCollection.Find(x => x.EventAttributes.Any(y => y.EventAttributeValue.EventAttributeValue == value && y.EventAttributeName == name)).ToList();
        }

        public List<EventEntity> FindByEventAttributeValues(IList<string> filters)
        {
           if(filters.Count() > 0)
            {
                var res = FindByEventAttributeValue(filters[0].Split(":")[0], filters[0].Split(":")[1]);
                var filtered = new List<EventEntity>();
                foreach (EventEntity entity in res)
                {
                    if (DoesEventMatchAttributeFilter(entity, filters))
                    {
                        filtered.Add(entity);
                    }
                }
                return filtered;
            }
            return _eventsCollection.Find(x => true).ToList();
        }


        private bool DoesEventMatchAttributeFilter(EventEntity doc, IList<string> filters)
        {
            int count = 0;
            foreach (string filter in filters)
            {
                var name = filter.Split(":")[0];
                var value = filter.Split(":")[1];

                foreach (EventAttribute attribute in doc.EventAttributes)
                {
                    if (attribute.EventAttributeName.Equals(name) && attribute.EventAttributeValue.EventAttributeValue.Equals(value))
                    {
                        count++;
                        break;
                    }
                }
            }

            return count == filters.Count();
        }

        public async Task<IList<EventEntity>> GetAsync(string eventStream, string eventName)
        {
            Func<Task<IList<EventEntity>>> getByStreamAndName = async () => {
                return await _eventsCollection
                    .Find(x => x.EventStream == eventStream && x.EventName == eventName)
                    .ToListAsync();
            };

            Func<Task<IList<EventEntity>>> getByStream = async () => {
                return await _eventsCollection
                    .Find(x => x.EventStream == eventStream)
                    .ToListAsync();
            };

            Func<Task<IList<EventEntity>>> getByName = async () => {
                return await _eventsCollection
                    .Find(x => x.EventStream == eventStream && x.EventName == eventName)
                    .ToListAsync();
            };

            Func<Task<IList<EventEntity>>> getAll = async () => {
                return await _eventsCollection
                    .Find(_ => true)
                    .ToListAsync();
            };

            Func<Func<Task<IList<EventEntity>>>> resolve = () =>
            {
                return eventStream != null && eventName != null ? getByStreamAndName
                        : eventName != null ? getByName : eventStream != null ? getByStream : getAll;
            };

            return await resolve.Invoke().Invoke();
        }
          

        public async Task<EventEntity?> GetAsync(string id) =>
            await _eventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(EventEntity newEvent) => 
            await _eventsCollection.InsertOneAsync(newEvent.WithCurrentTime());

        public async Task UpdateAsync(string id, EventEntity updatedEvent) =>
            await _eventsCollection.ReplaceOneAsync(x => x.Id == id, updatedEvent);

        public async Task RemoveAsync(string id) =>
            await _eventsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
