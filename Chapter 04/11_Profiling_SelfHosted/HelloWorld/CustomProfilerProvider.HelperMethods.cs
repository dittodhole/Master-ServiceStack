using ServiceStack;
using ServiceStack.MiniProfiler;
using ServiceStack.Web;

namespace HelloWorld
{
    public partial class CustomProfilerProvider
    {
        private const string ProfilerItemsKey = ":mini-profiler:";
        private const string RequestItemsKey = ":request:";

        private static Profiler Current
        {
            get
            {
                return (Profiler) RequestContext.Instance.Items[CustomProfilerProvider.ProfilerItemsKey];
            }
            set
            {
                RequestContext.Instance.Items[CustomProfilerProvider.ProfilerItemsKey] = value;
            }
        }

        private static IRequest GetRequest()
        {
            return (IRequest) RequestContext.Instance.Items[CustomProfilerProvider.RequestItemsKey];
        }

        public static void SetRequest(IRequest request)
        {
            RequestContext.Instance.Items[CustomProfilerProvider.RequestItemsKey] = request;
        }
    }
}
