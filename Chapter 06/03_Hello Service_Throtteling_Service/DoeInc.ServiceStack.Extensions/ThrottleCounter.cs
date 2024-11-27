using System;

namespace DoeInc.ServiceStack.Extensions
{
    public sealed class ThrottleCounter
    {
        public string RemoteIp { get; set; }
        public string Operation { get; set; }
        public string DurationAbbreviation { get; set; }
        public int Counter { get; set; }
        public string ExpiresIn { get; set; }
    }
}