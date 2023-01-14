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
using System.Linq.Expressions;
// ReSharper disable ArrangeStaticMemberQualifier

namespace Moppet.Lapa;

/// <summary>
/// Parsers for lambda expressions. Lp - short for Lambda Parsers.
/// </summary>
public static partial class Lp
{

    #region Tokens

    /// <summary>
    /// Parser one character.
    /// </summary>
    /// <param name="predicate">Lambda identification symbol.</param>
    /// <returns>Парсер символа.</returns>
    public static LpsParser One(Func<char, bool> predicate)
    {
        //TODO: LpLex to alter in Expression
        return new(p => p.Length > 0 && predicate(p[0]) ? new LpNode(p, 1) : new LpNode(p));
    }

    /// <summary>
    /// Parser characters.
    /// </summary>
    /// <param name="ch">symbol.</param>
    /// <returns>Therma parser.</returns>
    public static LpsParser Char(char ch)
    {
        return new(p => p.Length > 0 && p[0] == ch ? new LpNode(p, 1) : new LpNode(p));
    }

    /// <summary>
    /// Parser one or literal numbers.
    /// </summary>
    /// <returns>Therma parser.</returns>
    public static LpsParser LetterOrDigit()
    {
        return new(text => text.Length > 0 && char.IsLetterOrDigit(text[0]) ? new LpNode(text, 1) : new LpNode(text));
    }


    /// <summary>
    /// Greedy search parser one or more characters.
    /// </summary>
    /// <param name="predicate">Predicate.</param>
    /// <returns>Greedy search parser one or more characters.</returns>
    public static LpsParser OneOrMore(this Expression<Func<char, bool>> predicate)
    {
        var func = LpLex.OneOrMore(predicate).Compile();
        return new LpsParser(func);
    }

    /// <summary>
    /// Parser search of one or more characters. Greedy algorithm, but fast.
    /// </summary>
    /// <param name="ch">symbol.</param>
    /// <returns>Parser search of one or more characters.</returns>
    public static LpsParser OneOrMore(char ch)
    {
        return new(text =>
        {
            //fixed (char* end = text.Source + text.Index + text.Length)
            //{
            //    char* beg = end - text.Length, ind = beg;
            //    while (ind < end && *ind == ch) ++ind;

            //    return ind > beg ? new LpNode(text, (int)(ind - beg)) : LpNode.Fail(text);
            //}

            int cur = 0, end = text.Length, ind = text.Index;
            var str = text.Source;
            while (cur < end && str[ind] == ch) { ++ind; ++cur; }
            return cur > 0 ? new LpNode(text, cur) : new LpNode(text);
        });
    }


    /// <summary>
    /// Parser an identifier or name (term), which must start with a certain set of characters (eg only with the letters), and
    /// requirements followed by other characters. Also the name is always limited reach.
    /// </summary>
    /// <param name="firstChar">The first character.</param>
    /// <param name="maybeNextChars">Zero or more subsequent characters.</param>
    /// <param name="maxLength">The maximum length of.</param>
    /// <returns>greedy parser.</returns>
    private static LpsParser Name(Expression<Func<char, bool>> firstChar, Expression<Func<char, bool>> maybeNextChars, int maxLength = int.MaxValue)
    {
        var func = LpLex.Name(firstChar, maybeNextChars, maxLength).Compile();
        return new LpsParser(func);

        //var firstCharFunc = firstChar.Compile();
        //var maybeNextCharsFunc = maybeNextChars.Compile();
        //return new LpsParser("Name", (text) =>
        //{
        //    int end = maxLength < text.Length ? maxLength + 1 : text.Length;
        //    int cur = 0, ind = text.Index;
        //    var str = text.Source;
        //    if (cur < end && firstCharFunc(str[ind])) { ++ind; ++cur; } else { return LpNode.Fail(text); }
        //    while (cur < end && maybeNextCharsFunc(str[ind])) { ++ind; ++cur; }
        //    return cur > maxLength ? LpNode.Fail(text) : LpNode.Take(text, cur);
        //});
    }

