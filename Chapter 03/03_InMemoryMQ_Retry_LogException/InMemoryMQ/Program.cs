using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Text;

namespace InMemoryMQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var inMemoryTransientMessageFactory = new InMemoryTransientMessageFactory();

            var messageService = inMemoryTransientMessageFactory.CreateMessageService();

            messageService.RegisterHandler<Hello>(message =>
                                                  {
                                                      if (message.RetryAttempts == 0)
                                                      {
                                                          throw new Exception();
                                                      }

                                                      var hello = message.GetBody();
                                                      var name = hello.Name;
                                                      var helloResponse = new HelloResponse
                                                                          {
                                                                              Result = "Hello {0}, I've retried it for {1} times".Fmt(name,
                                                                                                                                      message.RetryAttempts)
                                                                          };

                                                      return helloResponse;
                                                  },
                                                  (messageHandler,
                                                   message,
                                                   exception) =>
                                                  {
                                                      exception.PrintDump();
                                                  });
            messageService.RegisterHandler<HelloResponse>(message =>
                                                          {
                                                              var helloResponse = message.GetBody();
                                                              helloResponse.Result.Print();

                                                              return null;
                                                          });
            messageService.Start();

            Task.Run(() =>
                     {
                         var messageProducer = inMemoryTransientMessageFactory.CreateMessageProducer();
                         for (var i = 0;
                              i < 10;
                              i++)
                         {
                             var hello = new Hello
                                         {
                                             Name = i.ToString()
                                         };
                             messageProducer.Publish(hello);
                         }
                     });

            Console.ReadLine();
        }

        public class Hello : IReturn<HelloResponse>
        {
            public string Name { get; set; }
        }

        public class HelloResponse
        {
            public string Result { get; set; }
        }
    }
}
