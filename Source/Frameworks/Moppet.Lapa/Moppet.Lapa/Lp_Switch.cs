using System;
using System.Collections.Generic;

namespace Moppet.Lapa;

/// <summary>
/// Parsers for lambda expressions. Lp - short for Lambda Parsers.
/// </summary>
public static partial class Lp
{
    /// <summary>
    /// Choosing a variety of options parsing text input character.
    /// </summary>
    /// <param name="parser">The parser that accepts a symbol and an incoming text beginning with that letter.</param>
    /// <returns>parser.</returns>
    public static LpsParser Switch(Func<char, LpText, LpNode> parser)
    {
        return new(null, (t) => // The ID is not needed, it suppresses subsidiaries
        {
            if (t.Length <= 0)
                return new LpNode(t);
            var c = t[0];
            return parser(c, t);
        });
    }

    /// <summary>
    /// Choosing a variety of options.
    /// This implementation is aligned with the parser behindParser, which provides a more optimized design analogue (behindParser + nextParser (...)).
    /// If behindParser performed, then further performed nextParser and success of both parsers results analysis is expanded (uncover) and placed in a single node.
    /// </summary>
    /// <param name="behindParser">The parser, which is concatenated like this (behindParser + nextParser (...)). The results of analysis revealed (uncover) and placed in a single node.</param>
    /// <param name="nextParser">The parser that can change behavior based on the input character.</param>
    /// <param name="ifFurtherNothing">The parser that is used, if after behindParser nothing.</param>
    /// <returns>parser.</returns>
    public static LpsParser Switch(this LpsParser behindParser, Func<char, LpText, LpNode> nextParser, LpsParser ifFurtherNothing = null)
    {
        if (ifFurtherNothing != null) return new LpsParser(null, (t) =>
        {
            var prev = behindParser.Do(t);
            if (prev.Match.Length < 0)
                return prev;
            var rest = prev.Rest;
            var next = rest.Length <= 0  ? ifFurtherNothing.Do(t)  :  nextParser(rest[0], rest);
            if (next.Match.Length < 0)
                return new LpNode(t);

            var child = new List<LpNode>(0x10);
            child.AddChildrenOrNodeOrNothing(prev);
            child.AddChildrenOrNodeOrNothing(next);
            if (child.Count <= 1)
                return child.Count == 0 ? next : child[0];
            return new LpNode(t, next.Rest.Index - t.Index, null, child);
        });

        return new LpsParser(null, (t) =>
        {
            var prev = behindParser.Do(t);
            if (prev.Match.Length < 0)
                return prev;
            var rest = prev.Rest;
            if (rest.Length <= 0)
                return new LpNode(t);

            var next = nextParser(rest[0], rest);
            if (next.Match.Length < 0)
                return new LpNode(t);

            var child = new List<LpNode>(0x10);
            child.AddChildrenOrNodeOrNothing(prev);
            child.AddChildrenOrNodeOrNothing(next);
            if (child.Count <= 1)
                return child.Count == 0 ? next : child[0];
            return new LpNode(t, next.Rest.Index - t.Index, null, child);
        });
    }
}
