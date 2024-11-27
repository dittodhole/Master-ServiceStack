using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class StoreComment : IReturn<Comment>
    {
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string TicketId { get; set; }
        public string Text { get; set; }
    }
}
