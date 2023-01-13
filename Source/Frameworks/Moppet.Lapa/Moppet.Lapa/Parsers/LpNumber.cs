////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51
// Email: x-ronos@yandex.ru
//
/////////////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace Moppet.Lapa.Parsers;

/// <summary>
/// A collection of some simple parsers.
/// </summary>
public static class LpNumber
{
    /// <summary>
    /// The parser is a simple decimal numbers with a positive test of its value.
    /// An example of parsing the date in the format yyyy.MM.dd: Positive (0, 9999, 4) + '.' + Positive (1, 12, 2) + '.' + Positive (1, 31, 2)
    /// </summary>
    /// <param name="minDigits">The minimum number of characters. Required when tedious to parse the number format 001, 002, 012 ...</param>
    /// <returns>parser.</returns>
    public static LpsParser Positive(int minDigits = 1)
    {
        return Positive(0, long.MaxValue, minDigits);
    }

    /// <summary>
    /// The parser is a simple decimal numbers with a positive test of its value.
    /// An example of parsing the date in the format yyyy.MM.dd: Positive (0, 9999, 4) + '.' + Positive (1, 12, 2) + '.' + Positive (1, 31, 2)
    /// </summary>
    /// <param name="minValue">The minimum allowable value. For example, a month may start with one.</param>
    /// <param name="maxValue">The maximum allowable value. For example, the months of the year more than 12 and can not be.</param>
    /// <param name="minDigits">The minimum number of characters. Required when tedious to parse the number format 001, 002, 012 ...</param>
    /// <returns>parser.</returns>
    public static LpsParser Positive(long minValue, long maxValue, int minDigits = 1)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater or equal minValue.");

        if (minValue < 0)
            throw new ArgumentOutOfRangeException(nameof(minValue));

        if (minDigits < 1)
            throw new ArgumentOutOfRangeException(nameof(minDigits));

        var maxDigits = 1;
        var rest = maxValue / 10;
        while (rest > 0)
        {
            rest /= 10; ++maxDigits;
        }
        var parser = new LpsParser("Integer", (text) =>
        {
            var end = text.Length > maxDigits ? (maxDigits + 1) : text.Length;
            int cur = 0, ind = text.Index;
            var str = text.Source;
            var val = 0L;

            while (cur < end)
            {
                var c = str[ind];
                var n = c - '0';
                if (n > 9 || n < 0)
                    break;

                val *= 10;
                val += n;
                ++ind;
                ++cur;
            }
            return (cur >= minDigits && cur <= maxDigits && val >= minValue && val <= maxValue) ? new LpNode(text, cur) : new LpNode(text);
        });
        return parser;
    }
}
