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
            var counters = this.Cache.GetKeysStartingWith(ThrottlePlugin.CacheKeyPrefix)
                               .Select(cacheKey => new
                                                   {
                                                       CacheKey = cacheKey,
                                                       Counter = this.Cache.Get<int>(cacheKey),
                                                       ExpiresIn = this.Cache.GetTimeToLive(cacheKey) ?? TimeSpan.Zero
                                                   });

            var throttleCountersResponse = new ThrottleCountersResponse
                                           {
                                               ThrottleCounters = counters.Select(counter =>
                                                                                  {
                                                                                      var throttleCounter = ThrottlePlugin.CreateThrottleCounter(counter.CacheKey);
                                                                                      throttleCounter.Counter = counter.Counter;
                                                                                      throttleCounter.ExpiresIn = counter.ExpiresIn.ToString();
                                                                                      return throttleCounter;
                                                                                  })
                                                                          .ToArray()
                                           };

            return throttleCountersResponse;
        }
    }
}
