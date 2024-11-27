using System;
using ServiceStack;
using ServiceStack.Text;

namespace HelloWorld
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var licensePath = @"~/../../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            var url = "http://*:5555/";
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(url);

            "Press ENTER to exit".Print();

            Console.ReadLine();
        }
    }
}
