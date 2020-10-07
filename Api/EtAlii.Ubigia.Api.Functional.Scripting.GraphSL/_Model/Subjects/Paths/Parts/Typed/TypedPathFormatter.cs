namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Linq;

    public class TypedPathFormatter
    {
        public static class Time
        {
            public static readonly TypedPathFormatter YearFormatter;
            public static readonly TypedPathFormatter MonthFormatter;
            public static readonly TypedPathFormatter DayFormatter;
            public static readonly TypedPathFormatter HourFormatter;
            public static readonly TypedPathFormatter MinuteFormatter;
            public static readonly TypedPathFormatter SecondFormatter;
            public static readonly TypedPathFormatter MillisecondFormatter;

            static Time()
            {
                YearFormatter = new TypedPathFormatter("YYYY", @"^[0123]?[0123456789]{1,3}$");
                MonthFormatter = new TypedPathFormatter("MM", @"^[01]?[0123456789]$");
                DayFormatter = new TypedPathFormatter("DD", @"^[0123]?[0123456789]$");
                HourFormatter = new TypedPathFormatter("HH", @"^[012]?[0123456789]$");
                MinuteFormatter = new TypedPathFormatter("mm", @"^[012345]?[0123456789]$");
                SecondFormatter = new TypedPathFormatter("ss", @"^[012345]?[0123456789]$");
                MillisecondFormatter = new TypedPathFormatter("MMM", @"^[0123456789]{1,3}$");
            }
        }

        public static class Text
        {
            public static readonly TypedPathFormatter WordFormatter;
            public static readonly TypedPathFormatter NumberFormatter;

            static Text()
            {
                WordFormatter = new TypedPathFormatter("WORD", @"^\p{L}+$");
                NumberFormatter = new TypedPathFormatter("NUMBER", @"^[0123456789]+$");
            }
        }

        public static class Name
        {
            public static readonly TypedPathFormatter FirstNameFormatter;
            public static readonly TypedPathFormatter LastNameFormatter;

            static Name()
            {
                FirstNameFormatter = new TypedPathFormatter("FIRSTNAME", @"^\p{L}+$");
                LastNameFormatter = new TypedPathFormatter("LASTNAME", @"^\p{L}+$");
            }
        }

        public static readonly TypedPathFormatter[] All;

        static TypedPathFormatter()
        {
            All = new[]
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
        }
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