    /// <summary>
    /// Parser an identifier or name (term), which must start with a certain set of characters (eg only with the letters), and
    /// requirements followed by other characters. Also the name is always limited reach.
    /// </summary>
    public static LpsParser Name()
    {
        return Lp.Name
        (
            c => char.IsLetter(c) || c == '_',
            c => char.IsLetterOrDigit(c) || c == '_'
        );
    }

    /// <summary>
    /// Parser that looks for matches for the specified term.
    /// </summary>
    /// <param name="term">Word, ie any sequence of characters.</param>
    /// <returns>result.</returns>
    public static LpsParser Term(string term)
    {
        return new(text => text.StartsWith(term) ? new LpNode(text, term.Length) : new LpNode(text));
    }

    /// <summary>
    /// Parser that looks for matches for the specified term.
    /// </summary>
    /// <param name="term">Word, ie any sequence of characters.</param>
    /// <param name="ignoreCase"></param>
    /// <returns>result.</returns>
    public static LpsParser Term(string term, bool ignoreCase)
    {
        return new(text => text.StartsWith(term, ignoreCase) ? new LpNode(text, term.Length) : new LpNode(text));
    }

    /// <summary>
    /// Parser that looks for matches for any given word.
    /// Corresponds to the structure of the regular expression: (a | b | c), where a, b and c - is the word.
    /// Words are matched in the order in which they are passed as arguments.
    /// For example, if you search for "(ab | a)" in the string "ab", it will be found "ab", and if
    /// We seek "(a | ab)", it is found only the first letter.
    /// </summary>
    /// <param name="ignoreCase"></param>
    /// <param name="words">Words. Empty words can not pass here.</param>
    /// <returns>The result for one of the words found.</returns>
    public static LpsParser Any(bool ignoreCase, params string[] words)
    {
        // attention!
        // option Any(params char[] chars) Expression through is not necessary to implement,
        // because here it is Lp.One(c => c == '0' || c == '1' ...) still works several times faster.

        // TODO:have the option to speed up the search by using trees and search the hash table.
        //
        return new(text =>
        {
            var wc = words.Length;
            var tl = text.Length;

            if (tl <= 0)
                return new LpNode(text);

            for (var i = 0; i < wc; ++i)
            {
                var word = words[i];
                if (text.StartsWith(word, ignoreCase))
                    return new LpNode(text, word.Length);
            }
            return new LpNode(text);
        });
    }

    #endregion // Tokens

    #region Special

    /// <summary>
    /// Wrap the parser into new parser to wrap the node and mark it by new id.
    ///
    /// Creates an empty wrapper over parser parser, to the resulting node
    /// also wrap in additional node and tag identifier.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <param name="id">New parser with the new identifier.</param>
    /// <returns>New parser as wrapper.</returns>
    public static LpsParser Wrap(this LpsParser parser, string id)
    {
        if (parser.Identifier == null)
            return new LpsParser(id, wrapNode: true, parser: parser.Recurse ? parser.Do : parser.Parser);
        return new LpsParser(id, wrapNode: true, parser: parser.Do);
    }

    /// <summary>
    /// Wrap the parser into new parser to wrap the node and mark it by new id.
    ///
    /// Creates an empty wrapper over parser parser, to the resulting node
    /// also wrap in additional node and tag identifier.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <param name="id">New parser with the new identifier.</param>
    /// <returns>New parser as wrapper.</returns>
    public static LpsParser Wrap(this LpsChain parser, string id)
    {
        var par = parser.ToParser();
        if (par.Identifier == null)
            return new LpsParser(id, wrapNode: true, parser: par.Recurse ? par.Do : par.Parser);
        return new LpsParser(id, wrapNode: true, parser: par.Do);
    }

