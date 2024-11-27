using ServiceStack.DataAnnotations;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public abstract class EntityBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        public ulong RowVersion { get; set; }
    }
}
