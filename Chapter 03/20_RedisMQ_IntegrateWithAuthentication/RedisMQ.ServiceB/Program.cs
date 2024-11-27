using System;
using RedisMQ.Models;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(Constants.UrlBase);

            "listening to RedisMQ".Print();

            Console.ReadLine();

            appHost.Dispose();
        }
    }
}
