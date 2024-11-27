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
        private const string StopwatchItemsKey = "_requestDurationStopwatch";

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
                                            HostContext.Config.HandlerFactoryPath);
            if (this.Request.FilePath.StartsWith(apiBasePath))
            {
                return;
            }

            var requestLogger = HostContext.TryResolve<IRequestLogger>();
            if (requestLogger != null)
            {
                this.Context.Items[Global.StopwatchItemsKey] = Stopwatch.StartNew();
            }
        }

        protected void Application_EndRequest(object src,
                                              EventArgs e)
        {
            var request = HostContext.TryGetCurrentRequest();
            if (request == null)
            {
                return;
            }

            var stopwatch = (Stopwatch) request.GetItem(Global.StopwatchItemsKey);
            if (stopwatch == null)
            {
                return;
            }

            stopwatch.Stop();

            var requestLogger = request.TryResolve<IRequestLogger>();
            if (requestLogger == null)
            {
                return;
            }

            requestLogger.Log(request: request,
                              requestDto: Global.SerializableQueryString(request.QueryString),
                              response: null,
                              elapsed: stopwatch.Elapsed);
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
