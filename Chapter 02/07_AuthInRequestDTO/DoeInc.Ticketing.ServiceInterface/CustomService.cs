using DoeInc.Ticketing.ServiceModel;
using ServiceStack;

namespace DoeInc.Ticketing.ServiceInterface
{
    public abstract class CustomService : Service
    {
        private CustomAuthUserSession _customAuthUserSession;

        protected CustomAuthUserSession UserSession
        {
            get
            {
                return this._customAuthUserSession ?? (this._customAuthUserSession = this.SessionAs<CustomAuthUserSession>());
            }
        }
    }
}
