using System.Web;
using System.Web.SessionState;
using ServiceStack.Caching;

namespace DoeInc.Ticketing.Web
{
    public class LegacySession : ISession
    {
        public HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public void Set<T>(string key,
                           T value)
        {
            this[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T) this[key];
        }

        public bool Remove(string key)
        {
            this.Session.Remove(key);
            return true;
        }

        public void RemoveAll()
        {
            this.Session.RemoveAll();
        }

        public object this[string key]
        {
            get
            {
                return this.Session[key];
            }
            set
            {
                this.Session[key] = value;
            }
        }
    }
}
