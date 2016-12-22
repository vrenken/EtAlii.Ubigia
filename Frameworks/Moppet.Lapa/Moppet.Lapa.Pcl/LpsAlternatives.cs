﻿////////////////////////////////////////////////////////////////////////////////////////////////
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
    // Attention can not be in this class to use the implicit cast to LpsParser! 
    // This leads to the fact that the reduction manifests itself not where it should.
    //

	/// <summary>
    /// List of alternatives. This class simplifies the combination of parsers. For example, take a combination of parsers 
    /// p1 | p2 | p3 | p4, if all 4th parsers are type LpsParser, then it will be 
    /// formed a list of alternatives which will now be combined into a single optimum parser and thus will 
    /// selected first successful alternative.
    /// </summary>
	public sealed class LpsAlternatives : LpParserAttrs<LpsAlternatives>
	{
		/// <summary>
        /// List parsers.
		/// </summary>
		List<LpsParser> m_parsers = new List<LpsParser>();

		/// <summary>
		/// The default constructor.
		/// </summary>
		public LpsAlternatives()
		{
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="left">Left chain.</param>
		/// <param name="right">Right chain.</param>
		public LpsAlternatives(IEnumerable<LpsParser> left, IEnumerable<LpsParser> right)
		{
            if (left.Any(l => l == null))
                throw new ArgumentNullException("left", "One of item in arguments is null.");
            if (right.Any(r => r == null))
                throw new ArgumentNullException("right", "One of item in arguments is null.");

            m_parsers = new List<LpsParser>(left.Count() + right.Count());
            m_parsers.AddRange(left);
            m_parsers.AddRange(right);
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="parsers">Parser.</param>
		public LpsAlternatives(params LpsParser[] parsers)
		{
			m_parsers = new List<LpsParser>(parsers.Length);
			foreach (var p in parsers)
			{
				if (p == null)
					throw new ArgumentNullException("parsers", "One of item in arguments is null.");
				m_parsers.Add(p);
			}
		}

		/// <summary>
		/// A copy of the object.
		/// </summary>
		/// <returns>Branch.</returns>
		public override LpsAlternatives Copy()
		{
			var c = base.Copy();
			c.m_parsers = new List<LpsParser>(m_parsers.Count + 1);
			c.m_parsers.AddRange(m_parsers);
			return c;
		}


		/// <summary>
        /// Helper function to convert string parser select the first successful alternative. 
        /// From the order of the parsers (alternatives) depends on the speed.
        /// </summary>
		/// <returns>parser.</returns>
		public LpsParser TakeFirst()
		{
			return TakeFirst(Identifier, m_parsers);
		}

		/// <summary>
        /// Schemer who chooses the first successful alternative (the result of the parser), which is performed for the postcondition behind. 
        /// This function is particularly advantageous if the alternatives are sorted in order of decreasing probability 
        /// their appearance.
        /// </summary>
		/// <param name="ifBehind">Postcondition.</param>
		/// <returns>The resulting parser.</returns>
		public LpsParser TakeFirst(LpsParser ifBehind)
		{
			return TakeFirst(Identifier, m_parsers, ifBehind);
		}

		/// <summary>
        /// Helper function to convert string parser that selects the longest match - greedy algorithm.
		/// </summary>
		/// <returns>parser.</returns>
		public LpsParser TakeMax()
		{
			var copyParsers = m_parsers.ToArray();
            return new LpsParser(Identifier, (text) => All(text, copyParsers).Max() ?? new LpNode(Identifier, text));
		}

		/// <summary>
        /// Helper function to convert string parser select all alternatives.
		/// </summary>
		/// <returns>parser.</returns>
		public LpmParser TakeAll()
		{
			var copyParsers = m_parsers.ToArray();
			return new LpmParser(Identifier, (text) => All(text, copyParsers).DistinctMatches()); // DistinctVoids
		}


		/// <summary>
        /// An implicit typecast in multiparser.
		/// </summary>
		/// <param name="parsers">a list of alternatives.</param>
		/// <returns>parser.</returns>
		public static implicit operator Func<LpText, IEnumerable<LpNode>>(LpsAlternatives parsers)
		{
			return parsers.TakeAll().Parser;
		}
		

		/// <summary>
		/// An implicit cast.
		/// </summary>
		/// <param name="parsers">a list of alternatives.</param>
		/// <returns>parser.</returns>
		public static implicit operator Func<LpText, LpNode>(LpsAlternatives parsers)
		{
			return parsers.TakeFirst().Parser;
		}

		/// <summary>
        /// Concatenation operator parser to chain alternatives.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right odnorezultatny parser.</param>
		/// <returns>Chain parsers.</returns>
		public static LpsChain operator + (LpsAlternatives left, LpsParser right)
		{
			if (right == null)
				throw new ArgumentNullException("right");
			return left.TakeFirst() + right;
		}


		/// <summary>
		/// Operator sequential combination parsers.
		/// </summary>
		/// <param name="left">Parser.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsChain operator + (LpsParser left, LpsAlternatives right)
		{
			if (left == null)
				throw new ArgumentNullException("left");
			return left + right.TakeFirst();
		}


		/// <summary>
		/// Operator sequential combination parsers.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsAlternatives left, LpsAlternatives right)
		{
			return left.TakeFirst() + right.TakeFirst();
		}

		/// <summary>
		/// Operator sequential combination parsers.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">Search for a (following) characters.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsAlternatives left, char right)
		{
			return left.TakeFirst() + Lp.Char(right);
		}

		/// <summary>
		/// Operator sequential combination parsers.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">Search for a (next) word.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsAlternatives left, string right)
		{
			if (right == null)
				throw new ArgumentNullException("right");
			return left.TakeFirst() + Lp.Term(right);
		}

		/// <summary>
        /// The concatenation operator multiparsera to the chain alternatives.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">Right multiparser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator + (LpsAlternatives left, LpmParser right)
		{
			if (right == null)
				throw new ArgumentNullException("right");
			return left.TakeFirst() + right;
		}

		/// <summary>
        /// The concatenation operator chain alternatives to multiparser.
		/// </summary>
		/// <param name="left">Right multiparser.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator + (LpmParser left, LpsAlternatives right)
		{
			if (left == null)
				throw new ArgumentNullException("left");
			return left + right.TakeFirst();
		}

		/// <summary>
        /// The operator Or. Either string or multiparser.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
        /// <param name="right">Right multiparser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpsAlternatives left, LpmParser right)
		{
			if (right == null)
				throw new ArgumentNullException("right");
			return left.TakeFirst() | right;
		}

		/// <summary>
        /// The concatenation operator chains alternatives.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>Results in a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsAlternatives left, LpsAlternatives right)
		{
			return new LpsAlternatives(left.m_parsers, right.m_parsers);
		}

		/// <summary>
        /// The concatenation operator with a list of alternatives to chain parsers.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Results in a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsAlternatives left, LpsChain right)
		{
			return left.TakeFirst() | right.ToParser();
		}

		/// <summary>
        /// The concatenation operator parser to the list of alternatives.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
        /// <param name="ch">Parser one character.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsAlternatives left, char ch)
		{
			return new LpsAlternatives(left.m_parsers, new[] { Lp.Char(ch) });
		}

		/// <summary>
        /// The concatenation operator list of alternatives to the parser symbol.
		/// </summary>
        /// <param name="ch">Parser one character.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsAlternatives operator | (char ch, LpsAlternatives right)
		{
			return new LpsAlternatives(new[] { Lp.Char(ch) }, right.m_parsers);
		}

		/// <summary>
        /// The concatenation operator parser to the list of alternatives.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
        /// <param name="term">The parser is one word - term.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsAlternatives left, string term)
		{
			if (term == null)
				throw new ArgumentNullException("term");
			return new LpsAlternatives(left.m_parsers, new[] { Lp.Term(term) });
		}

		/// <summary>
        /// The concatenation operator parser to the list of alternatives.
		/// </summary>
		/// <param name="left">a list of alternatives.</param>
		/// <param name="right">Parser.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsAlternatives left, LpsParser right)
		{
			if (right == null)
				throw new ArgumentNullException("right");
			return new LpsAlternatives(left.m_parsers, new [] { right });
		}

		/// <summary>
        /// The concatenation operator list of alternatives to the parser.
		/// </summary>
		/// <param name="left">Parser.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsParser left, LpsAlternatives right)
		{
			return new LpsAlternatives(new[] { left }, right.m_parsers);
		}

		/// <summary>
        /// String concatenation operator with a list of alternatives parsers.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">a list of alternatives.</param>
		/// <returns>Results in a list of alternatives.</returns>
		public static LpsAlternatives operator | (LpsChain left, LpsAlternatives right)
		{
            // This bug was here
            // return left.ToParser() | right.TakeFirst();

            // Right behaviour
            return new LpsAlternatives(new[] { left.ToParser() }, right.m_parsers);
		}

		/// <summary>
        /// The operator Or. Either multiparser or chain.
		/// </summary>
        /// <param name="left">Left multiparser.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpmParser left, LpsAlternatives right)
		{
			return left | right.TakeFirst();
		}


		/// <summary>
        /// Combiner to select the first successful alternative.
		/// </summary>
        /// <param name="parsers">List parsers.</param>
		/// <returns>The resulting parser.</returns>
		public static LpsParser TakeFirst(IEnumerable<LpsParser> parsers)
		{
			return TakeFirst(null, parsers);
		}

		/// <summary>
        /// Combiner to select the first successful alternative.
		/// </summary>
        /// <param name="id">Result ID.</param>
        /// <param name="parsers">List parsers.</param>
		/// <returns>The resulting parser.</returns>
		public static LpsParser TakeFirst(string id, IEnumerable<LpsParser> parsers)
		{
			var parsersArray = parsers.ToArray();
			return new LpsParser(id, (text) =>
			{
				foreach (var next in parsersArray)
				{
					var res = next.Do(text);
                    if (res.Match.Length >= 0)
						return res;
				}
                return new LpNode(text);
			});
		}

		/// <summary>
		/// Schemer who chooses the first successful alternative for running the postcondition behind.
		/// This function is especially beneficial if an alternative sorted in order of decreasing probability
        /// their appearance.
        /// </summary>
        /// <param name="id">ID parser.</param>
        /// <param name="parsers">List parsers, which together represent parsing options.</param>
		/// <param name="behind">Postcondition.</param>
		/// <returns>The resulting parser.</returns>
		public static LpsParser TakeFirst(string id, IEnumerable<LpsParser> parsers, LpsParser behind)
		{
			var parsersArray = parsers.ToArray();
			return new LpsParser(id, (text) =>
			{
				foreach (var parser in parsersArray)
				{
					var res = parser.Do(text);
                    if (res.Match.Length >= 0)
					{
						if (behind.Do(res.Rest).Success)
							return res;
					}
				}
                return new LpNode(text);
			});
		}

		/// <summary>
        /// Combiner to select all the alternatives.
		/// </summary>
		/// <param name="text">text Block.</param>
        /// <param name="parsers">List parsers.</param>
		/// <returns>The resulting parser.</returns>
		private static IEnumerable<LpNode> All(LpText text, IEnumerable<LpsParser> parsers)
		{
			foreach (var next in parsers)
			{
				var res = next.Do(text);
				if (res.Match.Length >= 0)
					yield return res;
			}
		}
	}
}
