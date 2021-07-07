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
    /// Parser that returns a lot of options analysis, ie generic parser
	/// Func&lt;LpText, IEnumerable&lt;LpNode&gt;&gt;^t;/LpNode&gt;.
    /// At the input text. In many embodiments the outlet parsing wherein each variant
    /// - A pair of {Part parsed text; Balance}.
    /// For example, in the line "1234" parsing embodiments may be "", "1", "12", "123", "1234".
	/// </summary>
	public sealed class LpmParser : LpBaseParser<IEnumerable<LpNode>, LpmParser>
	{
		/// <summary>
		/// The default constructor.
		/// </summary>
		public LpmParser()
		{
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
        /// <param name="parser">The actual function of parsing - parser.</param>
		public LpmParser(Func<LpText, IEnumerable<LpNode>> parser) { Parser = parser; }

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="id">ID.</param>
        /// <param name="parser">The actual function of parsing - parser.</param>
		public LpmParser(string id, Func<LpText, IEnumerable<LpNode>> parser)
		{
			Identifier = id;
			Parser = parser;
		}

		/// <summary>
        /// Function - the top-level parser.
		/// </summary>
		/// <param name="text">text Block.</param>
        /// <returns>The results of analysis.</returns>
		public IEnumerable<LpNode> Do(LpText text)
		{
			IEnumerable<LpNode> res;
			if (Stack != null)
			{
                //if (m_stack.FindLast(text) != null)
                //    return new LpNode[0];
                //var added = m_stack.AddLast(text);
                //res = m_parser(text);
                //m_stack.Remove(added);

                LinkedListNode<LpText> added;
                lock (Stack)
                {
                    if (Stack.FindLast(text) != null)
                    {
                        return Array.Empty<LpNode>();
                    }
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
				if (WrapNode)
					return WrapIdentifiers(res);
				return SetIdentifiers(res);
			}
			return res;
		}


		/// <summary>
        /// Wrap the parsed results.
		/// </summary>
		/// <param name="res">The results of analysis.</param>
		/// <returns>findings.</returns>
		private IEnumerable<LpNode> WrapIdentifiers(IEnumerable<LpNode> res)
		{
			foreach (var n in res)
			{
				if (n.Id != null)
					yield return new LpNode(Identifier, n.Match, n.Rest, n);
				n.Id = Identifier;
				yield return n;
			}
		}

		/// <summary>
        /// Sets (overwrites) the identifier on the results of analysis.
		/// </summary>
		/// <param name="res">The results of analysis.</param>
		/// <returns>findings.</returns>
		private IEnumerable<LpNode> SetIdentifiers(IEnumerable<LpNode> res)
		{
			foreach (var n in res)
			{
				n.Id = Identifier;
				yield return n;
			}
		}


		/// <summary>
		/// An implicit cast.
		/// </summary>
		/// <param name="p">parser.</param>
		/// <returns>object parser.</returns>
		public static implicit operator LpmParser(Func<LpText, IEnumerable<LpNode>> p)
		{
			return new(p);
		}

		/// <summary>
		/// An implicit cast.
		/// </summary>
		/// <param name="oParser">Parser object.</param>
		/// <returns>object parser.</returns>
		public static implicit operator Func<LpText, IEnumerable<LpNode>>(LpmParser oParser)
		{
			if (oParser.Identifier == null && oParser.Parser != null)
				return oParser.Parser;
			return oParser.Do;
		}

		#region And

		/// <summary>
        /// F-I sequential use parsers.
		/// </summary>
        /// <param name="left">Left parser.</param>
        /// <param name="right">Right parser.</param>
        /// <param name="p">The analyzed text.</param>
		/// <returns>The results of analysis.</returns>
		private static IEnumerable<LpNode> And(LpmParser left, LpsParser right, LpText p)
		{
			var leftResults = left.Do(p);
			foreach (var l in leftResults)
			{
				var r = right.Do(l.Rest);
				if (r.Success)
					yield return LpNode.Concat(l, r);
			}
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
        /// <param name="left">Left parser.</param>
        /// <param name="right">Right parser.</param>
		/// <returns>The resulting parser.</returns>
		public static LpmParser operator +(LpmParser left, LpsParser right)
		{
			return new(p => And(left, right, p));
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
        /// <param name="left">Left parser.</param>
        /// <param name="right">Right parser.</param>
		/// <returns>The resulting parser.</returns>
		public static LpmParser operator +(LpsParser left, LpmParser right)
		{
			return new(p =>
			{
				var prevResult = left.Do(p);
				if (!prevResult.Success)
					return Array.Empty<LpNode>();
				return right.Do(prevResult.Rest).Select(next => LpNode.Concat(prevResult, next));
			});
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
        /// <param name="left">Left parser.</param>
        /// <param name="right">Right parser.</param>
		/// <returns>The resulting parser.</returns>
		public static LpmParser operator +(LpmParser left, LpmParser right)
		{
			return new((p) => Lp.Next(left.Do(p), right).DistinctMatches()); // DistinctVoids()
		}

		#endregion And

		#region Or

		/// <summary>
        /// Combinator parsers.
        /// The resulting parser combines the results of two other parsers that are called to the same line.
        /// </summary>
		/// <param name="p1">Parser 1.</param>
		/// <param name="p2">Parser 2.</param>
		/// <returns>The resulting parser.</returns>
		public static LpmParser operator |(LpmParser p1, LpmParser p2)
		{
			return new((p) => p1.Do(p).Concat(p2.Do(p)).DistinctMatches()); // DistinctVoids()
		}

		#endregion Or
	}
}
