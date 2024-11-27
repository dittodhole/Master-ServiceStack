using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.Messaging;
using ServiceStack.Web;

namespace HelloWorld
{
    public class MessageServiceRequestLogger : InMemoryRollingRequestLogger
    {
        private readonly string _component;

        public MessageServiceRequestLogger(string component)
        {
            this._component = component;
        }

        public override void Log(IRequest request,
                                 object requestDto,
                                 object response,
                                 TimeSpan requestDuration)
        {
            Type requestType;
            if (requestDto == null)
            {
                requestType = null;
            }
            else
            {
                requestType = requestDto.GetType();
            }

            if (this.ExcludeRequestType(requestType))
            {
                return;
            }

            var requestLogEntry = this.CreateEntry(request,
                                                   requestDto,
                                                   response,
                                                   requestDuration,
                                                   requestType);

            requestLogEntry.Items.Add("Component",
                                      this._component);

            using (var messageService = request.TryResolve<IMessageService>())
            {
                if (messageService == null)
                {
                    return;
                }

                using (var messageProducer = messageService.CreateMessageProducer())
                {
                    messageProducer.Publish(requestLogEntry);
                }
            }
        }

        public override List<RequestLogEntry> GetLatestLogs(int? take)
        {
            throw new NotSupportedException();
        }
    }
}
