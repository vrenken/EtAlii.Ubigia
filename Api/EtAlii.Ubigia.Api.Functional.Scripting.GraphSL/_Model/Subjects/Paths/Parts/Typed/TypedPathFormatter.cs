namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;

    public class TypedPathFormatter
    {
        public static class Time
        {
            public static readonly TypedPathFormatter YearFormatter = new TypedPathFormatter("YYYY", @"^[0123]?[0123456789]{1,3}$");
            public static readonly TypedPathFormatter MonthFormatter = new TypedPathFormatter("MM", @"^[01]?[0123456789]$");
            public static readonly TypedPathFormatter DayFormatter = new TypedPathFormatter("DD", @"^[0123]?[0123456789]$");
            public static readonly TypedPathFormatter HourFormatter = new TypedPathFormatter("HH", @"^[012]?[0123456789]$");
            public static readonly TypedPathFormatter MinuteFormatter = new TypedPathFormatter("mm", @"^[012345]?[0123456789]$");
            public static readonly TypedPathFormatter SecondFormatter = new TypedPathFormatter("ss", @"^[012345]?[0123456789]$");
            public static readonly TypedPathFormatter MillisecondFormatter = new TypedPathFormatter("MMM", @"^[0123456789]{1,3}$");
        }

        public static class Text
        {
            public static readonly TypedPathFormatter WordFormatter = new TypedPathFormatter("WORD", @"^\p{L}+$");
            public static readonly TypedPathFormatter NumberFormatter = new TypedPathFormatter("NUMBER", @"^[0123456789]+$");
        }

        public static class Name
        {
            public static readonly TypedPathFormatter FirstNameFormatter = new TypedPathFormatter("FIRSTNAME", @"^\p{L}+$");
            public static readonly TypedPathFormatter LastNameFormatter = new TypedPathFormatter("LASTNAME", @"^\p{L}+$");
        }

        public static readonly TypedPathFormatter[] All = 
        {
            Time.YearFormatter,
            Time.MonthFormatter,
            Time.DayFormatter,
            Time.HourFormatter,
            Time.MinuteFormatter,
            Time.SecondFormatter,
            Time.MillisecondFormatter,

            Text.WordFormatter,
            Text.NumberFormatter,

            Name.FirstNameFormatter,
            Name.LastNameFormatter,
        };

        public string Type { get; }
        public string Regex { get; }

        private TypedPathFormatter(string type, string regex)
        {
            Type = type;
            Regex = regex;
        }

        public override string ToString()
        {
            return $"[{Type}]";
        }

        public static TypedPathFormatter FromString(string type)
        {
            return All.Single(formatter => formatter.Type == type);
        }
    }
}
