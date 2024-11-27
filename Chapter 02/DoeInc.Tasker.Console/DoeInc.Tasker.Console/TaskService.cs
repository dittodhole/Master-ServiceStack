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
            var session = this.GetSession();

            const string sessionKey = "previousTimestamp";

            var previousTimestamp = this.SessionBag.Get<string>(sessionKey);

            "Previous Timestamp: {0}".Print(previousTimestamp);

            this.SessionBag.Set(sessionKey,
                                DateTime.Now.Ticks.ToString());

            session.Id.Print();
        }
    }
}
