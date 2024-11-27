using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class StoreTicket : IReturn<Ticket>
    {
        public int Id { get; set; }
        public ulong RowVersion { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
    }
}
