using event_store_api.Models;

namespace event_store_api.Services;

public class GenericEventManager
{

    public GenericEventManager(EventHandler<GenericEventArgs> eventSubmitted) 
    {
        this.eventSubmitted = eventSubmitted;
    }

    public void OnGenericEvent(GenericEventHttpRequestModel genericEvent){
        GenericEventArgs args = new GenericEventArgs();
        args.eventStream = genericEvent.eventStream;
        args.eventName = genericEvent.eventName;
        args.eventProperties = genericEvent.eventProperties;
        OnEventReceived(args);
    }

    protected virtual void OnEventReceived(GenericEventArgs e)
    {
        EventHandler<GenericEventArgs> handler = eventSubmitted;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<GenericEventArgs> eventSubmitted;
}

    public class GenericEventArgs : EventArgs
    {
        public string eventStream { get; set; } = null!;

        public string eventName { get; set; } = null!;

        public IList<EventProperty> eventProperties { get; set; } = new List<EventProperty>();
}

