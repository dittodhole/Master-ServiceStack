using ServiceStack.Text;

namespace DoeInc.Tasker.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string url = "http://*:1337/";

            var appHost = new AppHost().Init()
                                       .Start(url);

            string.Format("Listening at {0}",
                          url)
                  .Print();

            System.Console.ReadLine();
        }
    }
}
