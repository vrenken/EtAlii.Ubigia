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

namespace Moppet.Lapa.Parsers
{
    /// <summary>
    /// A collection of some simple parsers.
    /// </summary>
    public static class LpNumber
	{
        /// <summary>
        /// Parser scientific number.
        /// It contains a podparsery:
        /// Sign - a sign;
        /// Integer - integer;
        /// Scientific - fractional and / or the number of science;
        /// Exp - exhibitor, such as E + 10;
        /// ExpFactor - decimal degree;
        /// Frac - the fractional part, eg .5, .10;
        /// </summary>
		/// <param name="comma">The decimal part. Default perceives and point and comma.</param>
		public static LpsParser Scientific(Func<char, bool> comma = null)
		{
			if (comma == null)
				comma = c => c == '.' || c == ',';

			var sign   = Lp.Range(c => c == '+' || c == '-', 0, 1).Id("Sign");
			var digits = Lp.Digits();
			var integ  = (sign + digits).Id("Integer");
			var exp    = (Lp.One(c => c == 'E' || c == 'e') + sign + digits.Id("ExpFactor")).Maybe().Id("Exp");
			var frac   = (Lp.One(comma) + digits + exp).Id("Frac");
			var number = (integ + frac).Id("Scientific") | frac.Rename("Scientific") | integ;
			return number.TakeFirst();
		}

        /// <summary>
        /// Function to determine the affiliation to the character hexadecimal number.
        /// </summary>
        public static Func<char, bool> IsHex { get { return (c) => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'); } }

        /// <summary>
        /// Function to determine the accessories symbol to the octal number.
        /// </summary>
        public static Func<char, bool> IsOctal { get { return c => c >= '0' && c <= '8'; } }

        /// <summary>
        /// Function to determine the accessories symbol of the Roman number.
        /// </summary>
        public static Func<char, bool> IsRoman { get { return c => c == 'I' || c == 'V' || c == 'X' || c == 'L' || c == 'C' || c == 'D' || c == 'M'; } }

        /// <summary>
        /// The parser is a simple decimal numbers with a positive test of its value.
        /// An example of parsing the date in the format yyyy.MM.dd: Positive (0, 9999, 4) + '.' + Positive (1, 12, 2) + '.' + Positive (1, 31, 2)
        /// </summary>
        /// <param name="minDigits">The minimum number of characters. Required when tedious to parse the number format 001, 002, 012 ...</param>
        /// <returns>parser.</returns>
        public static LpsParser Positive(int minDigits = 1)
	    {
	        return Positive(0, long.MaxValue, minDigits);
	    }

	    /// <summary>
	    /// The parser is a simple decimal numbers with a positive test of its value.
        /// An example of parsing the date in the format yyyy.MM.dd: Positive (0, 9999, 4) + '.' + Positive (1, 12, 2) + '.' + Positive (1, 31, 2)
        /// </summary>
        /// <param name="minValue">The minimum allowable value. For example, a month may start with one.</param>
        /// <param name="maxValue">The maximum allowable value. For example, the months of the year more than 12 and can not be.</param>
        /// <param name="minDigits">The minimum number of characters. Required when tedious to parse the number format 001, 002, 012 ...</param>
        /// <returns>parser.</returns>
        public static LpsParser Positive(long minValue, long maxValue, int minDigits = 1)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("maxValue", "maxValue must be greater or equal minValue.");

            if (minValue < 0)
                throw new ArgumentOutOfRangeException("minValue");
            
            if (minDigits < 1)
                throw new ArgumentOutOfRangeException("minDigits");

            var maxDigits = 1;
            var rest = maxValue / 10;
            while (rest > 0)
            {
                rest /= 10; ++maxDigits;
            }
            var parser = new LpsParser("Integer", (text) =>
            {
                var end = text.Length > maxDigits ? (maxDigits + 1) : text.Length;
                int cur = 0, ind = text.Index;
                var str = text.Source;
                var val = 0L;

                while (cur < end)
                {
                    var c = str[ind];
                    var n = c - '0';
                    if (n > 9 || n < 0)
                        break;

                    val *= 10;
                    val += n;
                    ++ind;
                    ++cur;
                }
                return (cur >= minDigits && cur <= maxDigits && val >= minValue && val <= maxValue) ? new LpNode(text, cur) : new LpNode(text);
            });
            return parser;
        }

        /// <summary>
        /// The hexadecimal number.
        /// </summary>
        /// <param name="prefix">Prefix before the number. Default is '0x' for the C language notation.</param>
        /// <param name="id">ID nodes.</param>
        public static LpsParser Hex(string prefix = "0x", string id = "Hex")
		{
            var hex = Lp.OneOrMore((c) => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F')).Id(id); //TODO: Embedding only LpLex
            if (prefix != null)
                hex = (Lp.Term(prefix) + hex).Id(id);
			return hex;
		}
	}
}
