using System;
using System.Linq;
using ServiceStack;
using ServiceStack.MiniProfiler;

namespace HelloWorld
{
    public partial class CustomProfilerProvider : BaseProfilerProvider
    {
        public override Profiler Start(ProfileLevel level)
        {
            var request = CustomProfilerProvider.GetRequest();
            if (request == null)
            {
                return null;
            }

            var path = request.PathInfo;
            var ignoredPaths = Profiler.Settings.IgnoredPaths ?? new string[0];
            if (ignoredPaths.Any(arg => path.IndexOf(arg ?? string.Empty,
                                                     StringComparison.InvariantCultureIgnoreCase) >= 0))
            {
                return null;
            }

            var result = CustomProfilerProvider.Current = new Profiler(request.AbsoluteUri,
                                                                      level)
                                                         {
                                                             User = request.RemoteIp,
                                                             Name = path
                                                         };

            BaseProfilerProvider.SetProfilerActive(result);

            return result;
        }

        public override void Stop(bool discardResults)
        {
            var profiler = CustomProfilerProvider.Current;
            if (profiler == null)
            {
                return;
            }

            if (!BaseProfilerProvider.StopProfiler(profiler))
            {
                return;
            }

            if (discardResults)
            {
                CustomProfilerProvider.Current = null;
                return;
            }

            BaseProfilerProvider.SaveProfiler(profiler);

            try
            {
                var arrayOfIds = Profiler.Settings.Storage.GetUnviewedIds(profiler.User)
                                         .ToJson();
                var request = CustomProfilerProvider.GetRequest();
                var response = request.Response;
                response.AddHeader("X-MiniProfiler-Ids",
                                   arrayOfIds);
            }
            catch
            {
            }
        }

        public override Profiler GetCurrentProfiler()
        {
            return CustomProfilerProvider.Current;
        }
    }
}
