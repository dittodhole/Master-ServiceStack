namespace DoeInc.Ticketing.ServiceModel
{
    public interface IHasCredentials
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
