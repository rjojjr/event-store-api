namespace event_store_api.Models
{
    public interface IEventAttributeValue
    {
        string ValueType { get; set; } 
        string EventAttributeValue { get; set; }
    }
}
