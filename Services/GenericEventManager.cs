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
        args.EventStream = genericEvent.EventStream;
        args.EventName = genericEvent.EventName;
        args.EventAttributes = genericEvent.EventAttributes;
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
        public string EventStream { get; set; } = null!;

        public string EventName { get; set; } = null!;

        public IList<EventAttribute> EventAttributes { get; set; } = new List<EventAttribute>();
}

