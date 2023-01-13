// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;


public sealed class TypedPathFormatter
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

        MediaPathFormatter.CompanyNameFormatter,
        MediaPathFormatter.ProductFamilyNameFormatter,
        MediaPathFormatter.ProductModelNameFormatter,
        MediaPathFormatter.ProductNumberFormatter,
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
