using ServiceStack.DataAnnotations;

namespace DoeInc.Ticketing.ServiceModel.Types
{
    public abstract class EntityBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string RowVersion { get; set; }
    }
}
