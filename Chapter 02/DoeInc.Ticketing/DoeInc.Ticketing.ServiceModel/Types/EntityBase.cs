namespace DoeInc.Ticketing.ServiceModel.Types
{
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public string RowVersion { get; set; }
    }
}
