// ReSharper disable All

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

namespace Moppet.Lapa
{
	/// <summary>
	/// A class that helps build a recursive parser for parsing some expression in parentheses.
	/// An expression can be parenthesized multiple times, for example ((expr)).
	/// </summary>
	public class LpBrackets
	{
	    private readonly LpsParser _openBracket;
	    private readonly LpsParser _closeBracket;
	    private readonly LpsParser _body;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="openBracket">opening bracket.</param>
		/// <param name="body">body.</param>
		/// <param name="closeBracket">Closing parenthesis.</param>
		public LpBrackets(LpsParser openBracket, LpsParser body, LpsParser closeBracket)
		{
			if (openBracket == null)
				throw new ArgumentNullException("openBracket");
			
			if (body == null)
				throw new ArgumentNullException("body");
			
			if (closeBracket == null)
				throw new ArgumentNullException("closeBracket");

			_openBracket = openBracket;
			_body = body;
			_closeBracket = closeBracket;
		}

		/// <summary>
		/// The main function is a parser.
		/// </summary>
		/// <param name="t">Block text.</param>
		/// <returns>result.</returns>
		public LpNode Do(LpText t)
		{
			var open = _openBracket.Do(t);
			if (!open.Success)
                return open;

			var body = _body.Do(open.Rest);

			if (!body.Success)
				body = Do(open.Rest);

			if (!body.Success)
                return body;

			var close = _closeBracket.Do(body.Rest);
			if (!close.Success)
                return close;

            return new LpNode(t, close.Rest.Index - t.Index, null, open, body, close);
		}
	}
}
