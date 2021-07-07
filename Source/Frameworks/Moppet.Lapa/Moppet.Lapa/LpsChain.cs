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
using System.Collections.Generic;
using System.Linq;

namespace Moppet.Lapa
{
	/// <summary>
	/// The sequence (chain) of simple parsers to their consistent application.
	/// This class simplifies the auto sequential combination parser. For example, take
	/// combination parsers p1 + p2 + p3 + p4, if all 4th parser are type LpsParser, then it will be
	/// It formed a chain, which will later be combined into one optimal parser, instead of a plurality of pairs of parsers.
    /// Note: none of the functions of this class do not change the state of an object class.
	/// </summary>
	public sealed class LpsChain : LpParserAttrs<LpsChain>
	{
		/// <summary>
		/// List of parsers.
		/// </summary>
		private List<LpsParser> _mParsers = new();

		/// <summary>
		/// The default constructor. An empty chain.
		/// </summary>
		public LpsChain()
		{
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="parsers">Parser.</param>
		public LpsChain(params LpsParser[] parsers)
		{
			_mParsers = new List<LpsParser>(parsers.Length); _mParsers.AddRange(parsers);
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="left">Left chain.</param>
		/// <param name="right">Right chain.</param>
        private LpsChain(ICollection<LpsParser> left, ICollection<LpsParser> right)
		{
			_mParsers = new List<LpsParser>(left.Count + right.Count);
			_mParsers.AddRange(left);
			_mParsers.AddRange(right);
		}

		/// <summary>
		/// A copy of the object.
		/// </summary>
		/// <returns>Branch.</returns>
		public override LpsChain Copy()
		{
			var c = base.Copy();
			c._mParsers = new List<LpsParser>(_mParsers.Count + 1);
			c._mParsers.AddRange(_mParsers);
			return c;
		}

		/// <summary>
		/// A helper function for converting a string to a parser.
		/// </summary>
		/// <returns>parser.</returns>
		public LpsParser ToParser()
		{
			if (_mParsers.Count <= 0)
				throw new Exception("Parsers chain is empty.");

			if (_mParsers.Count == 1)
				return _mParsers[0];

			if (_mParsers.Count > 2)
				return Concat(_mParsers, Identifier, WrapNode);

			var par1 = _mParsers[0]; // Copies are needed because the Parsers array can change.
			var par2 = _mParsers[1];

			return new LpsParser(Identifier, WrapNode, (text) =>
			{
				var left = par1.Do(text);
                if (left.Match.Length < 0)
                    return new LpNode(text);
				var right = par2.Do(left.Rest);
                if (right.Match.Length < 0)
                    return new LpNode(text); // You can't just return right here, since there will be an incorrect remainder.

                if (left.Match.Length == 0 && left.Id == null)
                    return right;
                if (right.Match.Length == 0 && right.Id == null)
                    return left;

				return LpNode.Concat(left, right);
			});
		}

		/// <summary>
		/// An implicit cast.
		/// </summary>
		/// <param name="parsers">Chain parsers.</param>
		/// <returns>parser.</returns>
		public static implicit operator LpsParser(LpsChain parsers)
		{
			return parsers.ToParser();
		}

		/// <summary>
		/// An implicit cast.
		/// </summary>
		/// <param name="parsers">Chain parsers.</param>
		/// <returns>parser.</returns>
		public static implicit operator Func<LpText, LpNode>(LpsChain parsers)
		{
			return parsers.ToParser().Parser;
		}

		/// <summary>
		/// Operator for concatenating a parser to a chain.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right odnorezultatny parser.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsChain left, LpsParser right)
		{
            if (left.Identifier != null)
                return new LpsChain(left.ToParser(), right);
			return new LpsChain(left._mParsers, new [] { right });
		}

		/// <summary>
		/// Concatenation operator for parser chains.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right odnorezultatny parser.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator +(LpsChain left, LpsChain right)
		{
            if (left.Identifier != null)
            {
                if (right.Identifier != null)
                    return new LpsChain(left, right);
                return new LpsChain(new [] { left.ToParser() }, right._mParsers);
            }
            else if (right.Identifier != null)
            {
                if (left.Identifier != null)
                    return new LpsChain(left, right);
                return new LpsChain(left._mParsers, new[] { right.ToParser() });
            }
            return new LpsChain(left._mParsers, right._mParsers);
		}

		/// <summary>
		/// Concatenation operator for parser chains.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Search for a (following) characters.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator +(LpsChain left, char right)
		{
            if (left.Identifier != null)
                return new LpsChain(left.ToParser(), Lp.Char(right));
			return new LpsChain(left._mParsers, new[] { Lp.Char(right) });
		}

        /// <summary>
        /// Concatenation operator for parser chains.
        /// </summary>
        /// <param name="left">Chain parsers.</param>
        /// <param name="right">Search for a (following) characters.</param>
        /// <returns>parser.</returns>
        public static LpsChain operator + (LpsChain left, Func<char, bool> right)
        {
            if (left.Identifier != null)
                return new LpsChain(left.ToParser(), Lp.One(right));
            return new LpsChain(left._mParsers, new[] { Lp.One(right) });
        }

		/// <summary>
		/// Concatenation operator for parser chains.
		/// </summary>
		/// <param name="left">Search for the first character.</param>
		/// <param name="right">Matching the next chain of parsers.</param>
		/// <returns>parser.</returns>
        public static LpsChain operator + (char left, LpsChain right)
        {
            if (right.Identifier != null)
                return new LpsChain(Lp.Char(left), right.ToParser());
            return new LpsChain(new[] { Lp.Char(left) },  right._mParsers);
        }

        /// <summary>
        /// Concatenation operator for parser chains.
        /// </summary>
        /// <param name="left">Search for the first character.</param>
        /// <param name="right">Matching the next chain of parsers.</param>
        /// <returns>parser.</returns>
        public static LpsChain operator + (Func<char, bool> left, LpsChain right)
        {
            if (right.Identifier != null)
                return new LpsChain(Lp.One(left), right.ToParser());
            return new LpsChain(new[] { Lp.One(left) }, right._mParsers);
        }


		/// <summary>
		/// Concatenation operator for parser chains.
		/// </summary>
		/// <param name="left">Search for the first term.</param>
		/// <param name="right">Matching the next chain of parsers.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (string left, LpsChain right)
		{
            if (right.Identifier != null)
                return new LpsChain(Lp.Term(left), right.ToParser());

            return new LpsChain(new[] { Lp.Term(left) }, right._mParsers);
		}

		/// <summary>
		/// Concatenation operator for parser chains.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Search for a (next) word.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsChain left, string right)
		{
            if (left.Identifier != null)
                return new LpsChain(left.ToParser(), Lp.Term(right));

			return new LpsChain(left._mParsers, new[] { Lp.Term(right) });
		}


		/// <summary>
		/// Or operator. Either chain or chain.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Multiparser.</returns>
		public static LpsAlternatives operator | (LpsChain left, LpsChain right)
		{
			return left.ToParser() | right.ToParser();
		}

		/// <summary>
		/// Or operator. Either a chain or a parser.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Parser.</param>
		/// <returns>A chain of two alternatives.</returns>
		public static LpsAlternatives operator | (LpsChain left, LpsParser right)
		{
			return left.ToParser() | right;
		}


		/// <summary>
		/// Or operator. Either chain or multi-parser.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right multiparser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpsChain left, LpmParser right)
		{
			return left.ToParser() | right;
		}

		/// <summary>
		/// Or operator. Either a multi-parser or a chain.
		/// </summary>
		/// <param name="left">Left multi-parser.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpmParser left, LpsChain right)
		{
			return left | right.ToParser();
		}

		/// <summary>
		/// Multiparser-to-string concatenation operator.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right odnorezultatny parser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator + (LpsChain left, LpmParser right)
		{
			return left.ToParser() + right;
		}


		/// <summary>
        /// Take Faster analogue combination parser.Maybe _ (). TakeMax ().
		/// </summary>
		/// <returns>Optimized parser.</returns>
		public LpsParser Maybe()
		{
			return ToParser().Maybe();
		}

        /// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
		/// <param name="parsersList">List of parsers.</param>
		/// <returns>The resulting parser.</returns>
        private static Func<LpText, LpNode> Concat(IEnumerable<LpsParser> parsersList)
		{
			var parsers = parsersList.ToArray();
			if (parsers.Length < 2)
				throw new ArgumentOutOfRangeException(nameof(parsersList));

			return text =>
			{
                var children = new List<LpNode>(0x10);
                var next = new LpNode(new LpText(text.Source, text.Index, 0), text);

                foreach (var parser in parsers)
                {
	                next = parser.Do(next.Rest);
	                var nextLen = next.Match.Length;
	                if (nextLen < 0)
		                return new LpNode(text);

	                if (nextLen > 0)
		                children.Add(next);
	                else if (next.Id != null)
		                children.Add(next);
                }
                if (children.Count == 0)
                    return new LpNode(text, next.Rest.Index - text.Index);
                return new LpNode(text, next.Rest.Index - text.Index, null, children);
			};
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
		/// <param name="parsersList">List of parsers.</param>
		/// <param name="id">ID.</param>
		/// <param name="wrapNode">wrapping result.</param>
		/// <returns>The resulting parser.</returns>
        private static LpsParser Concat(IEnumerable<LpsParser> parsersList, string id, bool wrapNode = false)
		{
			var func = Concat(parsersList);
			return new LpsParser(id: id, wrapNode: wrapNode, parser : func);
		}
	}
}
