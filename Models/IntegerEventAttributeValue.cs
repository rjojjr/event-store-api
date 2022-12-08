namespace event_store_api.Models
{
    public class IntegerEventAttributeValue : DefaultEventAttributeValue
    {

        public IntegerEventAttributeValue(int rawValue) : base("int32", rawValue.ToString())
        {
            
        }

        public int ExtractRawValue()
        {
            return Int32.Parse(EventAttributeValue);
        }
    }
}
