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
    private static LpsParser Time()
    {
        var delimiter1 = Lp.Char(':');
        var delimiter2 = Lp.Char('.');

        var hour  = LpNumber.Positive(0, 23).Rename("h");
        var minute   = LpNumber.Positive(0, 59).Rename("m");
        var second   = LpNumber.Positive(0, 59).Rename("s");
        var millisecond  = LpNumber.Positive(0, 999).Rename("f");

        var time1 = hour + delimiter1 + minute + delimiter1 + second + delimiter2 + millisecond;
        var time2 = hour + delimiter1 + minute + delimiter1 + second;
        var time3 = hour + delimiter1 + minute;

        return (time1 | time2 | time3).TakeFirst().Id("Time", true);
    }

    /// <summary>
    /// Parses the output of the parser Time ().
    /// In case of success in recording time period.
    /// </summary>
    /// <param name="n">The result of the parser.</param>
    /// <param name="time">The variable, which records the time.</param>
    /// <returns>Truth is, if you could make out all the components of time and write the result in time.</returns>
    private static bool TryParseTime(LpNode n, ref TimeSpan time)
    {
        if (n.Children == null || !n.Success)
            return false;

        int h = 0, m = 0, s = 0, f = 0, ok = 0;
        foreach (var cn in n.Children)
        {
            switch (cn.Id)
            {
                case "h":
                    if (!int.TryParse(cn.Match.ToString(), out h))
                        return false;
                    ok |= 1;
                    break;
                case "m":
                    if (!int.TryParse(cn.Match.ToString(), out m))
                        return false;
                    ok |= 2;
                    break;
                case "s":
                    if (!int.TryParse(cn.Match.ToString(), out s))
                        return false;
                    break;
                case "f":
                    if (!int.TryParse(cn.Match.ToString(), out f))
                        return false;
                    break;
            }
        }
        if (ok != 3)
            return false;

        try
        {
            time = new TimeSpan(days: 0, hours: h, minutes: m, seconds: s, milliseconds: f);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
