namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    public class TypedPathFormatter
    {
        public static class Time
        {
            public static TypedPathFormatter YearFormatter => _yearFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _yearFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("YYYY", @"^[0123]?[0123456789]{1,3}$"));
            public static TypedPathFormatter MonthFormatter => _monthFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _monthFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("MM", @"^[01]?[0123456789]$"));
            public static TypedPathFormatter DayFormatter => _dayFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _dayFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("DD", @"^[0123]?[0123456789]$"));
            public static TypedPathFormatter HourFormatter => _hourFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _hourFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("HH", @"^[012]?[0123456789]$"));
            public static TypedPathFormatter MinuteFormatter => _minuteFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _minuteFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("mm", @"^[012345]?[0123456789]$"));
            public static TypedPathFormatter SecondFormatter => _secondFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _secondFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("ss", @"^[012345]?[0123456789]$"));
            public static TypedPathFormatter MillisecondFormatter => _millisecondFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _millisecondFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("MMM", @"^[0123456789]{1,3}$"));
        }

        public static class Text
        {
            public static TypedPathFormatter WordFormatter => _wordFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _wordFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("WORD", @"^\p{L}+$"));
            public static TypedPathFormatter NumberFormatter => _numberFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _numberFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("NUMBER", @"^[0123456789]+$"));
        }

        public static class Name
        {
            public static TypedPathFormatter FirstNameFormatter => _firstNameFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _firstNameFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("FIRSTNAME", @"^\p{L}+$"));
            public static TypedPathFormatter LastNameFormatter => _lastNameFormatter.Value;
            private static readonly Lazy<TypedPathFormatter> _lastNameFormatter = new Lazy<TypedPathFormatter>(() => new TypedPathFormatter("LASTNAME", @"^\p{L}+$"));
        }

        public static TypedPathFormatter[] All => _all.Value;
        private static readonly Lazy<TypedPathFormatter[]> _all = new Lazy<TypedPathFormatter[]>(() => new TypedPathFormatter[]
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
        });

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
