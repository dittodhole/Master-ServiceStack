using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class DeleteComment : IReturn<Comment>
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
    }
}
