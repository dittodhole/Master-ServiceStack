using System.Web;
using System.Web.SessionState;

namespace DoeInc.Ticketing.Web
{
    public class LegacySessionHttpHandler : IHttpHandler,
                                            IRequiresSessionState
    {
        private readonly IHttpHandler _handler;

        public LegacySessionHttpHandler(IHttpHandler handler)
        {
            this._handler = handler;
        }

        public IHttpHandler Handler
        {
            get
            {
                return this._handler;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            this.Handler.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get
            {
                return this.Handler.IsReusable;
            }
        }
    }
}
