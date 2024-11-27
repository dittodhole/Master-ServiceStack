using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Web;

namespace DoeInc.Ticketing.Web
{
    public class LegacySessionFactory : ISessionFactory
    {
        public ISession GetOrCreateSession(IRequest httpReq,
                                           IResponse httpRes)
        {
            return new LegacySession();
        }

        public ISession GetOrCreateSession()
        {
            var httpReq = HostContext.GetCurrentRequest();
            var httpRes = httpReq.Response;

            return this.GetOrCreateSession(httpReq,
                                           httpRes);
        }
    }
}
