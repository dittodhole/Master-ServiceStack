using DoeInc.Ticketing.ServiceModel.Types;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class StoreComment : IReturn<Comment>
    {
        public int Id { get; set; }
        public ulong RowVersion { get; set; }
        public string TicketId { get; set; }
        public string Text { get; set; }
    }
}
