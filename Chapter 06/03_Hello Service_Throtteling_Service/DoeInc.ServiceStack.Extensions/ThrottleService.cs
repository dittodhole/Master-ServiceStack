using System;
using System.Linq;
using ServiceStack;

namespace DoeInc.ServiceStack.Extensions
{
    public sealed class ThrottleService : Service,
                                          IGet<ThrottleCountersRequest>
    {
        public object Get(ThrottleCountersRequest request)
        {
            var throttlePlugin = HostContext.GetPlugin<ThrottlePlugin>();
            var cacheKeys = throttlePlugin.CacheKeys;

            var counters = cacheKeys.Select(cacheKey => new
            {
                CacheKey = cacheKey,
                Counter = this.Cache.Get<int>(cacheKey),
                ExpiresIn = this.Cache.GetTimeToLive(cacheKey) ?? TimeSpan.Zero
            });

            var throttleCountersResponse = new ThrottleCountersResponse
            {
                ThrottleCounters = counters.Select(counter =>
                {
                    var throttleCounter = throttlePlugin.CreateThrottleCounter(counter.CacheKey);
                    throttleCounter.Counter = counter.Counter;
                    throttleCounter.ExpiresIn = counter.ExpiresIn;
                    return throttleCounter;
                }).ToArray()
            };

            return throttleCountersResponse;
        }
    }
}