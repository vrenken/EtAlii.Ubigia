namespace SimpleJson
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public abstract partial class JsonNode
    {
 
        public static bool ForceAscii = false; // Use Unicode by default
        public static bool LongAsString = false; // lazy creator creates a JSONString instead of JSONNumber
        public static bool AllowLineComments = true; // allow "//"-style comments at the end of a line

        private static JsonNode ParseElement(string token, bool quoted)
        {
            if (quoted)
                return token;
            var tmp = token.ToLower();
            if (tmp == "false" || tmp == "true")
                return tmp == "true";
            if (tmp == "null")
                return JsonNull.CreateOrGet();
            if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                return val;
            else
                return token;
        }

        public static JsonNode Parse(string jsonText)
        {
            var stack = new Stack<JsonNode>();
            JsonNode ctx = null;
            var i = 0;
            var token = new StringBuilder();
            var tokenName = "";
            var quoteMode = false;
            var tokenIsQuoted = false;
            while (i < jsonText.Length)
            {
                switch (jsonText[i])
                {
                    case '{':
                        if (quoteMode)
                        {
                            token.Append(jsonText[i]);
                            break;
                        }
                        stack.Push(new JsonObject());
                        if (ctx != null)
                        {
                            ctx.Add(tokenName, stack.Peek());
                        }
                        tokenName = "";
                        token.Length = 0;
                        ctx = stack.Peek();
                        break;

                    case '[':
                        if (quoteMode)
                        {
                            token.Append(jsonText[i]);
                            break;
                        }

                        stack.Push(new JsonArray());
                        if (ctx != null)
                        {
                            ctx.Add(tokenName, stack.Peek());
                        }
                        tokenName = "";
                        token.Length = 0;
                        ctx = stack.Peek();
                        break;

                    case '}':
                    case ']':
                        if (quoteMode)
                        {

                            token.Append(jsonText[i]);
                            break;
                        }
                        if (stack.Count == 0)
                            throw new Exception("JSON Parse: Too many closing brackets");

                        stack.Pop();
                        if (token.Length > 0 || tokenIsQuoted)
                            ctx.Add(tokenName, ParseElement(token.ToString(), tokenIsQuoted));
                        tokenIsQuoted = false;
                        tokenName = "";
                        token.Length = 0;
                        if (stack.Count > 0)
                            ctx = stack.Peek();
                        break;

                    case ':':
                        if (quoteMode)
                        {
                            token.Append(jsonText[i]);
                            break;
                        }
                        tokenName = token.ToString();
                        token.Length = 0;
                        tokenIsQuoted = false;
                        break;

                    case '"':
                        quoteMode ^= true;
                        tokenIsQuoted |= quoteMode;
                        break;

                    case ',':
                        if (quoteMode)
                        {
                            token.Append(jsonText[i]);
                            break;
                        }
                        if (token.Length > 0 || tokenIsQuoted)
                            ctx.Add(tokenName, ParseElement(token.ToString(), tokenIsQuoted));
                        tokenIsQuoted = false;
                        tokenName = "";
                        token.Length = 0;
                        tokenIsQuoted = false;
                        break;

                    case '\r':
                    case '\n':
                        break;

                    case ' ':
                    case '\t':
                        if (quoteMode)
                            token.Append(jsonText[i]);
                        break;

                    case '\\':
                        ++i;
                        if (quoteMode)
                        {
                            var c = jsonText[i];
                            switch (c)
                            {
                                case 't':
                                    token.Append('\t');
                                    break;
                                case 'r':
                                    token.Append('\r');
                                    break;
                                case 'n':
                                    token.Append('\n');
                                    break;
                                case 'b':
                                    token.Append('\b');
                                    break;
                                case 'f':
                                    token.Append('\f');
                                    break;
                                case 'u':
                                {
                                    var s = jsonText.Substring(i + 1, 4);
                                    token.Append((char)int.Parse(
                                        s,
                                        System.Globalization.NumberStyles.AllowHexSpecifier));
                                    i += 4;
                                    break;
                                }
                                default:
                                    token.Append(c);
                                    break;
                            }
                        }
                        break;
                    case '@':
                        if (!quoteMode && i + 1 < jsonText.Length)
                        {
                            while (++i < jsonText.Length && jsonText[i] != '\n' && jsonText[i] != '\r') ;
                            break;
                        }
                        token.Append(jsonText[i]);
                        break;
                    case '/':
                        if (AllowLineComments && !quoteMode && i + 1 < jsonText.Length && jsonText[i+1] == '/')
                        {
                            while (++i < jsonText.Length && jsonText[i] != '\n' && jsonText[i] != '\r') ;
                            break;
                        }
                        token.Append(jsonText[i]);
                        break;
                    case '\uFEFF': // remove / ignore BOM (Byte Order Mark)
                        break;

                    default:
                        token.Append(jsonText[i]);
                        break;
                }
                ++i;
            }
            if (quoteMode)
            {
                throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
            }
            return ctx == null ? ParseElement(token.ToString(), tokenIsQuoted) : ctx;
        }

    }
}