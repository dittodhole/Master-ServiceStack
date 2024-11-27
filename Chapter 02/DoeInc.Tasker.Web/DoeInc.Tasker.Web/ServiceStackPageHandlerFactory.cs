using System.Web;
using System.Web.UI;
using ServiceStack;

namespace DoeInc.Tasker.Web
{
    public class ServiceStackPageHandlerFactory : PageHandlerFactory
    {
        public override IHttpHandler GetHandler(HttpContext context,
                                                string requestType,
                                                string virtualPath,
                                                string path)
        {
            var httpHandler = base.GetHandler(context,
                                              requestType,
                                              virtualPath,
                                              path);

            ServiceStackHost.Instance.GetContainer()
                            .AutoWire(httpHandler);

            return httpHandler;
        }
    }
}
