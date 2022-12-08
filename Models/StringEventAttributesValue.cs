namespace event_store_api.Models
{
    public class StringEventAttributesValue : DefaultEventAttributeValue
    {
        public StringEventAttributesValue(string value) : base("string", value) { }
    }
}
