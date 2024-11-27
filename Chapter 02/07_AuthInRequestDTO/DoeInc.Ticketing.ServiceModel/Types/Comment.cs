using ServiceStack.DataAnnotations;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Comment
    {
        public int TicketId { get; set; }
        public string Text { get; set; }

        [AutoIncrement]
        public int Id { get; set; }

        public ulong RowVersion { get; set; }
        public string CreatorUserAuthId { get; set; }
    }
}
