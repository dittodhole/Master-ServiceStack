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
            "program on thread {0}".Print(Thread.CurrentThread.ManagedThreadId);

            var inMemoryTransientMessageFactory = new InMemoryTransientMessageFactory();

            var messageService = inMemoryTransientMessageFactory.CreateMessageService();

            messageService.RegisterHandler<Hello>(m =>
                                                  {
                                                      var hello = m.GetBody();
                                                      var name = hello.Name;
                                                      var helloResponse = new HelloResponse
                                                                          {
                                                                              Result = "Hello {0}".Fmt(name)
                                                                          };

                                                      "conumser on thread {0}".Print(Thread.CurrentThread.ManagedThreadId);

                                                      return helloResponse;
                                                  });
            messageService.RegisterHandler<HelloResponse>(m =>
                                                          {
                                                              var helloResponse = m.GetBody();
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
                             "producer on thread {0}".Print(Thread.CurrentThread.ManagedThreadId);

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
