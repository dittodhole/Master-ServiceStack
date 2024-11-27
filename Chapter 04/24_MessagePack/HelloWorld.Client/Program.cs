using System;
using HelloWorld.Models;
using ServiceStack.MsgPack;
using ServiceStack.Text;

namespace HelloWorld.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var msgPackServiceClient = new MsgPackServiceClient("http://localhost:5555"))
            {
                var hello = new Hello
                            {
                                Name = "John Doe"
                            };
                var response = msgPackServiceClient.Post(hello);

                response.PrintDump();
            }

            Console.ReadLine();
        }
    }
}
