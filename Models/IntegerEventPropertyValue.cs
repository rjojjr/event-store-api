namespace event_store_api.Models
{
    public class IntegerEventPropertyValue : DefaultEventPropertyValue
    {

        public IntegerEventPropertyValue(int rawValue) : base("int32", rawValue.ToString())
        {
            
        }

        public int ExtractRawValue()
        {
            return Int32.Parse(EventPropertyValue);
        }
    }
}
