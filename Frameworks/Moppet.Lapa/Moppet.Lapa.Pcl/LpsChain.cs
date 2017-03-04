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
		/// Список парсеров.
		/// </summary>
		List<LpsParser> m_parsers = new List<LpsParser>();

		/// <summary>
		/// The default constructor. Пустая цепочка.
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
			m_parsers = new List<LpsParser>(parsers.Length); m_parsers.AddRange(parsers);
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="left">Left chain.</param>
		/// <param name="right">Right chain.</param>
		public LpsChain(IEnumerable<LpsParser> left, IEnumerable<LpsParser> right)
		{
			m_parsers = new List<LpsParser>(left.Count() + right.Count());
			m_parsers.AddRange(left);
			m_parsers.AddRange(right);
		}

		/// <summary>
		/// A copy of the object.
		/// </summary>
		/// <returns>Branch.</returns>
		public override LpsChain Copy()
		{
			var c = base.Copy();
			c.m_parsers = new List<LpsParser>(m_parsers.Count + 1);
			c.m_parsers.AddRange(m_parsers);
			return c;
		}

		/// <summary>
		/// Вспомогательная функция конвертации цепочки в парсер.
		/// </summary>
		/// <returns>parser.</returns>
		public LpsParser ToParser()
		{
			if (m_parsers.Count <= 0)
				throw new Exception("Parsers chain is empty.");

			if (m_parsers.Count == 1)
				return m_parsers[0];

			if (m_parsers.Count > 2)
				return Concat(m_parsers, Identifier, WrapNode);

			var par1 = m_parsers[0]; // Копии нужны, ибо массив Parsers может измениться.
			var par2 = m_parsers[1];

			return new LpsParser(Identifier, WrapNode, (text) =>
			{
				var left = par1.Do(text);
                if (left.Match.Length < 0)
                    return new LpNode(text);
				var right = par2.Do(left.Rest);
                if (right.Match.Length < 0)
                    return new LpNode(text); // Здесь нельзя просто вернуть right, т.к. будет неверный остаток.

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
		/// Оператор конкатенации парсера к цепочке.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right odnorezultatny parser.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsChain left, LpsParser right)
		{
            if (left.m_identifier != null)
                return new LpsChain(left.ToParser(), right);
			return new LpsChain(left.m_parsers, new [] { right });
		}

		/// <summary>
		/// Оператор конкатенации цепочек парсеров.
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
                return new LpsChain(new [] { left.ToParser() }, right.m_parsers);
            }
            else if (right.Identifier != null)
            {
                if (left.Identifier != null)
                    return new LpsChain(left, right);
                return new LpsChain(left.m_parsers, new[] { right.ToParser() });
            }
            return new LpsChain(left.m_parsers, right.m_parsers);
		}

		/// <summary>
		/// Оператор конкатенации цепочек парсеров.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Search for a (following) characters.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator +(LpsChain left, char right)
		{
            if (left.m_identifier != null)
                return new LpsChain(left.ToParser(), Lp.Char(right));
			return new LpsChain(left.m_parsers, new[] { Lp.Char(right) });
		}

        /// <summary>
        /// Оператор конкатенации цепочек парсеров.
        /// </summary>
        /// <param name="left">Chain parsers.</param>
        /// <param name="right">Search for a (following) characters.</param>
        /// <returns>parser.</returns>
        public static LpsChain operator + (LpsChain left, Func<char, bool> right)
        {
            if (left.m_identifier != null)
                return new LpsChain(left.ToParser(), Lp.One(right));
            return new LpsChain(left.m_parsers, new[] { Lp.One(right) });
        }

		/// <summary>
		/// Оператор конкатенации цепочек парсеров.
		/// </summary>
		/// <param name="left">Поиск первого символа.</param>
		/// <param name="right">Поиск соответствия следующей цепочке парсеров.</param>
		/// <returns>parser.</returns>
        public static LpsChain operator + (char left, LpsChain right)
        {
            if (right.m_identifier != null)
                return new LpsChain(Lp.Char(left), right.ToParser());
            return new LpsChain(new[] { Lp.Char(left) },  right.m_parsers);
        }

        /// <summary>
        /// Оператор конкатенации цепочек парсеров.
        /// </summary>
        /// <param name="left">Поиск первого символа.</param>
        /// <param name="right">Поиск соответствия следующей цепочке парсеров.</param>
        /// <returns>parser.</returns>
        public static LpsChain operator + (Func<char, bool> left, LpsChain right)
        {
            if (right.m_identifier != null)
                return new LpsChain(Lp.One(left), right.ToParser());
            return new LpsChain(new[] { Lp.One(left) }, right.m_parsers);
        }


		/// <summary>
		/// Оператор конкатенации цепочек парсеров.
		/// </summary>
		/// <param name="left">Поиск первого терма.</param>
		/// <param name="right">Поиск соответствия следующей цепочке парсеров.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (string left, LpsChain right)
		{
            if (right.m_identifier != null)
                return new LpsChain(Lp.Term(left), right.ToParser());

            return new LpsChain(new[] { Lp.Term(left) }, right.m_parsers);
		}

		/// <summary>
		/// Оператор конкатенации цепочек парсеров.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Search for a (next) word.</param>
		/// <returns>parser.</returns>
		public static LpsChain operator + (LpsChain left, string right)
		{
            if (left.m_identifier != null)
                return new LpsChain(left.ToParser(), Lp.Term(right));

			return new LpsChain(left.m_parsers, new[] { Lp.Term(right) });
		}


		/// <summary>
		/// Оператор Or. Либо цепочка либо цепочка.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Multiparser.</returns>
		public static LpsAlternatives operator | (LpsChain left, LpsChain right)
		{
			return left.ToParser() | right.ToParser();
		}

		/// <summary>
		/// Оператор Or. Либо цепочка либо парсер.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Parser.</param>
		/// <returns>Цепочка из двух альтернатив.</returns>
		public static LpsAlternatives operator | (LpsChain left, LpsParser right)
		{
			return left.ToParser() | right;
		}


		/// <summary>
		/// Оператор Or. Либо цепочка либо мультипарсер.
		/// </summary>
		/// <param name="left">Chain parsers.</param>
		/// <param name="right">Right multiparser.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpsChain left, LpmParser right)
		{
			return left.ToParser() | right;
		}

		/// <summary>
		/// Оператор Or. Либо мультипарсер либо цепочка.
		/// </summary>
		/// <param name="left">Левый мультипарсер.</param>
		/// <param name="right">Chain parsers.</param>
		/// <returns>Multiparser.</returns>
		public static LpmParser operator | (LpmParser left, LpsChain right)
		{
			return left | right.ToParser();
		}

		/// <summary>
		/// Оператор конкатенации мултипарсера к цепочке.
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
        /// Returns the parser that returns the two options: either a single blank line + line or empty line.
		/// </summary>
		/// <returns>parser.</returns>
		public LpmParser Maybe_()
		{
			return new LpmParser((p) => Lp.Maybe_(ToParser(), p));
		}

		/// <summary>
		/// Returns multiparser find one and / or appropriate.
        /// Returns a parser returns all the options that 1, 12, 123, 1234, etc.
		/// </summary>
		/// <returns>Multiparser.</returns>
		public LpmParser OneOrMore_()
		{
			return new LpmParser((p) => Lp.OneOrMore_(ToParser(), p));
		}

		/// <summary>
        /// Returns multiparser to capture any number of matches including a blank.
        /// Returns a parser returns all the options that empty, 1, 12, 123, 1234, etc.
        /// </summary>
		/// <returns>Multiparser.</returns>
		public LpmParser ZeroOrMore_()
		{
			return new LpmParser((p) => Lp.ZeroOrMore(ToParser(), p));
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
		/// <param name="parsersList">Список парсеров.</param>
		/// <returns>The resulting parser.</returns>
		public static Func<LpText, LpNode> Concat(IEnumerable<LpsParser> parsersList)
		{
			var parsers = parsersList.ToArray();
			if (parsers.Length < 2)
				throw new ArgumentOutOfRangeException("parsersList");

			return (text) =>
			{
                var children = new List<LpNode>(0x10);
                var next = new LpNode(new LpText(text.Source, text.Index, 0), text);

                int nextLen = 0;
				for (int i = 0; i < parsers.Length; ++i)
				{
					next = parsers[i].Do(next.Rest);
                    nextLen = next.Match.Length;
                    if (nextLen < 0)
                        return new LpNode(text);

                    if (nextLen > 0)
						children.Add(next);
                    else if (nextLen == 0 && next.Id != null)
                        children.Add(next);
				}
                if (children.Count == 0)
                    return new LpNode(text, next.Rest.Index - text.Index, null, null);
                return new LpNode(text, next.Rest.Index - text.Index, null, children);
			};
		}

		/// <summary>
		/// Consistent application of combinatorial parsers.
		/// </summary>
		/// <param name="parsersList">Список парсеров.</param>
		/// <param name="id">ID.</param>
		/// <param name="wrapNode">wrapping result.</param>
		/// <returns>The resulting parser.</returns>
		public static LpsParser Concat(IEnumerable<LpsParser> parsersList, string id, bool wrapNode = false)
		{
			var func = Concat(parsersList);
			return new LpsParser(id: id, wrapNode: wrapNode, parser : func);
		}
	}
}
