using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Host;
using ServiceStack.RabbitMq;
using ServiceStack.Web;

namespace HelloWorld
{
    public class RabitMqRequestLogger : InMemoryRollingRequestLogger
    {
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
                                      "HelloWorld");

            using (var rabbitMqServer = new RabbitMqServer())
            {
                using (var messageProducer = rabbitMqServer.CreateMessageProducer())
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
