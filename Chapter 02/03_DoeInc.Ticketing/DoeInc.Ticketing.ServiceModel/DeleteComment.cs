using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class DeleteComment : IReturnVoid
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
    }
}
