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
    /// Constructs a date parser in the following variants:
    /// (28/07/2009 | 28.07.2009 | 28-07-2009 | 2009/7/28 | 2009.7.28 | 2009-7-28 | 0001-7-28 | 28/07/09 | 28.07.99 | 28 -07-09).
    /// The last three formats can not be determined because There is a small chance that we will look at the date where the year at the beginning, and at the end of the day.
    /// The separator is marked with a '-', year, month and day, respectively, signs 'Y', 'M', 'D'.
    /// </summary>
    /// <returns>parser.</returns>
    private static LpsParser Date()
    {
        var year  = LpNumber.Positive(0, 9999, 4).Rename("Y");
        var month = LpNumber.Positive(1, 12).Rename("M");
        var day   = LpNumber.Positive(1, 31).Rename("D");

        var dMinus     = Lp.Char('-' ).Rename("-");
        var dPoint     = Lp.Char('.' ).Rename("-");
        var dSlash     = Lp.Char('/' ).Rename("-");
        var dBackSlash = Lp.Char('\\').Rename("-");

        // Whatever options are always in the middle of the month.

        // 28/07/2009 | 28.07.2009 | 28-07-2009 |
        var caseMinus1  = (dMinus + month + dMinus + year).ToParser().Parser;
        var casePoint1  = (dPoint + month + dPoint + year).ToParser().Parser;
        var caseSlash1  = (dSlash + month + dSlash + year).ToParser().Parser;
        var caseBSlash1 = (dBackSlash + month + dBackSlash + year).ToParser().Parser;

        var date1 = day.Switch((c, t) => { switch (c)
        {
            case '-' : return caseMinus1(t);
            case '.' : return casePoint1(t);
            case '/' : return caseSlash1(t);
            case '\\': return caseBSlash1(t);
            default  : return new LpNode(t);
        }});


        // 2009/7/28 | 2009.7.28 | 2009-7-28 | 0001-7-28
        var caseMinus2  = (dMinus     + month + dMinus     + day).ToParser().Parser;
        var casePoint2  = (dPoint     + month + dPoint     + day).ToParser().Parser;
        var caseSlash2  = (dSlash     + month + dSlash     + day).ToParser().Parser;
        var caseBSlash2 = (dBackSlash + month + dBackSlash + day).ToParser().Parser;
        var date2 = year.Switch((c, t) => { switch (c)
        {
            case '-' : return caseMinus2(t);
            case '.' : return casePoint2(t);
            case '/' : return caseSlash2(t);
            case '\\': return caseBSlash2(t);
            default  : return new LpNode(t);
        }});


        // The following options are clearly defined, if the day more than 12
        day   = LpNumber.Positive(1,  31, 2).Rename("D");
        month = LpNumber.Positive(1,  12, 2).Rename("M");
        year  = LpNumber.Positive(0,  99, 2).Rename("Y");

        // 28/07/09 | 28.07.09 | 28-07-09
        var caseMinus3  = (dMinus     + month + dMinus     + year).ToParser().Parser;
        var casePoint3  = (dPoint     + month + dPoint     + year).ToParser().Parser;
        var caseSlash3  = (dSlash     + month + dSlash     + year).ToParser().Parser;
        var caseBSlash3 = (dBackSlash + month + dBackSlash + year).ToParser().Parser;
        var date3 = day.Switch((c, t) => { switch (c)
        {
            case '-' : return caseMinus3(t);
            case '.' : return casePoint3(t);
            case '/' : return caseSlash3(t);
            case '\\': return caseBSlash3(t);
            default  : return new LpNode(t);
        }});

        //var date = Lp.If
        //(
        //    condition: (c) => c >= '0' && c <= '9',
        //    ifTrue   : (date1 | date2 | date3).TakeFirst(),
        //    ifFalse  : Lp.Fail
        //);
        var date = (date1 | date2 | date3).TakeFirst();
        var result = Lp.Switch((c, t) => (c >= '0' && c <= '9') ? date.Parser(t) : new LpNode(t));
        return result.Id("Date", true);
    }

    /// <summary>
    /// Parses the output of the parser Date ().
    /// If successful records to date only the date but not the time.
    /// </summary>
    /// <param name="n">The result of the parser.</param>
    /// <param name="date">The variable, which is written the date.</param>
    /// <returns>Truth is, if you could make out all the components of the date and record the result in date.</returns>
    private static bool TryParseDate(LpNode n, ref DateTime date)
    {
        if (n.Children == null || !n.Success)
            return false;

        int year = -1, month = -1, day = -1, ok = 0;
        foreach (var cn in n.Children)
        {
            switch (cn.Id)
            {
                case "Y":
                    if (!int.TryParse(cn.Match.ToString(), out year))
                        return false;
                    ok |= 1;
                    break;
                case "M":
                    if (!int.TryParse(cn.Match.ToString(), out month))
                        return false;
                    ok |= 2;
                    break;
                case "D":
                    if (!int.TryParse(cn.Match.ToString(), out day))
                        return false;
                    ok |= 4;
                    break;
            }
        }
        if (ok != (1 | 2 | 4))
        {
            return false;
        }

        try
        {
            date = new DateTime(year, month, day);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
