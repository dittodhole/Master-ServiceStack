using System;
using System.IO;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Models;
using RabbitMQ.Util;
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
            var sharedQueue = new SharedQueue<BasicGetResult>();
            var rabbitMqBasicConsumer = new RabbitMqBasicConsumer(channel, sharedQueue);
            channel.BasicConsume(queueName,
                                    false,
                                    rabbitMqBasicConsumer);

            Task.Run(() =>
                        {
                            while (true)
                            {
                                try
                                {
                                    var basicMsg = sharedQueue.Dequeue();
                                    var message = basicMsg.ToMessage<Hello>();
                                    var hello = message.GetBody();
                                    var name = hello.Name;
                                    var result = "Hello {0}".Fmt(name);

                                    result.Print();
                                }
                                catch (EndOfStreamException endOfStreamException)
                                {
                                    // this is ok
                                }
                                catch (OperationInterruptedException operationInterruptedException)
                                {
                                    // this is ok
                                }
                                catch (Exception ex)
                                {
                                    throw;
                                }
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
