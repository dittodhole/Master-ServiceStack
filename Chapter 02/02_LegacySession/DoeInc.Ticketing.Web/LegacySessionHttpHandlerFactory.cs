using System.Web;
using ServiceStack;

namespace DoeInc.Ticketing.Web
{
    public class LegacySessionHttpHandlerFactory : IHttpHandlerFactory
    {
        private static readonly HttpHandlerFactory HttpHandlerFactory = new HttpHandlerFactory();

        public IHttpHandler GetHandler(HttpContext context,
                                       string requestType,
                                       string url,
                                       string pathTranslated)
        {
            var handler = LegacySessionHttpHandlerFactory.HttpHandlerFactory.GetHandler(context,
                                                                                             requestType,
                                                                                             url,
                                                                                             pathTranslated);
            if (handler != null)
            {
                handler = new LegacySessionHttpHandler(handler);
            }

            return handler  ;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            LegacySessionHttpHandlerFactory.HttpHandlerFactory.ReleaseHandler(handler);
        }
    }
}
