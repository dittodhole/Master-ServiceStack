using ServiceStack;
using ServiceStack.MiniProfiler;
using ServiceStack.Web;

namespace HelloWorld
{
    public class CustomProfileProvider : BaseProfilerProvider
    {
        private const string CacheKey = ":mini-profiler:";
        public const string RequestKey = ":request:";

        private Profiler Current
        {
            get
            {
                var request = CustomProfileProvider.GetRequest();
                if (request == null)
                {
                    return null;
                }

                return request.Items[CustomProfileProvider.CacheKey] as Profiler;
            }
            set
            {
                var request = CustomProfileProvider.GetRequest();
                if (request == null)
                {
                    return;
                }

                request.Items[CustomProfileProvider.CacheKey] = value;
            }
        }

        public override Profiler Start(ProfileLevel level)
        {
            var request = CustomProfileProvider.GetRequest();
            if (request == null)
            {
                return null;
            }

            var url = request.AbsoluteUri;
            var path = request.PathInfo;

            // don't profile /content or /scripts, either - happens in web.dev
            foreach (var ignored in Profiler.Settings.IgnoredPaths ?? new string[0])
            {
                if (path.ToUpperInvariant()
                        .Contains((ignored ?? "").ToUpperInvariant()))
                {
                    return null;
                }
            }

            var result = new Profiler(url,
                                      level);
            this.Current = result;

            BaseProfilerProvider.SetProfilerActive(result);

            result.User = request.RemoteIp;

            return result;
        }

        private static IRequest GetRequest()
        {
            return (IRequest) RequestContext.Instance.Items[CustomProfileProvider.RequestKey];
        }

        public static void SetRequest(IRequest request)
        {
            RequestContext.Instance.Items[CustomProfileProvider.RequestKey] = request;
        }

        public override void Stop(bool discardResults)
        {
            var request = CustomProfileProvider.GetRequest();
            if (request == null)
            {
                return;
            }

            var response = request.Response;

            var current = this.Current;
            if (current == null)
            {
                return;
            }

            // stop our timings - when this is false, we've already called .Stop before on this session
            if (!BaseProfilerProvider.StopProfiler(current))
            {
                return;
            }

            if (discardResults)
            {
                this.Current = null;
                return;
            }

            // set the profiler name to Controller/Action or /url
            CustomProfileProvider.EnsureName(current,
                                             request);

            // save the profiler
            BaseProfilerProvider.SaveProfiler(current);

            try
            {
                var arrayOfIds = Profiler.Settings.Storage.GetUnviewedIds(current.User)
                                         .ToJson();
                // allow profiling of ajax requests
                response.AddHeader("X-MiniProfiler-Ids",
                                   arrayOfIds);
            }
            catch
            {
            } // headers blew up
        }

        public override Profiler GetCurrentProfiler()
        {
            return this.Current;
        }

        /// <summary>
        ///     Makes sure 'profiler' has a Name, pulling it from route data or url.
        /// </summary>
        private static void EnsureName(Profiler profiler,
                                       IRequest request)
        {
            // also set the profiler name to Controller/Action or /url
            if (!string.IsNullOrWhiteSpace(profiler.Name))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(profiler.Name))
            {
                return;
            }

            profiler.Name = request.PathInfo ?? string.Empty;
            if (profiler.Name.Length > 70)
            {
                profiler.Name = profiler.Name.Remove(70);
            }
        }
    }
}
