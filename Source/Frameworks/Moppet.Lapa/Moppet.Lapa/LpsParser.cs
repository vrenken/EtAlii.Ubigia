////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51
// Email: x-ronos@yandex.ru
//
// Translated comments to English using Google Translate: Peter Vrenken - 2014.
//
/////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

namespace Moppet.Lapa;

/// <summary>
/// parser type Func&lt;LpText, LpNode&gt;.
/// At the input text. On leaving one option parsing.
///
/// Class LpsParser constant, ie there are no functions that would change the data class.
/// </summary>
public sealed class LpsParser : LpBaseParser<LpNode, LpsParser>
{
    #region Constructors

    /// <summary>
    /// The default constructor.
    /// </summary>
    public LpsParser()
    {
    }

    /// <summary>
    /// Auxiliary constructor.
    /// </summary>
    /// <param name="parser">parser.</param>
    public LpsParser(Func<LpText, LpNode> parser) : this() { Parser = parser; }

    /// <summary>
    /// Auxiliary constructor.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="parser">parser.</param>
    public LpsParser(string id, Func<LpText, LpNode> parser)
    {
        Identifier = id;
        Parser = parser;
    }

    /// <summary>
    /// Auxiliary constructor.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="wrapNode">wrapping result.</param>
    /// <param name="parser">parser.</param>
    /// <param name="recurse">Support for recursive call.</param>
    public LpsParser(string id, bool wrapNode = false, Func<LpText, LpNode> parser = null, bool recurse = false)
    {
        Identifier = id;
        WrapNode = wrapNode;
        Parser = parser;
        if (recurse)
            Recurse = true;
    }

    #endregion Constructors

    /// <summary>
    /// Summary function parser.
    /// </summary>
    /// <param name="text">Text.</param>
    /// <returns>findings.</returns>
    public LpNode  Do(LpText text)
    {
        LpNode res;
        if (Stack != null)
        {
            //if (m_stack.FindLast(text) != null)
            //    return new LpNode(text);
            //var added = m_stack.AddLast(text);
            //res = m_parser(text);
            //m_stack.Remove(added);

            LinkedListNode<LpText> added = null;
            lock (Stack)
            {
                if (Stack.FindLast(text) != null)
                    return new LpNode(text);
                added = Stack.AddLast(text);
            }
            res = Parser(text);
            lock (Stack)
            {
                Stack.Remove(added);
            }
        }
        else
        {
            res = Parser(text);
        }
        if (Identifier != null)
        {
            if (WrapNode && res.Id != null)
            {
                res = new LpNode(Identifier, res.Match, res.Rest, res);
            }
            else
            {
                res.Id = Identifier;
            }
        }
        return res;
    }

    /// <summary>
    /// An implicit cast.
    /// </summary>
    /// <param name="p">parser.</param>
    /// <returns>object parser.</returns>
    public static implicit operator LpsParser(Func<LpText, LpNode> p)
    {
        return new(p);
    }

    /// <summary>
    /// Operator implicit in parser one character.
    /// </summary>
    /// <param name="ch">symbol.</param>
    /// <returns>object parser.</returns>
    public static implicit operator LpsParser(char ch)
    {
        return Lp.Char(ch);
    }

    /// <summary>
    /// Operator implicit in one line parser.
    /// </summary>
    /// <param name="term">String word.</param>
    /// <returns>object parser.</returns>
    public static implicit operator LpsParser(string term)
    {
        return Lp.Term(term);
    }

    /// <summary>
    /// An implicit cast.
    /// </summary>
    /// <param name="oParser">Parser object.</param>
    /// <returns>parser.</returns>
    public static implicit operator Func<LpText, LpNode>(LpsParser oParser)
    {
        if (oParser.Identifier == null && oParser.Parser != null)
            return oParser.Parser;
        return oParser.Do;
    }

    /// <summary>
    /// The cast operator in multiparser.
    /// </summary>
    /// <param name="parser">Optimized parser.</param>
    /// <returns>Multiparser.</returns>
    public static implicit operator Func<LpText, IEnumerable<LpNode>>(LpsParser parser)
    {
        return text =>
        {
            var result = parser.Do(text);
            return result.Success ? new[] { result } : Array.Empty<LpNode>();
        };
    }

    /// <summary>
    /// The cast operator in multiparser.
    /// </summary>
    /// <param name="parser">Optimized parser.</param>
    /// <returns>Multiparser.</returns>
    public static explicit operator LpmParser(LpsParser parser)
    {
        return new(parser.Identifier, text =>
        {
            var result = parser.Do(text);
            return result.Success ? new[] { result } : Array.Empty<LpNode>();
        });
    }

    /// <summary>
    /// Consistent application of combinatorial parsers.
    /// </summary>
    /// <param name="leftParser">left parser.</param>
    /// <param name="rightParser">Right parser.</param>
    /// <returns>Chain parsers (for deferred construction resulting parser).</returns>
    public static LpsChain operator + (LpsParser leftParser, LpsParser rightParser)
    {
        return new(leftParser, rightParser);

        //return LpsParser.New((text) =>
        //{
        //    var left = leftParser.Do(text);
        //    if (!left.Success)
        //        return LpNode.Fail(text);
        //    var right = rightParser.Do(left.Rest);
        //    if (!right.Success)
        //        return LpNode.Fail(text);
        //    return LpNode.Concat(left, right);
        //});
    }

