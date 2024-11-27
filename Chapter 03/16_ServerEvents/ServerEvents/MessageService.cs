using ServiceStack;

namespace ServerEvents
{
    public class MessageService : Service,
                                  IAnyVoid<Say>
    {
        public IServerEvents ServerEvents { get; set; }

        public void Any(Say request)
        {
            this.ServerEvents.NotifyAll(request);
        }
    }
}
