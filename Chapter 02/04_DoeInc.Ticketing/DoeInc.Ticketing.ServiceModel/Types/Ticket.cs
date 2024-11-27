using ServiceStack.DataAnnotations;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Ticket
    {
        public string Title { get; set; }
        public Status Status { get; set; }

        [AutoIncrement]
        public int Id { get; set; }

        public ulong RowVersion { get; set; }
    }
}
