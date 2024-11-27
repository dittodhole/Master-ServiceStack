using ServiceStack;

namespace DoeInc.ServiceStack.Extensions
{
    [Route("/throttlecounters")]
    public sealed class ThrottleCountersRequest : IReturn<ThrottleCountersResponse>
    {
    }
}