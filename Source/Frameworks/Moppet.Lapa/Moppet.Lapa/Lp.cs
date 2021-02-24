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

namespace Moppet.Lapa
{
	/// <summary>
    /// Parsers for lambda expressions. Lp - short for Lambda Parsers.
	/// </summary>
	public static partial class Lp
	{
		/// <summary>
		/// Returns all the search matches throughout the text.
		/// Sequence is applied to the text parser, passing the remainder of the previous parser successful outcome, until
		/// While the text is recognized. Returns all matching results. Found between compliance can not be
		/// Breaks.
		/// </Summary>
		/// <param name="parser">parser.</param>
		/// <param name="text">block of text.</param>
		/// <returns>Results.</Returns>
		public static IEnumerable<LpNode> Matches(LpmParser parser, LpText text)
		{
			foreach (var res in parser.Do(text))
			{
				yield return res;

                // If the previous result is productive and if there is a balance, looking on.
				if (res.Match.Length > 0 && res.Rest.Length > 0)
				{
					foreach (var sub in Matches(parser, res.Rest))
						yield return sub;
				}
			}
		}

		/// <summary>
		/// Consistently applied to the text parser, passing parser residue from the previous result, until
		/// yet recognized text. Returns all matching results. Found between compliance can not be
        /// discontinuities.</summary>
        /// <param name="parser">parser.</param>
		/// <param name="text">text Block.</param>
        /// <returns>findings.</returns>
		public static IEnumerable<LpNode> Matches(LpsParser parser, LpText text)
		{
			return Chain(parser, text);
		}

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
        /// Parser one number [0;9].
		/// </summary>
		/// <returns>Therma parser.</returns>
		public static LpsParser Digit()
		{
            return new(p => p.Length > 0 && char.IsDigit(p[0]) ? new LpNode(p, 1) : new LpNode(p));
		}

        /// <summary>
        /// Parser one decimal digit in a given range [minValue; maxValue].
        /// For example, you expect only the digit from 1 to 6, then call Lp.Digit (1, 6).
        /// </summary>
        /// <param name="minDigitValue">minimum value  [0;9].</param>
        /// <param name="maxDigitValue">maximum value [0;9].</param>
        /// <returns>parser.</returns>
        public static LpsParser Digit(int minDigitValue, int maxDigitValue)
        {
            return new(p =>
            {
                if (p.Length <= 0 || !char.IsDigit(p[0]))
                    return new LpNode(p);
                var value = p[0] - '0';
                return minDigitValue >= value && value <= maxDigitValue ? new LpNode(p, 1) : new LpNode(p);
            });
        }

