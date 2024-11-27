using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServiceStack;
using ServiceStack.Web;

namespace DoeInc.ServiceStack.Extensions
{
    public sealed class ThrottlePlugin : IPlugin
    {
        public const string CacheKeyPrefix = "__throttleCounter";

        private readonly Dictionary<Type, ThrottleRestrictionAttribute> _throttleRestrictionAttributes = new Dictionary<Type, ThrottleRestrictionAttribute>();

        public void Register(IAppHost appHost)
        {
            this.RegisterThrottleRestrictionAttributes(appHost);

            appHost.GlobalRequestFilters.Add(RejectRequestIfThrottelingApplies);
            appHost.RegisterService<ThrottleService>();
        }

        private void RegisterThrottleRestrictionAttributes(IAppHost appHost)
        {
            foreach (var operation in appHost.Metadata.Operations)
            {
                var requestType = operation.RequestType;
                var throttleRestrictionAttribute = requestType.GetCustomAttribute<ThrottleRestrictionAttribute>();
                if (throttleRestrictionAttribute != null)
                {
                    this._throttleRestrictionAttributes[requestType] = throttleRestrictionAttribute;
                }
            }
        }

        private void RejectRequestIfThrottelingApplies(IRequest request,
                                                       IResponse response,
                                                       object dto)
        {
            if (dto == null)
            {
                return;
            }

            var type = dto.GetType();

            ThrottleRestrictionAttribute throttleRestrictionAttribute;
            if (!this._throttleRestrictionAttributes.TryGetValue(type,
                                                                 out throttleRestrictionAttribute))
            {
                return;
            }

            var durationScopes = new[]
            {
                new
                {
                    DurationAbbreviation = ThrottleRestrictionAttribute.MinuteAbbreviation,
                    Duration = TimeSpan.FromMinutes(1d)
                },
                new
                {
                    DurationAbbreviation = ThrottleRestrictionAttribute.HourAbbreviation,
                    Duration = TimeSpan.FromHours(1d)
                },
                new
                {
                    DurationAbbreviation = ThrottleRestrictionAttribute.DayAbbreviation,
                    Duration = TimeSpan.FromDays(1d)
                }
            };

            var cacheClient = request.GetCacheClient();

            var shouldThrottle = false;
            foreach (var durationScope in durationScopes)
            {
                var maximum = throttleRestrictionAttribute.GetMaximum(durationScope.DurationAbbreviation);
                if (maximum <= 0)
                {
                    continue;
                }

                var key = this.GetCacheKey(request,
                                           durationScope.DurationAbbreviation);

                var counter = cacheClient.Get<int>(key);

                TimeSpan expiresIn;
                if (counter > 0)
                {
                    expiresIn = cacheClient.GetTimeToLive(key) ?? durationScope.Duration;
                }
                else
                {
                    expiresIn = durationScope.Duration;
                }

                cacheClient.Set(key,
                                ++counter,
                                expiresIn);

                shouldThrottle |= counter > maximum;
            }

            if (shouldThrottle)
            {
                response.StatusCode = 429; // too many requests
                response.StatusDescription = "Too many requests.";
                response.End();
            }
        }

        private string GetCacheKey(IRequest request,
                                   string durationAbbreviation)
        {
            var key = "{0}|{1}|{2}|{3}".Fmt(CacheKeyPrefix,
                                            request.RemoteIp,
                                            request.OperationName,
                                            durationAbbreviation);
            return key;
        }

        internal static ThrottleCounter CreateThrottleCounter(string cacheKey)
        {
            var cacheKeyParts = cacheKey.Split('|');
            var prefix = cacheKeyParts.ElementAt(0);
            var remoteIp = cacheKeyParts.ElementAt(1);
            var operation = cacheKeyParts.ElementAt(2);
            var durationAbbreviation = cacheKeyParts.ElementAt(3);

            var throttleCounter = new ThrottleCounter
            {
                DurationAbbreviation = durationAbbreviation,
                Operation = operation,
                RemoteIp = remoteIp
            };

            return throttleCounter;
        }
    }
}
