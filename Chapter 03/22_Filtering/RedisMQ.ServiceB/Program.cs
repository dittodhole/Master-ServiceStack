using System;
using ServiceStack;

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
            appHost.Start("http://*:1337/");

            Console.ReadLine();

            appHost.Dispose();
        }
    }
}
