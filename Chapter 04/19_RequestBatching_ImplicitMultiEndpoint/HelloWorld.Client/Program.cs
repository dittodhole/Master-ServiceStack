using System;
using HelloWorld.Models;
using ServiceStack;
using ServiceStack.Text;

namespace HelloWorld.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var jsonServiceClient = new JsonServiceClient("http://localhost:5555"))
            {
                var requests = new[]
                               {
                                   new Hello
                                   {
                                       Name = "Person1"
                                   },
                                   new Hello
                                   {
                                       Name = "Person2"
                                   },
                                   new Hello
                                   {
                                       Name = "Person3"
                                   },
                                   new Hello
                                   {
                                       Name = "Person4"
                                   }
                               };

                var responses = jsonServiceClient.SendAll(requests);

                responses.PrintDump();
            }

            Console.ReadLine();
        }
    }
}
