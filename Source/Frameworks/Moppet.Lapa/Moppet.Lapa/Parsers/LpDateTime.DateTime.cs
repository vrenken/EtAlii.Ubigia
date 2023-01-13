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
    /// Builds parser of date and time. The combination of two parsers 'Date ()' and 'Time ()'.
    /// Constructs a parser date and time, the combination of parsers Date () and Time ().
    /// Remark PV: This method was commented before.
    /// </summary>
    /// <returns>Parser.</returns>
    public static LpsParser DateTime()
    {
        var date   = Date().Wrap("d");
        var time = Time().Wrap("t");
        var spaces = Lp.OneOrMore(' ');

        var format1 = date + spaces + time;
        var format2 = date;
        return (format1 | format2).TakeFirst().Id("DateTime", false);
    }

    public static bool TryParseDateTime(LpNode n, ref DateTime dateTime)
    {
        if (n.Children == null || !n.Success)
            return false;

        var dn = n.FirstOrDefault("Date");
        var tn = n.FirstOrDefault("Time");

        if (!TryParseDate(dn, ref dateTime))
        {
            return false;
        }

        if (tn != null)
        {
            var time = System.TimeSpan.MinValue;
            if (!TryParseTime(tn, ref time))
            {
                return false;
            }
            dateTime += time;
        }
        return true;
    }
}
