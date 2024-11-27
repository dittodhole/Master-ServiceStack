using ServiceStack;

namespace DoeInc.ServiceStack.Extensions
{
    public static class ServiceExtensions
    {
        public static T Create<T>(this Service service) where T : IHasRequestIdentifier, new()
        {
            var requestIdentifier = service.Request.GetRequestIdentifier();
            var instance = new T
                           {
                               RequestIdentifier = requestIdentifier
                           };

            return instance;
        }
    }
}
