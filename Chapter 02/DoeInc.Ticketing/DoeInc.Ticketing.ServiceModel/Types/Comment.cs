namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Comment : EntityBase
    {
        public int TicketId { get; set; }
        public string Text { get; set; }
        public string Creator { get; set; }
    }
}
