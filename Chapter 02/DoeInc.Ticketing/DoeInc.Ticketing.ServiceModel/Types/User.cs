namespace DoeInc.Ticketing.ServiceModel.Types
{
    public class User : EntityBase
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAdddress { get; set; }
    }
}
