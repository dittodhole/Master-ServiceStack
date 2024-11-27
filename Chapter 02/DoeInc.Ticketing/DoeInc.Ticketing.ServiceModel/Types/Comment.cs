namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class Comment : EntityBase
    {
        public string Text { get; set; }
        public User Creator { get; set; }
    }
}