    /// <summary>
    /// End of the text block, ie successful returns an empty line if the balance is empty.
    /// </summary>
    /// <returns>Tekstovogo parser thread blocks.</returns>
    public static LpsParser End => new(text => text.Length == 0 ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));

    /// <summary>
    /// Parser that parses the next character but does not capture it, ie returns an empty successful compliance
    /// if the character satisfies or if we end the text, otherwise returns fail.
    /// </summary>
    /// <param name="aheadChar">F-I identify the correct symbol to us.</param>
    /// <returns>Success or failure of a blank line.</returns>
    public static LpsParser Lookahead(char aheadChar)
    {
        return new(text => text.Length <= 0 || text[0] == aheadChar ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));
    }

    /// <summary>
    /// Applies parser and checks the result to be true, but does not capture it, ie returns an empty successful match,
    /// if the parser successfully completed, or if we are at the end of the text, otherwise it returns failure.
    /// </summary>
    /// <param name="ahead">Parser that checks, but what's next there?</param>
    /// <returns>Success or failure of a blank line.</returns>
    public static LpsParser Lookahead(LpsParser ahead)
    {
        return new(text => text.Length <= 0 || ahead.Do(text).Success ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));
    }


    /// <summary>
    /// Returns specific parser one character.
    /// This parser selects a symbol if it is not the start of a chain, which recognizes the parser.
    /// Lp.Not(Lp.Term("345")).OneOrMore().Do("12345") == "12".
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <returns>Parser one character.</returns>
    public static LpsParser Not(this LpsParser parser)
    {
        string id = null;
        if (!string.IsNullOrWhiteSpace(parser.Identifier))
            id = "Not(" + parser.Identifier + ")";

        return new LpsParser(text =>
        {
            if (text.Length <= 0)
                return new LpNode(id, text);
            var result = parser.Do(text);
            return result.Success ? new LpNode(id, text) : new LpNode(text, 1, id);
        });
    }

    /// <summary>
    /// Build a parser to parse an expression in parentheses, taking into account any nesting, ie (((body))).
    /// </summary>
    /// <param name="openBracket">opening bracket.</param>
    /// <param name="body">body.</param>
    /// <param name="closeBracket">Closing parenthesis.</param>
    /// <returns>parser.</returns>
    public static LpsParser InBrackets(LpsParser openBracket, LpsParser body, LpsParser closeBracket)
    {
        return new(new LpBrackets(openBracket, body, closeBracket).Do);
    }

    /// <summary>
    /// Parser to parse the list of one or more elements, recorded, for example, a comma or other delimiter.
    /// For example, the numbers 1,2,3.
    /// Note: for the parameters and maybeSpaces maybeTail not need to do Maybe () itself is taken into account automatically.
    /// </summary>
    /// <param name="listItem">Item.</param>
    /// <param name="delimiter">separator.</param>
    /// <param name="maybeSpaces">Possible gaps between the separator and the list item, for example Lp.OneOrMore (''). By default, spaces are not allowed.</param>
    /// <returns>greedy parser.</returns>
    public static LpsParser List(LpsParser listItem, LpsParser delimiter, LpsParser maybeSpaces = null)
    {
        if (maybeSpaces == null) return new LpsParser(text =>
        {
            var last = listItem.Do(text);
            if (!last.Success)
            {
                return last;
            }

            var children = new List<LpNode>(0x10);
            children.Add(last);

            while (last.Success)
            {
                var delim = delimiter.Do(last.Rest);
                if (!delim.Success)
                    break;
                var next = listItem.Do(delim.Rest);
                if (!next.Success)
                    break;
                last = next;
                children.Add(delim);
                children.Add(last);
            }
            //TODO: Clean up of children and extra blank check,,,
            return new LpNode(text, last.Rest.Index - text.Index, "List", children);
        });

        // ------------------------------------------------------------------
        return new LpsParser(text =>
        {
            var last = listItem.Do(text);
            if (!last.Success)
            {
                return last;
            }

            var children = new List<LpNode>(0x10);
            children.Add(last);

            while(last.Success)
            {
                var left = maybeSpaces.Do(last.Rest);
                var delim  = delimiter.Do(left.Rest);
                if (!delim.Success)
                    break;
                var right = maybeSpaces.Do(delim.Rest);
                var next = listItem.Do(right.Rest);
                if (!next.Success)
                    break;

                // Add spaces, the separator and the list item.
                last = next;
                if (left.Success && left.Match.Length > 0)
                    children.Add(left);
                children.Add(delim);
                if (right.Success && right.Match.Length > 0)
                    children.Add(right);
                children.Add(last);
            }
            //TODO: Clean up of children and extra blank check,,,
            return new LpNode(text, last.Rest.Index - text.Index, "List", children);
        });
    }

    #endregion Special

    #region Lpm special functions

    /// <summary>
    /// Returns a result set with the result of the concatenation prevResults parsing nextParser.
    /// </summary>
    /// <param name="prevResults">Options analysis in the previous step.</param>
    /// <param name="nextParser">Parsing options in the next step.</param>
    /// <returns>concatenation results.</returns>
    internal static IEnumerable<LpNode> Next(this IEnumerable<LpNode> prevResults, LpmParser nextParser)
    {
        foreach (var l in prevResults)
        {
            foreach (var r in nextParser.Do(l.Rest))
            {
                yield return LpNode.Concat(l, r);
            }
        }
    }

    /// <summary>
    /// Implementation Distinct field Match, ie nodes are considered equal if the field Match one node coincides with a similar field of another node.
    /// Other fields are not involved in the comparison.
    /// </summary>
    /// <param name="variants">Distinct parsed.</param>
    /// <returns>Purified from duplicate sequence.</returns>
    public static IEnumerable<LpNode> DistinctMatches(this IEnumerable<LpNode> variants)
    {
        // TODO: Maybe there is a faster way of stowing repeats. How will the time, be sure to analyze.
        return variants.Distinct(new LpNodeMatchComparer());
    }

    /// <summary>
    /// Implementing IEqualityComparer for LpNode. Compared only field Match.
    /// </summary>
    private sealed class LpNodeMatchComparer : IEqualityComparer<LpNode>
    {
        bool IEqualityComparer<LpNode>.Equals(LpNode x, LpNode y)
        {
            return x!.Match.Equals(y!.Match);
        }
        int IEqualityComparer<LpNode>.GetHashCode(LpNode obj)
        {
            return obj.Match.GetHashCode();
        }
    }

    #endregion Lpm special functions

    #region OneOrMore

    /// <summary>
    /// Designer parser chain of one or more elements.
    /// Returns a parser that consistently applies to the text parser, passing parser residue from the previous result, as
    /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
    /// discontinuities. All results are also stored in conformity Children property node.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <returns>parser.</returns>
    public static LpsParser OneOrMore(this LpsParser parser)
    {
        return Lp.OneOrMore((LpCover<LpsParser, LpNode>)parser);
    }

    /// <summary>
    /// Designer parser chain of one or more elements.
    /// Returns a parser that consistently applies to the text parser, passing parser residue from the previous result, as
    /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
    /// discontinuities. All results are also stored in conformity Children property node.
    /// </summary>
    /// <param name="parserInfo">parserInfo.</param>
    /// <returns>parser.</returns>
    private static LpsParser OneOrMore(this LpCover<LpsParser, LpNode> parserInfo)
    {
        var parser = parserInfo.Parser;
        if (!parserInfo.Uncover) return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = parser.Do(text);
            if (!next.Success)
            {
                return next;
            }

            // It turned out that List is 1.5 times faster than LinkedList, even at 10 ppm and arrays.
            // And memory eats one third less if LpNode - class, not the structure.
            var list = new List<LpNode>(0x10);
            while (next.Match.Length > 0)
            {
                list.Add(next);
                next = parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });

        return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = parser.Do(text);
            if (!next.Success)
                return next;

            var list = new List<LpNode>(0x10);
            while (next.Match.Length > 0)
            {
                list.AddChildrenOrNodeOrNothing(next);
                next = parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });
    }

    #endregion

    #region Maybe

    /// <summary>
    /// Faster analogue combination parser.Maybe().TakeMax().
    /// </summary>
    /// <param name="parser">Optimized parser.</param>
    /// <returns>Optimized parser.</returns>
    public static LpsParser Maybe(this LpsParser parser)
    {
        return new(text =>
        {
            var res = parser.Do(text);
            return res.Success ? res : new LpNode(new LpText(text.Source, text.Index, 0), text);
        });
    }

    #endregion // ZeroOrOne

    #region ZeroOrMore

    /// <summary>
    /// Variety combinator parser in the chain, which differs from Lp.OneOrMore (LpsParser) that for the analysis of the first element is responsible
    /// firstItemParser, and for subsequent analysis of the elements responsible maybeNextItemsParser.
    /// All results are stored in compliance Children property node.
    /// </summary>
    /// <param name="firstItem">Parser first element.</param>
    /// <param name="maybeNextItems">Parser subsequent elements.</param>
    /// <returns>parser.</returns>
    public static LpsParser NextZeroOrMore(this LpsChain firstItem, LpsChain maybeNextItems)
    {
        return firstItem.ToParser().NextZeroOrMore(maybeNextItems.ToParser());
    }

    /// <summary>
    /// Variety combinator parser in the chain, which differs from Lp.OneOrMore (LpsParser) that for the analysis of the first element is responsible
    /// firstItemParser, and for subsequent analysis of the elements responsible maybeNextItemsParser.
    /// All results are stored in compliance Children property node.
    /// </summary>
    /// <param name="firstItem">Parser first element.</param>
    /// <param name="maybeNextItems">Parser subsequent elements.</param>
    /// <returns>parser.</returns>
    private static LpsParser NextZeroOrMore(this LpsParser firstItem, LpUncover<LpsParser, LpNode> maybeNextItems)
    {
        return NextZeroOrMore((LpUncover<LpsParser, LpNode>)firstItem, maybeNextItems);
    }

    /// <summary>
    /// Implements pattern (firstItem + maybeNextItems *).
    /// Variety combiner parserov in Chains, kotoryj otličaetsâ by Lp.OneOrMore (LpsParser) tem, What to understanding Pervogo element otvečaet
    /// firstItemParser, and understanding posleduûŝih elements otvečaet maybeNextItemsParser.
    /// All najdennye matching Reserved v The Children node.
    /// </summary>
    /// <param name="firstItem">Parser first element.</param>
    /// <param name="maybeNextItems">Parser subsequent elements.</param>
    /// <returns>parser.</returns>
    private static LpsParser NextZeroOrMore(this LpUncover<LpsParser, LpNode> firstItem, LpUncover<LpsParser, LpNode> maybeNextItems)
    {

        if (!maybeNextItems.Uncover) return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = firstItem.Parser.Do(text);
            if (!next.Success)
                return next;

            var list = new List<LpNode>(0x10);
            if (firstItem.Uncover)
                list.AddChildrenOrNodeOrNothing(next);
            else if (next.Match.Length > 0) //TODO: Protest if Length == 0, а next - false
                list.Add(next);

            next = maybeNextItems.Parser.Do(next.Rest);
            while (next.Match.Length > 0)
            {
                list.Add(next);
                next = maybeNextItems.Parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });

        return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = firstItem.Parser.Do(text);
            if (!next.Success)
                return next;

            var list = new List<LpNode>(0x10);
            if (firstItem.Uncover)
                list.AddChildrenOrNodeOrNothing(next);
            else if (next.Match.Length > 0)
                list.Add(next);

            next = maybeNextItems.Parser.Do(next.Rest);
            while (next.Match.Length > 0)
            {
                list.AddChildrenOrNodeOrNothing(next);
                next = maybeNextItems.Parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });
    }

    /// <summary>
    /// Function to add a node to the list.
    /// If the node has children, instead of adding children nodes. In other cases, the node is added.
    /// This function is used when the option to uncover some specific functions, such MaybeNext.
    /// </summary>
    /// <param name="list">list.</param>
    /// <param name="node">node.</param>
    private static void AddChildrenOrNodeOrNothing(this IList<LpNode> list, LpNode node)
    {
        if (node.Match.Length <= 0)
            return;

        if (node.Children == null)
        {
            list.Add(node);
            return;
        }
        using var childrens = node.Children.GetEnumerator();
        if (!childrens.MoveNext())
        {
            list.Add(node);
            return;
        }
        do
        {
            list.Add(childrens.Current);
        }
        while (childrens.MoveNext());
    }

    /// <summary>
    ///Greedy parser zero or more characters.
    /// </summary>
    /// <param name="predicate">Predicate to select characters.</param>
    /// <returns>greedy parser.</returns>
    public static LpsParser ZeroOrMore(this Expression<Func<char, bool>> predicate)
    {
        var func = LpLex.ZeroOrMore(predicate).Compile();
        return new LpsParser(func);

        //return new LpsParser("ZeroOrMore", (text) =>
        //{
        //    int end = text.Length, cur = 0, ind = text.Index;
        //    var str = text.Source;
        //    while (cur < end && predicate(str[ind])) { ++ind; ++cur; }
        //    return LpNode.Take(text, cur);
        //});
    }


    /// <summary>
    /// Designer parser chain of zero or more elements.
    /// Returns a parser that consistently applies to the text parser, passing parser residue from the previous result, as
    /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
    /// discontinuities. All results are also stored in conformity Children property node.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <returns>parser.</returns>
    public static LpsParser ZeroOrMore(this LpsChain parser)
    {
        return Lp.ZeroOrMore(new LpCover<LpsParser, LpNode>(parser.ToParser(), false));
    }

    /// <summary>
    /// Designer parser chain of zero or more elements.
    /// Returns a parser that consistently applies to the text parser, passing parser residue from the previous result, as
    /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
    /// discontinuities. All results are also stored in conformity Children property node.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <returns>parser.</returns>
    public static LpsParser ZeroOrMore(this LpsParser parser)
    {
        return Lp.ZeroOrMore(new LpCover<LpsParser, LpNode>(parser, false));
    }

    /// <summary>
    /// Designer parser chain of zero or more elements.
    /// Returns a parser that consistently applies parserInfo.Parser to text, passing parserInfo.Parser residue from the previous result, as
    /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
    /// discontinuities. All results are also stored in conformity Children property node.
    /// </summary>
    /// <param name="parserInfo">parserInfo.</param>
    /// <returns>parser.</returns>
    private static LpsParser ZeroOrMore(this LpCover<LpsParser, LpNode> parserInfo)
    {
        var parser = parserInfo.Parser;
        if (!parserInfo.Uncover) return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = parser.Do(text);
            if (!next.Success)
            {
                return new LpNode(new LpText(text.Source, text.Index, 0), text);
            }

            // It turned out that List is 1.5 times faster than LinkedList, even at 10 ppm and arrays.
            // And memory eats one third less if LpNode - class, not the structure.
            var list = new List<LpNode>(0x10);
            while (next.Match.Length > 0)
            {
                list.Add(next);
                next = parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });

        return new LpsParser(id: null, wrapNode: true, parser: text =>
        {
            var next = parser.Do(text);
            if (!next.Success)
            {
                return new LpNode(new LpText(text.Source, text.Index, 0), text);
            }

            var list = new List<LpNode>(0x10);
            while (next.Match.Length > 0)
            {
                list.AddChildrenOrNodeOrNothing(next);
                next = parser.Do(next.Rest);
            }
            if (list.Count <= 1)
                return list.Count == 0 ? next : list[0];
            return new LpNode(text, next.Rest.Index - text.Index, null, list);
        });
    }

    /// <summary>
    /// Greedy parser zero or more characters.
    /// </summary>
    /// <param name="ch">symbol.</param>
    /// <returns>greedy parser.</returns>
    public static LpsParser ZeroOrMore(char ch)
    {
        return new(text =>
        {
            int end = text.Length, cur = 0, ind = text.Index;
            var str = text.Source;
            while (cur < end && str[ind] == ch) { ++ind; ++cur; }
            return new LpNode(text, cur);
        });
    }

    #endregion ZeroOrMore

}
