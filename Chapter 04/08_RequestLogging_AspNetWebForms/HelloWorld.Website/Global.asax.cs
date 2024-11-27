using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using HelloWorld.Website.api;
using ServiceStack;
using ServiceStack.Web;

namespace HelloWorld.Website
{
    public class Global : HttpApplication
    {
        private const string StopWatchItemsKey = "_requestDurationStopwatch";

        protected void Application_Start(object sender,
                                         EventArgs e)
        {
            var licensePath = @"~/../../../license.txt".MapHostAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licensePath);

            new AppHost().Init();
        }

        protected void Application_BeginRequest(object sender,
                                                EventArgs e)
        {
            var apiBasePath = string.Concat(this.Request.ApplicationPath,
                                            ServiceStackHost.Instance.Config.HandlerFactoryPath);
            if (this.Request.FilePath.StartsWith(apiBasePath))
            {
                return;
            }

            var requestLogger = ServiceStackHost.Instance.TryResolve<IRequestLogger>();
            if (requestLogger != null)
            {
                this.Context.Items[Global.StopWatchItemsKey] = Stopwatch.StartNew();
            }
        }

        protected void Application_EndRequest(object src,
                                              EventArgs e)
        {
            var stopwatch = (Stopwatch) this.Context.Items[Global.StopWatchItemsKey];
            if (stopwatch != null)
            {
                stopwatch.Stop();

                var requestLogger = ServiceStackHost.Instance.TryResolve<IRequestLogger>();
                var request = HostContext.GetCurrentRequest();
                requestLogger.Log(request,
                                  Global.SerializableQueryString(request.QueryString),
                                  null,
                                  stopwatch.Elapsed);
            }
        }

        private static Dictionary<string, string> SerializableQueryString(INameValueCollection queryString)
        {
            var to = new Dictionary<string, string>(queryString.Count);
            foreach (var key in queryString.AllKeys)
            {
                var value = queryString.Get(key);
                to[key] = value;
            }

            return to;
        }
    }
}
