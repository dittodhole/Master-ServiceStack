using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using ServiceStack;
using ServiceStack.Messaging;

namespace RCON.Client
{
    public class Client : ServiceStack.Messaging.Rcon.Client
    {
        private readonly Dictionary<uint, AsyncCallback> _callbacks;

        public Client(IPEndPoint rconEndpoint)
            : base(rconEndpoint)
        {
            var fieldInfo = typeof (ServiceStack.Messaging.Rcon.Client).GetField("_registeredCallbacks",
                                                                                 BindingFlags.Instance | BindingFlags.NonPublic);
            this._callbacks = (Dictionary<uint, AsyncCallback>) fieldInfo.GetValue(this);
        }

        public void Requeue<T>(IMessage<T> message,
                               AsyncCallback callback)
        {
            this._callbacks[this.SequenceID] = callback;
            this.InternalSend(new[]
                              {
                                  Encoding.UTF8.GetBytes(typeof (T).AssemblyQualifiedName),
                                  message.ToBytes()
                              });
        }
    }
}
