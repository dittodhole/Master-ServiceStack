using System;
using HelloWorld.Models;
using ServiceStack.ProtoBuf;
using ServiceStack.Text;

namespace HelloWorld.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var protoBufServiceClient = new ProtoBufServiceClient("http://localhost:5555"))
            {
                var hello = new Hello
                            {
                                Name = "John Doe"
                            };
                var response = protoBufServiceClient.Post(hello);

                response.PrintDump();
            }

            Console.ReadLine();
        }
    }
}