		/// <summary>
        /// One literal parser.
		/// </summary>
		/// <returns>Therma parser.</returns>
		public static LpsParser Letter()
		{
            return new(text => text.Length > 0 && char.IsLetter(text[0]) ? new LpNode(text, 1) : new LpNode(text));
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
        /// Parser searches digits in a given range.
		/// </summary>
        /// <param name="minCount">The minimum allowable number of digits, but not less than.</param>
        /// <param name="maxCount">Maximal allowable number of digits, but not more than.</param>
		/// <returns>greedy parser.</returns>
		public static LpsParser Digits(int minCount = 1, int maxCount = int.MaxValue)
		{
            return Range(c => c >= '0' && c <= '9', minCount, maxCount);
		}

        /// <summary>
        /// Parser search string of characters in the specified range.
        /// Greedy algorithm, ie, take the maximum allowed sequence.
        /// </summary>
        /// <param name="predicate">Lambda request.</param>
        /// <param name="minCount">The minimum number of characters allowed, but not less than.</param>
        /// <param name="maxCount">Maximum allowable number of characters, but no more.</param>
        /// <returns>parser.</returns>
        public static LpsParser Range(Expression<Func<char, bool>> predicate, int minCount, int maxCount)
        {
            if (minCount < 0)
                throw new ArgumentOutOfRangeException(nameof(minCount), "minCount must be greater than or equal zero.");

            if (minCount > maxCount)
                throw new ArgumentOutOfRangeException(nameof(maxCount), "maxCount must be greater than or equal minCount.");

            var func = LpLex.Range(predicate, minCount, maxCount).Compile();
            return new LpsParser(func);
        }

        /// <summary>
        /// Parser search for the specified number of characters in the range.
        /// Greedy algorithm, ie, take the maximum allowed sequence.
        /// </summary>
        /// <param name="ch">symbol.</param>
        /// <param name="minCount">The minimum number of characters allowed, but not less than.</param>
        /// <param name="maxCount">Maximum allowable number of characters, but no more.</param>
        /// <returns>parser.</returns>
        public static LpsParser Range(char ch, int minCount, int maxCount)
        {
            if (minCount < 0)
                throw new ArgumentOutOfRangeException(nameof(minCount), "minCount must be greater than or equal zero.");

            if (minCount > maxCount)
                throw new ArgumentOutOfRangeException(nameof(maxCount), "maxCount must be greater than or equal minCount.");

            return new LpsParser(text =>
            {
                var end = text.Length > maxCount ? maxCount + 1 : text.Length;
                int cur = 0, ind = text.Index;
                var str = text.Source;
                while (cur < end && str[ind] == ch) { ++ind; ++cur; }
                return cur >= minCount && cur <= maxCount ? new LpNode(text, cur) : new LpNode(text);
            });
        }

		/// <summary>
        /// Parser to search for one or more literal.
		/// </summary>
        /// <returns>Parser for determining one or more literal.</returns>
		public static LpsParser Letters()
		{
			return new(text =>
			{
				int end = text.Length, cur = 0, ind = text.Index;
				var str = text.Source;
				while (cur < end && char.IsLetter(str[ind])) { ++ind; ++cur; }
                return cur > 0 ? new LpNode(text, cur) : new LpNode(text);
			});
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
        public static LpsParser Name(Expression<Func<char, bool>> firstChar, Expression<Func<char, bool>> maybeNextChars, int maxLength = int.MaxValue)
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
        /// Parser an identifier or a domain name, which must start with a certain set of characters (eg letters only)
        /// and requirements for subsequent characters other. In behalf of always limited reach.
        /// Here name can be written with a hyphen (dashChar), with a dash can not be repeated more than once in a row (-), to be at the beginning or end of the name.
        /// After a dash or trailing characters allowed lastChars.
        /// </summary>
        /// <param name="firstChars">The first character or characters.</param>
        /// <param name="dashChar">Symbol denoting a dash.</param>
        /// <param name="lastChars">Valid characters after the dash or end of.</param>
        /// <param name="maxLength">The maximum length of the name.</param>
        /// <returns>greedy parser.</returns>
        public static LpsParser Name(Expression<Func<char, bool>> firstChars, char dashChar, Expression<Func<char, bool>> lastChars, int maxLength = int.MaxValue)
        {
            var func = LpLex.Name(firstChars, dashChar, lastChars, maxLength).Compile();
            return new LpsParser(func);
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
        /// Corresponds to the structure of the regular expression: (a | b | c), where a, b ​​and c - is the word.
        /// Words are matched in the order in which they are passed as arguments.
        /// For example, if you search for "(ab | a)" in the string "ab", it will be found "ab", and if
        /// We seek "(a | ab)", it is found only the first letter.
        /// </summary>
        /// <param name="words">Words. Empty words can not pass here.</param>
        /// <returns>The result for one of the words found.</returns>
        public static LpsParser Any(params string[] words)
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
                    if (text.StartsWith(word))
                        return new LpNode(text, word.Length);
                }
                return new LpNode(text);
            });
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
        /// Makes new copy of the parser's and sets WrapNode to true.
        /// </summary>
        /// <param name="parser">Parser.</param>
        /// <returns>New parser.</returns>
        public static LpsParser Wrap(this LpsParser parser)
        {
            if (parser.Identifier == null)
                throw new ArgumentException("The 'Identifier' property should be initialized.");
            if (parser.Recurse)
                return Wrap(parser, parser.Identifier);
            return new LpsParser(id: parser.Identifier, wrapNode: true, parser: parser.Parser, recurse: false);
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
        /// Successful returns an empty line if there is some more text.
        /// </summary>
        /// <returns>parser.</returns>
        public static LpsParser NotEnd => new(text => text.Length > 0 ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));

        /// <summary>
		/// Parser empty successful match.
		/// Always returns a blank line.
        /// </summary>
		/// <returns>parser.</returns>
		public static LpsParser Empty => new("Empty", text => new LpNode(new LpText(text.Source, text.Index, 0), text));

        /// <summary>
        /// Parser that always returns failure have not even begun to parse the text.
        /// This is good to use a parser combinator satisfying Lp.If.
        /// </summary>
        /// <returns>parser.</returns>
        public static LpsParser Fail => new("Fail", p => new LpNode(p));

        /// <summary>
        /// Returns the maximum length of line, or null.
        /// Match all must belong to one source.
        /// </summary>
		/// <param name="results">Correspondences. Match all must belong to one source.</param>
		/// <returns>node or null.</returns>
        private static LpNode Max(this IEnumerable<LpNode> results)
		{
			var maxLen = -int.MaxValue;
			LpNode maxResult = null;

			foreach (var r in results)
			{
				if (r.Rest.Length == 0) // If there is no remainder, then the maximum length.
					return r;

				if (r.Match.Length > maxLen)
				{
					maxLen = r.Match.Length;
					maxResult = r;
				}
			}
			return maxResult;
		}

		/// <summary>
		/// Converts multiparser that returns many options indiscriminately into a single (with a result of the parser), choosing an appropriate maximum length.
		/// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <returns>Singleparser.</returns>
		public static LpsParser TakeMax(this LpmParser parser)
		{
            return new(p => parser.Do(p).Max() ?? new LpNode(parser.Identifier, p));
		}

		/// <summary>
        /// Converts multiparser that returns many options indiscriminately into a single (with a result of the parser)
        /// choosing (expecting) the first and only line.
        /// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <returns>Singleparser.</returns>
		public static LpsParser TakeOne(this LpmParser parser)
		{
            return new(p => parser.Do(p).FirstOrDefault() ?? new LpNode(parser.Identifier, p));
		}

		/// <summary>
		/// Adds to the parser filter that selects results in a predetermined wavelength.
		/// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <param name="minLength">The minimum length of conformity.</param>
		/// <param name="maxLength">The maximum allowed length of conformity.</param>
		/// <returns>Multiparser filter.</returns>
		public static LpmParser TakeRange(this LpmParser parser, int minLength, int maxLength)
		{
			return new(p => parser.Do(p).Where(r => minLength <= r.Match.Length && r.Match.Length <= maxLength));
		}

