using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class GetComments : IReturn<Comment[]>
    {
        public string TicketId { get; set; }
    }
}
