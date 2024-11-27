using System;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Text;

namespace InMemoryMQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var inMemoryTransientMessageFactory = new InMemoryTransientMessageFactory();

            var messageService = inMemoryTransientMessageFactory.CreateMessageService();

            messageService.RegisterHandler<Hello>(message =>
                                                  {
                                                      var hello = message.GetBody();
                                                      "Name: {0}, retried for {1} times".Print(hello.Name,
                                                                                               message.RetryAttempts);

                                                      if (message.RetryAttempts == 0)
                                                      {
                                                          throw new Exception();
                                                      }

                                                      var name = hello.Name;
                                                      var helloResponse = new HelloResponse
                                                                          {
                                                                              Result = "Hello {0}".Fmt(name)
                                                                          };

                                                      return helloResponse;
                                                  });
            messageService.RegisterHandler<HelloResponse>(message =>
                                                          {
                                                              var helloResponse = message.GetBody();
                                                              helloResponse.Result.Print();

                                                              return null;
                                                          });
            messageService.Start();

            using (var messageProducer = inMemoryTransientMessageFactory.CreateMessageProducer())
            {
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
            }

            Console.ReadLine();

            messageService.Stop();
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