        /// <summary>
        /// Adds to the parser function processing (repair or validation) compliance.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="treat">Any handler (successful or not) result (compliance).</param>
        /// <returns>Followed by the parser results.</returns>
        public static LpsParser Treat(this LpsParser parser, Func<LpNode, LpNode> treat)
        {
            return new(parser.Identifier, t =>
            {
                var node = parser.Do(t);
                return treat(node);
            });
        }

        /// <summary>
        /// Adds to the parser function processing (repair or validation) compliance
        /// If a match is successful (Success).
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="treat">Handler successful outcome (compliance).</param>
        /// <returns>Parser followed by results.</returns>
        public static LpsParser TreatSuccess(this LpsParser parser, Func<LpNode, LpNode> treat)
        {
            return new(parser.Identifier, t =>
            {
                var node = parser.Do(t);
                if (!node.Success)
                    return node;
                return treat(node);
            });
        }

        /// <summary>
        /// Adds to the parser function processing (repair or validation) compliance
        /// if a match fails (! Success).
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="treat">Handler unsuccessful outcome (compliance).</param>
        /// <returns>Parser followed by results.</returns>
        public static LpsParser TreatFail(this LpsParser parser, Func<LpNode, LpNode> treat)
        {
            return new(parser.Identifier, t =>
            {
                var node = parser.Do(t);
                if (node.Success)
                    return node;
                return treat(node);
            });
        }


		/// <summary>
		/// Adds to the parser arbitrary filter results.
		/// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <param name="filter">Filter matches. Receives correspondence, returns false if this correspondence should be excluded from the results.</param>
		/// <returns>Multiparser filter.</returns>
		public static LpmParser Filter(this LpmParser parser, Func<LpText, bool> filter)
		{
			return new(p => parser.Do(p).Where(r => filter(r.Match)));
		}

		/// <summary>
        /// Parser that analyzes the character underneath the block of text (Lookbehind).
        /// Ie he looks at the character back if not first block of text strings.
        /// If the symbol satisfies behindChar or if it started a string, returning empty successful match,
        /// otherwise it returns failure.
        /// </summary>
		/// <param name="behindChar">F-I identify the correct symbol to us.</param>
		/// <returns>Success or failure of a blank line.</returns>
		public static LpsParser Lookbehind(Func<char, bool> behindChar)
		{
            return new(text => text.Index <= 0 || behindChar(text[-1]) ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));
		}

