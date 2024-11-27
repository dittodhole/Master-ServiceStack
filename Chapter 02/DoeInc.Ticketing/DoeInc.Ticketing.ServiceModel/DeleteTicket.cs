using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class DeleteTicket : IReturnVoid
    {
        public string Id { get; set; }
        public string RowVersion { get; set; }
    }
}
