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
	/// Класс, который помогает строить рекурсивный парсер для разбора некоторого выражения в скобках.
	/// Выражение может быть заключено в скобки многократно, например ((expr)).
	/// </summary>
	public class LpBrackets
	{
	    readonly LpsParser m_openBracket;
	    readonly LpsParser m_closeBracket;
	    readonly LpsParser m_body;

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

			m_openBracket = openBracket;
			m_body = body;
			m_closeBracket = closeBracket;
		}

		/// <summary>
		/// Основная функция - парсер.
		/// </summary>
		/// <param name="t">Блок текста.</param>
		/// <returns>result.</returns>
		public LpNode Do(LpText t)
		{
			var open = m_openBracket.Do(t);
			if (!open.Success)
                return open;

			var body = m_body.Do(open.Rest);

			if (!body.Success)
				body = Do(open.Rest);

			if (!body.Success)
                return body;

			var close = m_closeBracket.Do(body.Rest);
			if (!close.Success)
                return close;

            return new LpNode(t, close.Rest.Index - t.Index, null, open, body, close);
		}
	}
}
