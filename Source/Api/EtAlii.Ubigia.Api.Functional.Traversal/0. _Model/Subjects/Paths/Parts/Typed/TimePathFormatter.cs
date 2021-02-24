namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TimePathFormatter
    {
        public static readonly TypedPathFormatter YearFormatter = new("yyyy", @"^[0123]?[0123456789]{1,3}$");
        public static readonly TypedPathFormatter MonthFormatter = new("MM", @"^[01]?[0123456789]$");
        public static readonly TypedPathFormatter DayFormatter = new("DD", @"^[0123]?[0123456789]$");
        public static readonly TypedPathFormatter HourFormatter = new("HH", @"^[012]?[0123456789]$");
        public static readonly TypedPathFormatter MinuteFormatter = new("mm", @"^[012345]?[0123456789]$");
        public static readonly TypedPathFormatter SecondFormatter = new("ss", @"^[012345]?[0123456789]$");
        public static readonly TypedPathFormatter MillisecondFormatter = new("MMM", @"^[0123456789]{1,3}$");
    }
}
