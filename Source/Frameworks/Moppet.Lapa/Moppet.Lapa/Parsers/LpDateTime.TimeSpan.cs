////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51, +7(964)595-55-94
// Email: x-ronos@yandex.ru
//
/////////////////////////////////////////////////////////////////////////////////////////////////

namespace Moppet.Lapa.Parsers;

using System;

/// <summary>
/// The parser date and time.
/// List format: http://msdn.microsoft.com/ru-ru/library/saw4x629.aspx.
/// </summary>
public static partial class LpDateTime
{
    /// <summary>
    /// Constructs a parser of time in the following format (hh: mm: ss | hh: mm | hh: mm: ss.fff).
    /// Hours, minutes, seconds and milliseconds are marked as "h", "m", "s" and "f", respectively.
    /// All dividers are marked by a space.
    /// Remark PV: This method was commented before.
    /// </summary>
    /// <returns>parser.</returns>
    public static LpsParser TimeSpan()
    {
        var delimiter = Lp.Char(':');
        var delimiter2 = Lp.Char('.');
        var isNegative = Lp.Char('-').Rename("n");
        var days = LpNumber.Positive().Rename("d");
        var hours = LpNumber.Positive().Rename("h");
        var minutes = LpNumber.Positive().Rename("m");
        var seconds = LpNumber.Positive().Rename("s");
        var milliseconds = (delimiter2 + LpNumber.Positive()).Rename("f");

        var timeSpan1 = (isNegative.Maybe() + days + delimiter + hours + delimiter + minutes + delimiter + seconds + milliseconds.Maybe());
        var timeSpan2 = (isNegative.Maybe()                    + hours + delimiter + minutes + delimiter + seconds + milliseconds.Maybe());
        var timeSpan3 = (isNegative.Maybe()                    + hours + delimiter + minutes);
        return (timeSpan1 | timeSpan2 | timeSpan3).TakeFirst().Id("TimeSpan", false);
    }

    /// <summary>
    /// Parses the output of the parser TimeSpan ().
    /// In case of success in recording time period.
    /// </summary>
    /// <param name="n">The result of the parser.</param>
    /// <param name="time">The variable, which records the time.</param>
    /// <returns>Truth is, if you could make out all the components of time and write the result in time.</returns>
    public static bool TryParseTimeSpan(LpNode n, ref TimeSpan time)
    {
        if (n.Children == null || !n.Success)
        {
            return false;
        }

        var isNegative = false;

        int d = 0, h = 0, m = 0, s = 0, f = 0, ok = 0;
        foreach (var cn in n.Children)
        {
            switch (cn.Id)
            {
                case "n":
                    isNegative = true;
                    break;
                case "d":
                    if (!int.TryParse(cn.Match.ToString(), out d))
                    {
                        return false;
                    }
                    break;
                case "h":
                    if (!int.TryParse(cn.Match.ToString(), out h))
                    {
                        return false;
                    }
                    ok |= 1;
                    break;
                case "m":
                    if (!int.TryParse(cn.Match.ToString(), out m))
                    {
                        return false;
                    }
                    ok |= 2;
                    break;
                case "s":
                    if (!int.TryParse(cn.Match.ToString(), out s))
                    {
                        return false;
                    }
                    break;
                case "f":
                    if (!int.TryParse(cn.Match.ToString().TrimStart('.'), out f))
                    {
                        return false;
                    }
                    break;
            }
        }
        if (ok != (1 | 2))
        {
            return false;
        }

        try
        {
            time = new TimeSpan(days: d, hours: h, minutes: m, seconds: s, milliseconds: f);
            time = isNegative ? -time : time;
        }
        catch
        {
            return false;
        }
        return true;
    }
}
