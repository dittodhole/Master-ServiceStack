using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class DeleteComment : IReturnVoid
    {
        public string Id { get; set; }
        public string TicketId { get; set; }
    }
}
