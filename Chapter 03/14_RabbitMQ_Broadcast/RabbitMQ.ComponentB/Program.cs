using System;
using System.IO;
using System.Threading.Tasks;
using RabbitMQ.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using ServiceStack;
using ServiceStack.RabbitMq;
using ServiceStack.Messaging;
using ServiceStack.Text;

namespace RabbitMQ.ComponentB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var rabbitMqServer = new RabbitMqServer();
            var messageQueueClient = (RabbitMqQueueClient) rabbitMqServer.CreateMessageQueueClient();
            var channel = messageQueueClient.Channel;
            var queueName = messageQueueClient.GetTempQueueName();
            channel.QueueBind(queue: queueName,
                              exchange: CustomExchangeNames.FanoutExchangeName,
                              routingKey: QueueNames<Hello>.In);

            var consumer = new RabbitMqBasicConsumer(channel);
            channel.BasicConsume(queue: queueName,
                                 noAck: false,
                                 consumer: consumer);

            var messageHandler = new MessageHandler<Hello>(rabbitMqServer,
                                                           message =>
                                                           {
                                                               if (message.RetryAttempts == 0)
                                                               {
                                                                   throw new Exception();
                                                               }
                                                               var hello = message.GetBody();
                                                               var name = hello.Name;

                                                               name.Print();

                                                               return null;
                                                           });

            Task.Run(() =>
                     {
                         while (true)
                         {
                             BasicGetResult basicGetResult;
                             try
                             {
                                 basicGetResult = consumer.Queue.Dequeue();
                             }
                             catch (EndOfStreamException)
                             {
                                 // this is ok
                                 return;
                             }
                             catch (OperationInterruptedException)
                             {
                                 // this is ok
                                 return;
                             }

                             var message = basicGetResult.ToMessage<Hello>();
                             messageHandler.ProcessMessage(messageQueueClient,
                                                           message);
                         }
                     });

            "listening for hello messages".Print();

            Console.ReadLine();

            messageQueueClient.Dispose();
            rabbitMqServer.Dispose();
        }
    }
}
