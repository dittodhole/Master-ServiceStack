using ServiceStack;

namespace DoeInc.Ticketing.ServiceModel
{
    public class DeleteTicket : IReturnVoid
    {
        public int Id { get; set; }
    }
}
