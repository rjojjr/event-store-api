namespace event_store_api.Models
{
    public class StringEventPropertyValue : DefaultEventPropertyValue
    {
        public StringEventPropertyValue(string value) : base("string", value) { }
    }
}
