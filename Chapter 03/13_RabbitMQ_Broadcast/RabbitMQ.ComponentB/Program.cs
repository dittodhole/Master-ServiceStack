using System;
using System.IO;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Models;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;
using ServiceStack.Text;

namespace RabbitMQ.ComponentB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rabbitMqServer = new RabbitMqServer();
            var messageQueueClient = (RabbitMqQueueClient) rabbitMqServer.CreateMessageQueueClient();
            var channel = messageQueueClient.Channel;
            var queueName = messageQueueClient.GetTempQueueName();
            channel.QueueBind(queueName,
                              CustomExchangeNames.FanoutExchangeName,
                              QueueNames<Hello>.In);

            var consumer = new RabbitMqBasicConsumer(channel);
            channel.BasicConsume(queueName,
                                 noAck: false,
                                 consumer: consumer);

            Task.Run(() =>
                     {
                         while (true)
                         {
                             BasicGetResult basicGetResult;
                             try
                             {
                                 basicGetResult = consumer.Queue.Dequeue();
                             }
                             catch (EndOfStreamException endOfStreamException)
                             {
                                 // this is ok
                                 return;
                             }
                             catch (OperationInterruptedException operationInterruptedException)
                             {
                                 // this is ok
                                 return;
                             }
                             catch (Exception ex)
                             {
                                 throw;
                             }
                             var message = basicGetResult.ToMessage<Hello>();
                             var hello = message.GetBody();
                             var name = hello.Name;
                             var result = "Hello {0}".Fmt(name);

                             result.Print();
                         }
                     });

            "listening for hello messages".Print();

            Console.ReadLine();

            channel.QueueDelete(queueName);

            messageQueueClient.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}
