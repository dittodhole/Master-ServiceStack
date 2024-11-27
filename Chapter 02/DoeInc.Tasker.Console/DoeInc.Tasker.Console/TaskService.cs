using System;
using ServiceStack;
using ServiceStack.Text;

namespace DoeInc.Tasker.Console
{
    public sealed class TaskService : Service,
                                      IAnyVoid<HelloRequest>
    {
        public void Any(HelloRequest request)
        {
            const string sessionKey = "foo";

            var previousTimestamp = this.SessionBag.Get<long>(sessionKey);

            "Previous Timestamp: {0}".Print(previousTimestamp);

            this.SessionBag.Set(sessionKey,
                                DateTime.Now.Ticks);

            var sessionId = this.GetSessionId();

            "Session Id: {0}".Print(sessionId);
        }
    }
}
