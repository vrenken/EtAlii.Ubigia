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


using System.Collections;
using System.Collections.Generic;

namespace Moppet.Lapa
{
    using System;

    /*
     * Note that the structure does not use properties.
     * They really do not need, because especially on anything not save, and the brakes were added weight.
     */

    /// <summary>
	/// The structure describes a block of text.
    /// Note: operators bring to the line not to implement, comparison operators do not realize - this leads to errors of understanding
    /// how the structure and sub-optimal utilization!
	/// </summary>
	public struct LpText : IEnumerable<char>
	{
		/// <summary>
		/// The source of the text.
		/// </summary>
		public string Source { get; init; }

		/// <summary>
        /// The index of the first character of the block
		/// </summary>
		public int Index { get; init; }

		/// <summary>
        /// block Size
		/// </summary>
		public int Length { get; init; }


        /// <summary>
        /// Constructs a new block of text from another, setting the offset start.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="startOffset">The displacement starts. If back then the length of the text will increase if the forward is reduced.</param>
        public LpText(LpText text, int startOffset)
        {
            Source = text.Source; Index = text.Index + startOffset; Length = text.Length - startOffset;
        }

		/// <summary>
		/// The main constructor.
		/// </summary>
		/// <param name="source">Source of text.</param>
		/// <param name="index">The index to the first character of the block.</param>
		/// <param name="length">Block length.</param>
		public LpText(string source, int index, int length)
		{
			Source = source; Index = index; Length = length;
		}

		/// <summary>
		/// Auxiliary constructor.
		/// </summary>
		/// <param name="source">Источник текста.</param>
		public LpText(string source)
		{
			Source = source; Index = 0; Length = source.Length;
		}

		/// <summary>
        /// Returns the character in the relative index [0; Length).
		/// </summary>
        /// <param name="relativeIndex">Index characters [0; Length).</param>
		/// <returns>Символ.</returns>
		public char this[int relativeIndex] => Source[Index + relativeIndex];

	    /// <summary>
        /// The operator of bringing the line to the object.
		/// </summary>
        /// <param name="text">Строка.</param>
        /// <returns>object LpText.</returns>
		public static implicit operator LpText(string text)
		{
			return new LpText(text);
		}

		/// <summary>
        /// Возвращает урезаный от начала блок текста.
		/// </summary>
        /// <param name="n">The number of characters to skip.</param>
        /// <returns>Reduced to n characters of the text block.</returns>
		public LpText Skip(int n)
		{
			return new LpText(Source, Index + n, Length - n);
		}

		/// <summary>
        /// Returns true if the block of text begins with the substring subStr.
		/// </summary>
        /// <param name="subStr">Substring.</param>
        /// <returns>A Boolean value.</returns>
		public bool StartsWith(string subStr)
		{
			return string.CompareOrdinal(Source, Index, subStr, 0, subStr.Length) == 0;
		}

        /// <summary>
        /// Returns true if the block of text begins with the substring subStr.
        /// </summary>
        /// <param name="subStr">Подстрока.</param>
        /// <param name="ignoreCase">Truth is, if you want to enable case-insensitive.</param>
        /// <returns>Булево значение.</returns>
        //[Obsolete("Do not check and is not used anywhere.")]
        public bool StartsWith(string subStr, bool ignoreCase)
        {
            return string.Compare(Source, Index, subStr, 0, subStr.Length, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)  == 0;
        }

        /// <summary>
        /// The string is not always null.
        /// </summary>
        /// <returns>The string is not always null.</returns>
        public override string ToString()
		{
			return Length > 0 ? Source.Substring(Index, Length) : "";
		}

		/// <summary>
        /// The empty structure.
		/// </summary>
		public static readonly LpText Empty = new LpText(null, 0, 0);


        #region Comparison operations substring substring

        /// <summary>
		/// Операция сравнения подстроки с подстрокой.
		/// </summary>
		/// <param name="t">Блок.</param>
		/// <param name="s">Строка.</param>
		/// <returns>Истина, если строки равны.</returns>
		public static bool operator == (LpText t, string s)
		{
			return t.ToString() == s;
		}

		/// <summary>
		/// Операция сравнения подстроки с подстрокой.
		/// </summary>
		/// <param name="s">Строка.</param>
		/// <param name="t">Блок.</param>
		/// <returns>Истина, если строки равны.</returns>
		public static bool operator ==(string s, LpText t)
		{
			return t.ToString() == s;
		}

		/// <summary>
		/// Операция сравнения подстроки с подстрокой.
		/// </summary>
		/// <param name="t">Блок.</param>
		/// <param name="s">Строка.</param>
		/// <returns>Ложь, если строки равны.</returns>
		public static bool operator != (LpText t, string s)
		{
			return t.ToString() != s;
		}

		/// <summary>
		/// Операция сравнения подстроки с подстрокой.
		/// </summary>
		/// <param name="s">Строка.</param>
		/// <param name="t">Блок.</param>
		/// <returns>Ложь, если строки равны.</returns>
		public static bool operator !=(string s, LpText t)
		{
			return t.ToString() != s;
		}

        #endregion Comparison operations substring substring

        /// <summary>
        /// Bitmap comparison structures.
        /// Content Source is not involved in the comparison, i.e. compares the address (link).
		/// </summary>
        /// <param name="obj">The object that points to the structure LpText.</param>
        /// <returns>True if both structures are identical and Source refers to the same memory.</returns>
		public override bool Equals(object obj)
		{
			var t = (LpText)obj;
			return Index == t.Index && Length == t.Length && object.ReferenceEquals(Source, t.Source);
		}

		/// <summary>
		/// Хеш. Source в хешировании не участвует из-за тормозов, т.к. данная структура предназначена,
		/// чтобы отличать блоки текста относительно одного источника.
		/// </summary>
		/// <returns>Хеш.</returns>
		public override int GetHashCode()
		{
			return Index ^ Length;
		}

        /// <summary>
        /// Перечислитель символов.
        /// </summary>
        /// <returns>Перечислитель символов.</returns>
        public IEnumerator<char> GetEnumerator()
        {
            for (var i = 0; i < Length; ++i)
                yield return Source[Index + i];
        }

        /// <summary>
        /// Перечислитель символов.
        /// </summary>
        /// <returns>Перечислитель символов.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
