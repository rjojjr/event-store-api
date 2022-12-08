namespace event_store_api.Models
{
    public interface IEventPropertyValue
    {
        string ValueType { get; set; } 
        string EventPropertyValue { get; set; }
    }
}
