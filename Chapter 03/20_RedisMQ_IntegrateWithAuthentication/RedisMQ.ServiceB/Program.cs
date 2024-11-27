using System;
using RedisMQ.Models;
using ServiceStack.Messaging;
using ServiceStack.Text;

namespace RedisMQ.ServiceB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(Constants.UrlBase);

            "listening to RedisMQ".Print();

            Console.ReadLine();

            var messageService = appHost.Resolve<IMessageService>();
            messageService.Dispose();

            appHost.Dispose();
        }
    }
}
