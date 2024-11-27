using RabbitMQ.Client;
using ServiceStack.Messaging;

namespace RabbitMQ.Models
{
    public static class CustomExchangeNames
    {
        public static readonly string FanoutExchangeName = string.Concat(QueueNames.Exchange,
                                                                         ".",
                                                                         ExchangeType.Fanout);
    }
}
