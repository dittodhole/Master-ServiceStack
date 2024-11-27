using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Ticket : IHasIntId
    {
        [Required]
        public string Title { get; set; }

        public Status Status { get; set; }
        public ulong RowVersion { get; set; }
        public string ProcessorUserAuthId { get; set; }
        public string CreatorUserAuthId { get; set; }

        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
    }
}
