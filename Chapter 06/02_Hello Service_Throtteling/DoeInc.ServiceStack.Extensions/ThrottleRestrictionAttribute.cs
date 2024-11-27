using System;
using ServiceStack;

namespace DoeInc.ServiceStack.Extensions
{
    public sealed class ThrottleRestrictionAttribute : Attribute
    {
        public const string MinuteAbbreviation = "m";
        public const string HourAbbreviation = "h";
        public const string DayAbbreviation = "d";

        public int PerMinute { get; set; }
        public int PerHour { get; set; }
        public int PerDay { get; set; }

        public int GetMaximum(string durationAbbreviation)
        {
            switch (durationAbbreviation)
            {
                case MinuteAbbreviation:
                    return this.PerMinute;
                case HourAbbreviation:
                    return this.PerHour;
                case DayAbbreviation:
                    return this.PerDay;
                default:
                    var message = "Abbreviation {0} is unknown".Fmt(durationAbbreviation);
                    throw new ArgumentOutOfRangeException(durationAbbreviation,
                                                          message);
            }
        }
    }
}