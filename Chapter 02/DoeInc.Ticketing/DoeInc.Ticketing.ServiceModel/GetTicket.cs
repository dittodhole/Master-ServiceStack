using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class GetTicket : IReturn<Ticket>
    {
        public int Id { get; set; }
    }
}
