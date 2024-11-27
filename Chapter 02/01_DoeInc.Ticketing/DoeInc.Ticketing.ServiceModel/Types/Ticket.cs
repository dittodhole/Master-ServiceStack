namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; }
        public Status Status { get; set; }
    }
}
