using System;
using System.Web;
using ServiceStack;

namespace DoeInc.Ticketing.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender,
                                         EventArgs e)
        {
            var licensePath = @"~/../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);
            new AppHost().Init();
        }
    }
}
