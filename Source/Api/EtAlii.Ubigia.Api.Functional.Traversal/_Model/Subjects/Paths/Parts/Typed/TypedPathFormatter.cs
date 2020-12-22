namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;


    public class TypedPathFormatter
    {
        public static readonly TypedPathFormatter[] All =
        {
            TimePathFormatter.YearFormatter,
            TimePathFormatter.MonthFormatter,
            TimePathFormatter.DayFormatter,
            TimePathFormatter.HourFormatter,
            TimePathFormatter.MinuteFormatter,
            TimePathFormatter.SecondFormatter,
            TimePathFormatter.MillisecondFormatter,

            TextPathFormatter.WordFormatter,
            TextPathFormatter.NumberFormatter,

            NamePathFormatter.FirstNameFormatter,
            NamePathFormatter.LastNameFormatter,
        };

        public string Type { get; }
        public string Regex { get; }

        internal TypedPathFormatter(string type, string regex)
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
