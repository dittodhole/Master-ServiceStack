using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class StoreTicket : IReturn<Ticket>
    {
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string Title { get; set; }
        public string ProcessorId { get; set; }
        public Status Status { get; set; }
    }
}
