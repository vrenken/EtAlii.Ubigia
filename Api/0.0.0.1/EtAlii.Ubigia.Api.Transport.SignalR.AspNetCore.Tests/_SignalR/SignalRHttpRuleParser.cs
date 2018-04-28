namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Globalization;
    using System.Text;
    using HttpParseResult = EtAlii.Ubigia.Api.Transport.SignalR.Tests.SignalRHttpParseResult;

    internal static class SignalRHttpRuleParser
    {
        private static readonly bool[] tokenChars;
//        private const int maxNestedCount = 5;
        private static readonly string[] dateFormats;
        internal const char CR = '\r';
        internal const char LF = '\n';
        internal const int MaxInt64Digits = 19;
        internal const int MaxInt32Digits = 10;
        internal static readonly Encoding DefaultHttpEncoding;

        static SignalRHttpRuleParser()
        {
            string[] strArray = new string[15];
            int index1 = 0;
            string str1 = "ddd, d MMM yyyy H:m:s 'GMT'";
            strArray[index1] = str1;
            int index2 = 1;
            string str2 = "ddd, d MMM yyyy H:m:s";
            strArray[index2] = str2;
            int index3 = 2;
            string str3 = "d MMM yyyy H:m:s 'GMT'";
            strArray[index3] = str3;
            int index4 = 3;
            string str4 = "d MMM yyyy H:m:s";
            strArray[index4] = str4;
            int index5 = 4;
            string str5 = "ddd, d MMM yy H:m:s 'GMT'";
            strArray[index5] = str5;
            int index6 = 5;
            string str6 = "ddd, d MMM yy H:m:s";
            strArray[index6] = str6;
            int index7 = 6;
            string str7 = "d MMM yy H:m:s 'GMT'";
            strArray[index7] = str7;
            int index8 = 7;
            string str8 = "d MMM yy H:m:s";
            strArray[index8] = str8;
            int index9 = 8;
            string str9 = "dddd, d'-'MMM'-'yy H:m:s 'GMT'";
            strArray[index9] = str9;
            int index10 = 9;
            string str10 = "dddd, d'-'MMM'-'yy H:m:s";
            strArray[index10] = str10;
            int index11 = 10;
            string str11 = "ddd MMM d H:m:s yyyy";
            strArray[index11] = str11;
            int index12 = 11;
            string str12 = "ddd, d MMM yyyy H:m:s zzz";
            strArray[index12] = str12;
            int index13 = 12;
            string str13 = "ddd, d MMM yyyy H:m:s";
            strArray[index13] = str13;
            int index14 = 13;
            string str14 = "d MMM yyyy H:m:s zzz";
            strArray[index14] = str14;
            int index15 = 14;
            string str15 = "d MMM yyyy H:m:s";
            strArray[index15] = str15;
            dateFormats = strArray;
            DefaultHttpEncoding = Encoding.GetEncoding(28591);
            tokenChars = new bool[128];
            for (int index16 = 33; index16 < (int)sbyte.MaxValue; ++index16)
                tokenChars[index16] = true;
            tokenChars[40] = false;
            tokenChars[41] = false;
            tokenChars[60] = false;
            tokenChars[62] = false;
            tokenChars[64] = false;
            tokenChars[44] = false;
            tokenChars[59] = false;
            tokenChars[58] = false;
            tokenChars[92] = false;
            tokenChars[34] = false;
            tokenChars[47] = false;
            tokenChars[91] = false;
            tokenChars[93] = false;
            tokenChars[63] = false;
            tokenChars[61] = false;
            tokenChars[123] = false;
            tokenChars[125] = false;
        }

        private static bool IsTokenChar(char character)
        {
            if ((int)character > (int)sbyte.MaxValue)
                return false;
            return tokenChars[(int)character];
        }

        internal static int GetTokenLength(string input, int startIndex)
        {
            if (startIndex >= input.Length)
                return 0;
            for (int index = startIndex; index < input.Length; ++index)
            {
                if (!IsTokenChar(input[index]))
                    return index - startIndex;
            }
            return input.Length - startIndex;
        }

        internal static int GetWhitespaceLength(string input, int startIndex)
        {
            if (startIndex >= input.Length)
                return 0;
            int index = startIndex;
            while (index < input.Length)
            {
                switch (input[index])
                {
                    case ' ':
                    case '\t':
                        ++index;
                        continue;
                    case '\r':
                        if (index + 2 < input.Length && (int)input[index + 1] == 10)
                        {
                            switch (input[index + 2])
                            {
                                case ' ':
                                case '\t':
                                    index += 3;
                                    continue;
                            }
                        }
                        break;
                }
                return index - startIndex;
            }
            return input.Length - startIndex;
        }

        internal static bool ContainsInvalidNewLine(string value)
        {
            return ContainsInvalidNewLine(value, 0);
        }

        private static bool ContainsInvalidNewLine(string value, int startIndex)
        {
            for (int index1 = startIndex; index1 < value.Length; ++index1)
            {
                if ((int)value[index1] == 13)
                {
                    int index2 = index1 + 1;
                    if (index2 < value.Length && (int)value[index2] == 10)
                    {
                        index1 = index2 + 1;
                        if (index1 == value.Length)
                            return true;
                        switch (value[index1])
                        {
                            case ' ':
                            case '\t':
                                continue;
                            default:
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        internal static int GetNumberLength(string input, int startIndex, bool allowDecimal)
        {
            int index = startIndex;
            bool flag = !allowDecimal;
            if ((int)input[index] == 46)
                return 0;
            while (index < input.Length)
            {
                char ch = input[index];
                if ((int)ch >= 48 && (int)ch <= 57)
                    ++index;
                else if (!flag && (int)ch == 46)
                {
                    flag = true;
                    ++index;
                }
                else
                    break;
            }
            return index - startIndex;
        }

        internal static int GetHostLength(string input, int startIndex, bool allowToken, out string host)
        {
            host = (string)null;
            if (startIndex >= input.Length)
                return 0;
            int index = startIndex;
            bool flag = true;
            for (; index < input.Length; ++index)
            {
                char character = input[index];
                switch (character)
                {
                    case '/':
                        return 0;
                    case ' ':
                    case '\t':
                    case '\r':
                    case ',':
                        goto label_7;
                    default:
                        flag = flag && IsTokenChar(character);
                        continue;
                }
            }
        label_7:
            int length = index - startIndex;
            if (length == 0)
                return 0;
            string host1 = input.Substring(startIndex, length);
            if ((!allowToken || !flag) && !IsValidHostName(host1))
                return 0;
            host = host1;
            return length;
        }

        internal static HttpParseResult GetCommentLength(string input, int startIndex, out int length)
        {
            int nestedCount = 0;
            return GetExpressionLength(input, startIndex, '(', ')', true, ref nestedCount, out length);
        }

        internal static HttpParseResult GetQuotedStringLength(string input, int startIndex, out int length)
        {
            int nestedCount = 0;
            return GetExpressionLength(input, startIndex, '"', '"', false, ref nestedCount, out length);
        }

        private static HttpParseResult GetQuotedPairLength(string input, int startIndex, out int length)
        {
            length = 0;
            if ((int)input[startIndex] != 92)
                return HttpParseResult.NotParsed;
            if (startIndex + 2 > input.Length || (int)input[startIndex + 1] > (int)sbyte.MaxValue)
                return HttpParseResult.InvalidFormat;
            length = 2;
            return HttpParseResult.Parsed;
        }

        internal static string DateToString(DateTimeOffset dateTime)
        {
            return dateTime.ToUniversalTime().ToString("r", (IFormatProvider)CultureInfo.InvariantCulture);
        }

        internal static bool TryStringToDate(string input, out DateTimeOffset result)
        {
            return DateTimeOffset.TryParseExact(input, dateFormats, (IFormatProvider)DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out result);
        }

        private static HttpParseResult GetExpressionLength(string input, int startIndex, char openChar, char closeChar, bool supportsNesting, ref int nestedCount, out int length)
        {
            length = 0;
            if ((int)input[startIndex] != (int)openChar)
                return HttpParseResult.NotParsed;
            int startIndex1 = startIndex + 1;
            while (startIndex1 < input.Length)
            {
                if (startIndex1 + 2 < input.Length && GetQuotedPairLength(input, startIndex1, out var length1) == HttpParseResult.Parsed)
                {
                    startIndex1 += length1;
                }
                else
                {
                    if (supportsNesting && (int)input[startIndex1] == (int)openChar)
                    {
                        ++nestedCount;
                        try
                        {
                            if (nestedCount > 5)
                                return HttpParseResult.InvalidFormat;
                            switch (GetExpressionLength(input, startIndex1, openChar, closeChar, supportsNesting, ref nestedCount, out var length2))
                            {
                                case HttpParseResult.Parsed:
                                    startIndex1 += length2;
                                    break;
                                case HttpParseResult.InvalidFormat:
                                    return HttpParseResult.InvalidFormat;
                            }
                        }
                        finally
                        {
                            --nestedCount;
                        }
                    }
                    if ((int)input[startIndex1] == (int)closeChar)
                    {
                        length = startIndex1 - startIndex + 1;
                        return HttpParseResult.Parsed;
                    }
                    ++startIndex1;
                }
            }
            return HttpParseResult.InvalidFormat;
        }

        private static bool IsValidHostName(string host)
        {
            Uri result;
            return Uri.TryCreate("http://u@" + host + "/", UriKind.Absolute, out result);
        }
    }
}