    /// <summary>
    /// Consistent application of combinatorial parsers.
    /// </summary>
    /// <param name="leftParser">left parser.</param>
    /// <param name="ch">Symbol for compiling parser one character.</param>
    /// <returns>Chain parsers (for deferred construction resulting parser).</returns>
    public static LpsChain operator + (LpsParser leftParser, char ch)
    {
        return new(leftParser, Lp.Char(ch));
    }

    /// <summary>
    /// Consistent application of combinatorial parsers.
    /// </summary>
    /// <param name="ch">Symbol for compiling parser one character.</param>
    /// <param name="rightParser">Right parser.</param>
    /// <returns>Chain parsers (for deferred construction resulting parser).</returns>
    public static LpsChain operator + (char ch, LpsParser rightParser)
    {
        return new(Lp.Char(ch), rightParser);
    }

    /// <summary>
    /// Consistent application of combinatorial parsers.
    /// </summary>
    /// <param name="ch">Function to produce a single character parser.</param>
    /// <param name="rightParser">Right parser.</param>
    /// <returns>Chain parsers (for deferred construction resulting parser).</returns>
    public static LpsChain operator +(Func<char, bool> ch, LpsParser rightParser)
    {
        return new(Lp.One(ch), rightParser);
    }


    /// <summary>
    /// Consistent application of combinatorial parsers.
    /// </summary>
    /// <param name="leftParser">left parser.</param>
    /// <param name="term">Word to compose a simple search for the word parser.</param>
    /// <returns>Chain parsers (for deferred construction resulting parser).</returns>
    public static LpsChain operator + (LpsParser leftParser, string term)
    {
        return new(leftParser, Lp.Term(term));
    }

    /// <summary>
    /// Combinatorial parsers in Chains alternatives.
    /// </summary>
    /// <param name="p1">First parser</param>
    /// <param name="p2">Second parser</param>
    /// <returns>Chain alternatives.</returns>
    public static LpsAlternatives operator | (LpsParser p1, LpsParser p2)
    {
        return new(p1, p2);
    }

    /// <summary>
    /// Combinatorial parsers in Chains alternatives.
    /// </summary>
    /// <param name="p1">First parser.</param>
    /// <param name="ch">Parser one character.</param>
    /// <returns>chain alternatives.</returns>
    public static LpsAlternatives operator | (LpsParser p1, char ch)
    {
        return new(p1, Lp.Char(ch));
    }

    /// <summary>
    /// Combinatorial parsers in Chains alternatives.
    /// </summary>
    /// <param name="ch">Parser one character.</param>
    /// <param name="right">right parser.</param>
    /// <returns>chain alternatives.</returns>
    public static LpsAlternatives operator |(char ch, LpsParser right)
    {
        return new(Lp.Char(ch), right);
    }

    /// <summary>
    /// Combinatorial parsers in Chains alternatives.
    /// </summary>
    /// <param name="left">left parser</param>
    /// <param name="term">Parser one word.</param>
    /// <returns>chain alternatives.</returns>
    public static LpsAlternatives operator | (LpsParser left, string term)
    {
        if (left == null)
            throw new ArgumentNullException(nameof(left));
        if (term == null)
            throw new ArgumentNullException(nameof(term));

        return new LpsAlternatives(left, Lp.Term(term));
    }

    /// <summary>
    /// Combinatorial parsers in Chains alternatives.
    /// </summary>
    /// <param name="right">right parser.</param>
    /// <param name="term">Parser one word.</param>
    /// <returns>chain alternatives.</returns>
    public static LpsAlternatives operator |(string term, LpsParser right)
    {
        if (right == null)
            throw new ArgumentNullException(nameof(right));
        if (term == null)
            throw new ArgumentNullException(nameof(term));
        return new LpsAlternatives(Lp.Term(term), right);
    }

    /// <summary>
    /// Combinatorial parser.
    /// The resulting parser combines the results of two other parsers that are called to the same line.
    /// </summary>
    /// <param name="p1">First parser.</param>
    /// <param name="p2">Second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static LpmParser operator | (LpsParser p1, LpmParser p2)
    {
        if (p1 == null)
            throw new ArgumentNullException(nameof(p1));
        if (p2 == null)
            throw new ArgumentNullException(nameof(p2));

        return new LpmParser((text) =>
        {
            var r1 = p1.Do(text);
            return r1.Success ? new[] { r1 }.Concat(p2.Do(text)).DistinctMatches() : p2.Do(text); // DistinctVoids
        });
    }

    /// <summary>
    /// Combinatorial parser.
    /// The resulting parser combines the results of two other parsers that are called to the same line.
    /// </summary>
    /// <param name="p1">First parser.</param>
    /// <param name="p2">Second parser.</param>
    /// <returns>The resulting parser.</returns>
    public static LpmParser operator | (LpmParser p1, LpsParser p2)
    {
        if (p1 == null)
            throw new ArgumentNullException(nameof(p1));
        if (p2 == null)
            throw new ArgumentNullException(nameof(p2));

        return new LpmParser((text) =>
        {
            var r1 = p1.Do(text);
            var r2 = p2.Do(text);
            if (r2.Success)
                r1 = r1.Concat(new[] { r2 }).DistinctMatches(); // DistinctVoids
            return r1;
        });
    }
}