        /// <summary>
        /// Parser that analyzes the character underneath the block of text (lookahead).
        /// Ie he looks at the character back if not first block of text strings.
        /// If the character is behindChar or if it started a string, returning empty successful match,
        /// otherwise it returns failure.
        /// </summary>
        /// <param name="behindChar">The required character.</param>
        /// <returns>Success or failure of a blank line.</returns>
        public static LpsParser Lookbehind(char behindChar)
        {
            return new(text => text.Index <= 0 || behindChar == text[-1] ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));
        }

        /// <summary>
        /// Parser that parses the next character but does not capture it, ie returns an empty successful compliance
        /// if the character satisfies or if we end the text, otherwise returns fail.
        /// </summary>
        /// <param name="aheadChar">F-I identify the correct symbol to us.</param>
        /// <returns>Success or failure of a blank line.</returns>
        public static LpsParser Lookahead(Func<char, bool> aheadChar)
        {
            return new(text => text.Length <= 0 || aheadChar(text[0]) ? new LpNode(new LpText(text.Source, text.Index, 0), text) : new LpNode(text));
        }

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
        /// Modifies the parser, adding to it ahead and check behind.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="behind">Checking character back.</param>
        /// <param name="ahead">Checking character forward.</param>
        /// <returns>Modified parser.</returns>
        public static LpsParser Look(this LpsParser parser, Func<char, bool> behind = null, Func<char, bool> ahead = null)
        {
            if (ahead != null && behind != null) return new LpsParser(id: parser.Identifier, wrapNode: parser.WrapNode, recurse: parser.Recurse, parser: t =>
            {
                if (!(t.Index <= 0 || behind(t[-1])))
                    return new LpNode(t);
                var res = parser.Parser(t);
                if (res.Match.Length < 0)
                    return res;
                return res.Rest.Length <= 0 || ahead(res.Rest[0]) ? res : new LpNode(t);
            });

            if (behind != null) return new LpsParser(id: parser.Identifier, wrapNode: parser.WrapNode, recurse: parser.Recurse, parser: t =>
            {
                if (!(t.Index <= 0 || behind(t[-1])))
                    return new LpNode(t);
                return parser.Parser(t);
            });

            if (ahead != null) return new LpsParser(id: parser.Identifier, wrapNode: parser.WrapNode, recurse: parser.Recurse, parser: t =>
            {
                var res = parser.Parser(t);
                if (res.Match.Length < 0)
                    return res;
                return res.Rest.Length <= 0 || ahead(res.Rest[0]) ? res : new LpNode(t);
            });

            throw new ArgumentNullException(nameof(behind));
        }

		/// <summary>
		/// Parser with the condition. Allows you to choose the strategy analysis, after checking the condition.
		/// </summary>
		/// <param name="condition">condition.</param>
		/// <param name="ifTrue">If the condition is true.</param>
		/// <param name="ifFalse">If the condition is false..</param>
		/// <returns>S parser condition.</returns>
		public static LpsParser If(char condition, LpsParser ifTrue, LpsParser ifFalse)
		{
			return new(t =>
			{
				if (t.Length <= 0)
                    return new LpNode(t);
				return t[0] == condition ? ifTrue.Do(t) : ifFalse.Do(t);
			});
		}

		/// <summary>
		/// Parser with the condition. Allows you to choose the strategy analysis, after checking the condition.
		/// </summary>
		/// <param name="condition">condition.</param>
		/// <param name="ifTrue">If the condition is true.</param>
		/// <param name="ifFalse">If the condition is false..</param>
		/// <returns>S parser condition.</returns>
		public static LpsParser If(Func<char, bool> condition, LpsParser ifTrue, LpsParser ifFalse)
		{
			return new(t =>
			{
				if (t.Length <= 0)
                    return new LpNode(t);
				return condition(t[0]) ? ifTrue.Do(t) : ifFalse.Do(t);
			});
		}

        /// <summary>
        /// Parser with the condition. Allows you to choose the strategy analysis, after checking the condition.
        /// </summary>
        /// <param name="condition">condition.</param>
        /// <param name="ifTrue">If the condition is true.</param>
        /// <param name="ifFalse">If the condition is false..</param>
        /// <returns>S parser condition.</returns>
        public static LpsParser If(LpsParser condition, LpsParser ifTrue, LpsParser ifFalse)
        {
            return new(t => condition.Do(t).Success ? ifTrue.Do(t) : ifFalse.Do(t));
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
		/// Successful returns the minimum length that is unable to recognize the parser from some index to the end of the text block.
		/// example: new LpText("12345").EndsWith(Lp.Digits()).Match == "5";
		/// </summary>
		/// <param name="text">A block of text (string).</param>
		/// <param name="parser">parser.</param>
		/// <returns>Successful compliance without reserve or line failure.</returns>
		public static LpNode EndsWith(this LpText text, LpsParser parser)
		{
			var initialLength = text.Length;
			var currentLength = 0;

			while (++currentLength <= initialLength)
			{
				var res = parser.Do(new LpText(text.Source, text.Index + initialLength - currentLength, currentLength));
				if (res.Success)
					return res;
			}
            return new LpNode(text);
		}

        /// <summary>
        /// Memorization. You rarely helps accelerate parsing simple designs innogda but it still, it accelerates significantly.
        /// A typical parser: (a | a + b | a + b + c | a + b + c + d), where a - portion which is three times again calculated and is therefore suitable for memorizatsii.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="storage">Results Repository.</param>
        /// <param name="sync">Synchronizing access to a dictionary. Synchronization objects - himself dictionary.</param>
        /// <returns>parser.</returns>
        public static LpsParser Mem(this LpsChain parser, IDictionary<LpText, LpMemNode> storage = null, bool sync = true)
        {
            return Mem(parser.ToParser(), storage);
        }

        /// <summary>
        /// Memorizatsiya. You rarely helps accelerate parsing simple designs innogda but it still, it accelerates significantly.
        /// A typical parser: (a | a + b | a + b + c | a + b + c + d), where a - portion which is three times again calculated and is therefore suitable for memorizatsii.
        /// </summary>
        /// <param name="parser">parser.</param>
        /// <param name="storage">Results Repository.</param>
        /// <returns>parser.</returns>
        private static LpsParser Mem(this LpsParser parser, IDictionary<LpText, LpMemNode> storage = null)
		{
			return new(id: null, wrapNode: false, parser: Mem(parser.Do, storage));
		}


		/// <summary>
		/// Memorizaciâ.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <param name="storage">Results Repository.</param>
        /// <param name="sync">Synchronizing access to a dictionary. Synchronization objects - himself dictionary.</param>
		/// <returns>parser.</returns>
        private static Func<LpText, LpNode> Mem(this Func<LpText, LpNode> parser, IDictionary<LpText, LpMemNode> storage, bool sync = true)
		{
			if (storage == null)
                storage = new Dictionary<LpText, LpMemNode>(0x31);

            if (sync)
            {
                return t =>
                {
                    lock (storage)
                    {
                        if (storage.TryGetValue(t, out var memRes))
                        {
                            ++memRes.Count;
                            return memRes.Node;
                        }
                    }
                    var prevNodes = -1;
                    lock (storage)
                    {
                        prevNodes = storage.Count;
                    }
                    var resultNode = parser(t);

                    lock (storage)
                    {
                        if (prevNodes == storage.Count)   // If Count is not changed, then the dictionary no one touched and re ContainsKey can not verify.
                            storage.Add(t, new LpMemNode(resultNode));
                        else if (!storage.ContainsKey(t)) // Double check because of possible recursion.
                            storage.Add(t, new LpMemNode(resultNode));
                    }
                    return resultNode;
                };
            }
			return t =>
			{
                if (storage.TryGetValue(t, out var memRes))
                {
                    ++memRes.Count;
                    return memRes.Node;
                }

                var prevNodes = storage.Count;
				var resultNode = parser(t);

                if (prevNodes == storage.Count)   // If Count is not changed, then the dictionary no one touched and re ContainsKey can not verify.
                    storage.Add(t, new LpMemNode(resultNode));
                else if (!storage.ContainsKey(t)) // Double check because of possible recursion.
                    storage.Add(t, new LpMemNode(resultNode));
                return resultNode;
			};
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
        /// </summary>
		/// <param name="listItem">Item.</param>
		/// <param name="delimiter">separator.</param>
		/// <returns>greedy parser.</returns>
        public static LpsParser List(LpsParser listItem, char delimiter)
		{
			return new(text =>
			{
				var last = listItem.Do(text);
				if (!last.Success)
					return last;

                var children = new List<LpNode>(0x10);
				children.Add(last);

				while (last.Success)
				{
					var rest = last.Rest;
					if (!(rest.Length > 0 && rest[0] == delimiter))
						break;
					var delim = new LpNode(rest, 1);
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
					return last;

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
					return last;

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


        /// <summary>
        /// This function modifies the parser body, borrowing from it all the properties and child nodes, child nodes still adds
        /// possible tails left and right.
        /// </summary>
		/// <param name="body">The main body.</param>
		/// <param name="maybeLeft">Possible tail left, for example Lp.Char('{').</param>
		/// <param name="maybeRight">Possible tail right, for example Lp.Char('}').</param>
		/// <returns>greedy parser.</returns>
        public static LpsParser MaybeTails(this LpsChain body, LpsParser maybeLeft, LpsParser maybeRight)
        {
            return body.ToParser().MaybeTails(maybeLeft, maybeRight);
        }

		/// <summary>
        /// This function modifies the parser body, borrowing from it all the properties and child nodes, child nodes still adds
        /// possible tails left and right.
        /// </summary>
		/// <param name="body">The main body.</param>
		/// <param name="maybeLeft">Possible tail left, for example Lp.Char('{').</param>
		/// <param name="maybeRight">Possible tail right, for example Lp.Char('}').</param>
		/// <returns>greedy parser.</returns>
		public static LpsParser MaybeTails(this LpsParser body, LpsParser maybeLeft, LpsParser maybeRight)
		{
			return new(id: body.Identifier, wrapNode: body.WrapNode, recurse: body.Recurse, parser: text =>
			{
                var children = new List<LpNode>(0x10);

				LpNode center = null;
				var last = maybeLeft.Do(text);
				if (last.Match.Length > 0)
				{
					children.Add(last);
					center = body.Parser(last.Rest);
				}
				else
				{
                    center = body.Parser(text);
				}
				if (center.Match.Length < 0)
					return center;

				// Add child nodes center.
                children.AddChildrenOrNodeOrNothing(center);

				last = maybeRight.Do(center.Rest);
				if (last.Match.Length > 0)
					children.Add(last);
				else
					last = center;

                return new LpNode(text, last.Rest.Index - text.Index, center.Id, children);
			});
		}

		/// <summary>
        /// Parser to parse the string of binary operators and operands. For example, one +2-3/9.
        /// Minimum construction for parsing: a + b, ie right operand is required.
        /// </summary>
		/// <param name="leftOperand">Left or the first operand.</param>
		/// <param name="binOperator">binary operator.</param>
		/// <param name="rightOperand">Right and / or subsequent operands.</param>
		/// <param name="spacesBetween">Parser gaps between the operand and the operator, for example Lp.ZeroOrMore (''). By default, missing spaces, tabs, and proceeds to the next. line.</param>
		/// <returns>parser.</returns>
		public static LpsParser BinaryExpression(LpsParser leftOperand, LpsParser binOperator, LpsParser rightOperand, LpsParser spacesBetween = null)
		{
			if (spacesBetween == null)
				spacesBetween = Lp.ZeroOrMore(c => c == ' ' || c == '\t' || c == '\r' || c == '\n');

			return new LpsParser(text =>
			{

                var children = new List<LpNode>(0x10);
				var left = leftOperand.Do(text); // First or the left operand.
				if (left.Match.Length < 0)
					return left;
				children.Add(left);

				var lSpaces = spacesBetween.Do(left.Rest); // gaps Left
				if (lSpaces.Match.Length > 0)
					children.Add(lSpaces);
                else if (lSpaces.Match.Length < 0)
                    return lSpaces;

				var oper = binOperator.Do(lSpaces.Rest); //operator
				if (oper.Match.Length < 0)
					return oper;
				children.Add(oper);

                var rSpaces = spacesBetween.Do(oper.Rest); // gaps deal
                if (rSpaces.Match.Length > 0)
                    children.Add(rSpaces);
                else if (rSpaces.Match.Length < 0)
                    return rSpaces;

                var right = rightOperand.Do(rSpaces.Rest); // Right operand.
				if (right.Match.Length < 0)
					return right;

				// Further repeating tail (operator-operand).
				var last = right;
				while (true)
				{
					children.Add(last);
                    lSpaces = spacesBetween.Do(last.Rest); // gaps Left
                    if (lSpaces.Match.Length < 0)
                        break;

					var nextOper = binOperator.Do(lSpaces.Rest);
					if (nextOper.Match.Length < 0)
						break;

					rSpaces = spacesBetween.Do(nextOper.Rest);
                    if (rSpaces.Match.Length < 0)
                        break;

                    var nextRight = rightOperand.Do(rSpaces.Rest);
					if (nextRight.Match.Length < 0)
						break;

                    if (lSpaces.Match.Length > 0)
                        children.Add(lSpaces);
                    children.Add(nextOper);
                    if (rSpaces.Match.Length > 0)
                        children.Add(rSpaces);

					last = nextRight;
				}
                return new LpNode(text, last.Rest.Index - text.Index, null, children);
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
				foreach (var r in nextParser.Do(l.Rest))
					yield return LpNode.Concat(l, r);
		}

		/// <summary>
        /// Returns a result set with the result of the concatenation prevResults parsing nextParser.
        /// Unlike Next, this function filters to prevent empty results looping quantifier for
        /// can not connect to an empty move forward.
        /// </summary>
		/// <param name="prevResults">Options analysis in the previous step.</param>
		/// <param name="nextParser">Parsing options in the next step.</param>
		/// <returns>concatenation results.</returns>
        private static IEnumerable<LpNode> NextSelf(this IEnumerable<LpNode> prevResults, LpmParser nextParser)
		{
			foreach (var left in prevResults)
			{
				// If left.Match empty, then continue to move with the same parser, we can not.
				// If no residue left.Rest, then continue to move at all does not make sense, because everything has already analyzed.
				if (left.Match.Length <= 0 || left.Rest.Length <= 0)
					continue;

				foreach (var right in nextParser.Do(left.Rest))
				{
					if (right.Match.Length > 0)
						yield return LpNode.Concat(left, right);
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
		private class LpNodeMatchComparer : IEqualityComparer<LpNode>
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

		/// <summary>
        /// Removes empty repetitions of correspondences from the list of options parsing.
        /// This feature works quickly.
        /// </summary>
		/// <param name="variants">Distinct parsed.</param>
		/// <returns>Purified from duplicate sequence.</returns>
		public static IEnumerable<LpNode> DistinctVoids(this IEnumerable<LpNode> variants)
		{
			var first = true;
			foreach (var v in variants)
			{
				if (v.Match.Length == 0)
				{
					if (first)
					{
						first = false;
						yield return v;
					}
				}
				else
				{
					yield return v;
				}
			}
		}

		#endregion Lpm special functions

		#region OneOrMore

		internal static IEnumerable<LpNode> OneOrMore_(this LpsParser parser, LpText text)
		{
			var left = parser.Do(text);
			while (left.Success)
			{
				yield return left;

				// Protection stalled.
				// Lpm for this check is in the function NextSelf.
				if (left.Match.Length <= 0 || left.Rest.Length <= 0)
					break;

				var right = parser.Do(left.Rest);
				if (!right.Success)
					break;
				left = LpNode.Concat(left, right);
			}
		}

        private static IEnumerable<LpNode> OneOrMore_(this LpmParser parser, LpText text)
		{
			var results = parser.Do(text);
			while (true)
			{
				// Replays must always clean because the parsing in adverse outcomes can grow exponentially.
				results = results.DistinctMatches();

				var empty = true;
				foreach (var oneResult in results)
				{
					empty = false;
					yield return oneResult;
				}
				if (empty)
					yield break;

				results = results.NextSelf(parser);
			}
		}

		/// <summary>
		/// Quantifier. One or more times.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <returns>Multiparser.</returns>
        private static LpmParser OneOrMore_(this LpsParser parser)
		{
			return new(p => OneOrMore_(parser, p));
		}

        /// <summary>
        /// Quantifier. One or more times.
        /// </summary>
        /// <param name="predicate">Predicate - the membership function of a given set of characters.</param>
        /// <returns>Multiparser.</returns>
        public static LpmParser OneOrMore_(this Func<char, bool> predicate)
        {
            return OneOrMore_(Lp.One(predicate));
        }

		/// <summary>
		/// Quantifier. One or more times.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser OneOrMore_(this LpmParser parser)
		{
			return new(p => OneOrMore_(parser, p).DistinctMatches());
		}

		/// <summary>
        /// Consistently applied to the text parser, passing parser residue from the previous result, until
        /// yet recognized text. Returns all matching results. Found between compliance can not be
        /// discontinuities.
        /// </summary>
		/// <param name="parser">parser.</param>
		/// <param name="text">text Block.</param>
		/// <returns>Search results.</returns>
        private static IEnumerable<LpNode> Chain(this LpsParser parser, LpText text)
		{
			var res = parser.Do(text);
			while (res.Success)
			{
				yield return res;
				if (res.Match.Length == 0 || res.Rest.Length <= 0)
					break;
				res = parser.Do(res.Rest);
			}
		}



        /// <summary>
        /// Designer parser chain of one or more elements.
        /// Returns a parser that consistently applies to the text parser, passing parser residue from the previous result, as
        /// long as the recognized text. Returns and unites all found matching one node. Found between compliance can not be
        /// discontinuities. All results are also stored in conformity Children property node.
        /// </summary>
        /// <param name="parser">parser.</param>
		/// <returns>parser.</returns>
        public static LpsParser OneOrMore(this LpsChain parser)
        {
            return Lp.OneOrMore((LpCover<LpsParser, LpNode>)parser.ToParser());
        }

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
					return next;

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

		/// <summary>
		/// Slightly faster analogue combination parser.Maybe().TakeMax().
		/// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <returns>Optimized parser.</returns>
		public static LpsParser TakeMaxOrEmpty(this LpmParser parser)
		{
			return new(text =>
			{
				var res = parser.Do(text);
				var node = Max(res);
                return node != null ? node : new LpNode(new LpText(text.Source, text.Index, 0), text);
			});
		}

		/// <summary>
		/// Combinatorial. zero or one different.
		/// </summary>
		/// <param name="charPredicate">Parser odnogo Symbol - predicate.</param>
		/// <param name="text">Text.</param>
		/// <returns>All matching options.</returns>
        private static IEnumerable<LpNode> Maybe_(Func<char, bool> charPredicate, LpText text)
		{
            yield return new LpNode(new LpText(text.Source, text.Index, 0), text);
			if (text.Length > 0 && charPredicate(text[0]))
                yield return new LpNode(text, 1);
		}

		/// <summary>
		/// Combinatorial. zero or one different.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <param name="text">Text.</param>
		/// <returns>All matching options.</returns>
		internal static IEnumerable<LpNode> Maybe_(LpsParser parser, LpText text)
		{
            yield return new LpNode(new LpText(text.Source, text.Index, 0), text);
			var res = parser.Do(text);
			if (res.Success)
				yield return res;
		}

		/// <summary>
		/// Combinatorial. zero or one different.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <returns>All matching options.</returns>
		public static LpmParser Maybe_(this LpsParser parser)
		{
			return new(p => Maybe_(parser, p));
		}

		/// <summary>
		/// Combinatorial. zero or one different.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <returns>All matching options.</returns>
		public static LpmParser Maybe_(this LpmParser parser)
		{
			return new(p => Maybe(parser, p));
		}


		/// <summary>
		/// Combinatorial. zero or one different.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <param name="text">Text.</param>
		/// <returns>All matching options.</returns>
        private static IEnumerable<LpNode> Maybe(this LpmParser parser, LpText text)
		{
            yield return new LpNode(new LpText(text.Source, text.Index, 0), text);
			foreach (var v in parser.Do(text))
				yield return v;
		}

		/// <summary>
		/// Parser one character, if any.
		/// </summary>
		/// <param name="ch">symbol.</param>
		/// <returns>parser.</returns>
		public static LpsParser Maybe(char ch)
		{
            return new(t => t.Length > 0 && t[0] == ch ? new LpNode(t, 1) : new LpNode(new LpText(t.Source, t.Index, 0), t));
		}

        /// <summary>
        /// Parser one character, if any.
        /// </summary>
        /// <param name="ch">Function identification symbol.</param>
        /// <returns>parser.</returns>
        public static LpsParser Maybe(this Func<char, bool> ch)
        {
            return new(t => t.Length > 0 && ch(t[0]) ? new LpNode(t, 1) : new LpNode(new LpText(t.Source, t.Index, 0), t));
        }


		/// <summary>
		/// Parser symbol with two outcomes (empty symbol).
		/// </summary>
		/// <param name="ch">symbol.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser Maybe_(char ch)
		{
			return new(p => Maybe_(c => c == ch, p));
		}


		/// <summary>
		/// Parser term, which may or may not be.
		/// </summary>
		/// <param name="term">Some word.</param>
		/// <returns>parser.</returns>
		public static LpsParser Maybe(string term)
		{
            return new(text => text.StartsWith(term) ? new LpNode(text, term.Length) : new LpNode(new LpText(text.Source, text.Index, 0), text));
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
        public static LpsParser NextZeroOrMore(this LpsChain firstItem, LpUncover<LpsParser, LpNode> maybeNextItems)
        {
            return firstItem.ToParser().NextZeroOrMore(maybeNextItems);
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




		/*
		/// <summary>
		/// Implements the template (first + Maybenext{0,1}).
		/// Very similar to the combiner NextZeroOrMore, no otličaetsâ thereof only tem cto instead of {0} worth {0,1}.
		/// </summary>
		/// <param name="first">Parser first element.</param>
		/// <param name="maybeNext">Parser subsequent elements.</param>
		/// <returns>parser.</returns>
		[Obsolete("Protests add unit tests.")]
		public static LpsParser MaybeNext(this LpUncover<LpsParser, LpNode> first, LpUncover<LpsParser, LpNode> maybeNext)
		{
		    if (!first.Uncover && !maybeNext.Uncover) return new LpsParser(id: null, wrapNode: true, parser: (text) =>
		    {
		        var nFirst = first.Parser.Do(text);
		        if (!nFirst.Success)
		            return nFirst;
		        var nNext = maybeNext.Parser.Do(nFirst.Rest);
		        if (nNext.Match.Length <= 0)
		            return nFirst;
		        return nFirst.Match.Length <= 0 ? nNext : new LpNode(text, nNext.Rest.Index - text.Index, null, nFirst, nNext);
		    });


		    // maybeNext.Uncover
		    if (maybeNext.Uncover) return new LpsParser(id: null, wrapNode: true, parser: (text) =>
		    {
		        var next = first.Parser.Do(text);
		        if (!next.Success)
		            return next;

		        var list = new List<LpNode>(0x10);
		        if (first.Uncover)
		            list.AddChildrenOrNodeOrNothing(next);
		        else if (next.Match.Length > 0)
		            list.Add(next);

		        next = maybeNext.Parser.Do(next.Rest);
		        if (next.Match.Length > 0)
		            list.AddChildrenOrNodeOrNothing(next);

		        if (list.Count <= 1)
		            return list.Count == 0 ? next : list[0];
		        return new LpNode(text, next.Rest.Index - text.Index, null, list);
		    });


		    return new LpsParser(id: null, wrapNode: true, parser: (text) =>
		    {
		        var next = first.Parser.Do(text);
		        if (!next.Success)
		            return next;

		        var list = new List<LpNode>(0x10);
		        if (first.Uncover)
		            list.AddChildrenOrNodeOrNothing(next);
		        else if (next.Match.Length > 0)
		            list.Add(next);

		        next = maybeNext.Parser.Do(next.Rest);
		        if (next.Match.Length > 0)
		            list.Add(next);

		        if (list.Count <= 1)
		            return list.Count == 0 ? next : list[0];
		        return new LpNode(text, next.Rest.Index - text.Index, null, list);
		    });
		}
		 */

		/*
		/// <summary>
		/// implements template  (first + maybeNext{0,1}).
		/// Very similar to the combinatorial NextZeroOrMore, but the only difference is that instead of {0} worth {0,1}.
		/// </summary>
		/// <param name="first">Parser first element.</param>
		/// <param name="maybeNext">Parser subsequent elements.</param>
		/// <returns>parser.</returns>
		public static LpsParser MaybeNext(this LpsChain first, LpUncover<LpsParser, LpNode> maybeNext)
		{
		    return MaybeNext((LpUncover<LpsParser, LpNode>)first.ToParser(), maybeNext);
		}
		*/

		/*
        /// <summary>
        ///implements template(first + maybeNext{0,1}).
        /// Very similar to the combinatorial NextZeroOrMore, but the only difference is that instead {0,} worth {0,1}.
        /// </summary>
        /// <param name="first">Parser first element.</param>
        /// <param name="maybeNext">Parser subsequent elements.</param>
        /// <returns>parser.</returns>
        public static LpsParser MaybeNext(this LpsParser first, LpUncover<LpsParser, LpNode> maybeNext)
        {
            return MaybeNext((LpUncover<LpsParser, LpNode>)first, maybeNext);
        }
        */

		/*
		/// <summary>
		/// implements template (first + maybeNext{0,1}).
		/// Very similar to the combinatorial NextZeroOrMore, but the only difference is that instead {0,} worth {0,1}.
		/// </summary>
		/// <param name="first">Parser first element.</param>
		/// <param name="maybeNext">Parser subsequent elements.</param>
		/// <returns>parser.</returns>
		public static LpsParser MaybeNext(this LpsParser first, LpsChain maybeNext)
		{
		    return MaybeNext((LpUncover<LpsParser, LpNode>)first, maybeNext.ToParser());
		}
		*/

		/// <summary>
		/// Function to add a node to the list.
		/// If the node has children, instead of adding children nodes. In other cases, the node is added.
		/// This function is used when the option to uncover some specific functions, such MaybeNext.
		/// </summary>
		/// <param name="list">list.</param>
		/// <param name="node">node.</param>
		public static void AddChildrenOrNodeOrNothing(this LinkedList<LpNode> list, LpNode node)
		{
            if (node.Match.Length <= 0)
                return;

			if (node.Children == null)
			{
				list.AddLast(node);
				return;
			}

			using var children = node.Children.GetEnumerator();
			if (!children.MoveNext())
			{
				list.AddLast(node);
				return;
			}
			do
			{
				list.AddLast(children.Current);
			}
			while (children.MoveNext());
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


		internal static IEnumerable<LpNode> ZeroOrMore(LpsParser parser, LpText text)
		{
            yield return new LpNode(new LpText(text.Source, text.Index, 0), text);

			var left = parser.Do(text);
			while (left.Success)
			{
				yield return left;

				if (left.Match.Length <= 0 || left.Rest.Length <= 0)
					break;

				var right = parser.Do(left.Rest);
				if (!right.Success)
					break;
				left = LpNode.Concat(left, right);
			}
		}

		/// <summary>
		/// Combinatorial. zero or more times.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <returns>Multiparser that returns all matching options.</returns>
        private static LpmParser ZeroOrMore_(this LpsParser parser)
		{
			return new(p => ZeroOrMore(parser, p));
		}

        /// <summary>
        /// Combinatorial. zero or more times.
        /// </summary>
        /// <param name="predicate">Predicate - the membership function of a given set of characters.</param>
        /// <returns>Multiparser that returns all matching options.</returns>
        public static LpmParser ZeroOrMore_(this Func<char, bool> predicate)
        {
            return ZeroOrMore_(Lp.One(predicate));
        }


		/// <summary>
		/// Combinatorial. zero or more times.
		/// </summary>
		/// <param name="parser">parser.</param>
		/// <param name="text">Text.</param>
		/// <returns>All matching options.</returns>
        private static IEnumerable<LpNode> ZeroOrMore_(LpmParser parser, LpText text)
		{
			// Zero
            yield return new LpNode(new LpText(text.Source, text.Index, 0), text);

			// OneOrMore
			var results = parser.Do(text);
			while (true)
			{

				var empty = true;

				// Replays must always clean because the parsing in adverse outcomes can grow exponentially.
				results = results.DistinctMatches();

				foreach (var oneResult in results)
				{
					if (oneResult.Match.Length <= 0) // Zero already was.
						continue;

					empty = false;
					yield return oneResult;
				}
				if (empty)
					yield break;

				results = results.NextSelf(parser);
			}
		}

		/// <summary>
		/// Combinatorial. zero or more times.
		/// </summary>
		/// <param name="parser">Multiparser.</param>
		/// <returns>All matching options.</returns>
		public static LpmParser ZeroOrMore_(this LpmParser parser)
		{
			return new(p => ZeroOrMore_(parser, p).DistinctMatches());
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
					return new LpNode(new LpText(text.Source, text.Index, 0), text);

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
                    return new LpNode(new LpText(text.Source, text.Index, 0), text);

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
}
