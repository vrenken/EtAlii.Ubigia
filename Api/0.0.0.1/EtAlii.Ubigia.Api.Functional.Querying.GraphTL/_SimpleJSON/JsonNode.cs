namespace SimpleJson
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public abstract partial class JsonNode
    {
        #region Enumerators
        public struct Enumerator
        {
            private enum Type { None, Array, Object }
            private Type _type;
            private Dictionary<string, JsonNode>.Enumerator _mObject;
            private List<JsonNode>.Enumerator _mArray;
            public bool IsValid { get { return _type != Type.None; } }
            public Enumerator(List<JsonNode>.Enumerator aArrayEnum)
            {
                _type = Type.Array;
                _mObject = default(Dictionary<string, JsonNode>.Enumerator);
                _mArray = aArrayEnum;
            }
            public Enumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum)
            {
                _type = Type.Object;
                _mObject = aDictEnum;
                _mArray = default(List<JsonNode>.Enumerator);
            }
            public KeyValuePair<string, JsonNode> Current
            {
                get {
                    if (_type == Type.Array)
                        return new KeyValuePair<string, JsonNode>(string.Empty, _mArray.Current);
                    else if (_type == Type.Object)
                        return _mObject.Current;
                    return new KeyValuePair<string, JsonNode>(string.Empty, null);
                }
            }
            public bool MoveNext()
            {
                if (_type == Type.Array)
                    return _mArray.MoveNext();
                else if (_type == Type.Object)
                    return _mObject.MoveNext();
                return false;
            }
        }
        public struct ValueEnumerator
        {
            private Enumerator _mEnumerator;
            public ValueEnumerator(List<JsonNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public ValueEnumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public ValueEnumerator(Enumerator aEnumerator) { _mEnumerator = aEnumerator; }
            public JsonNode Current { get { return _mEnumerator.Current.Value; } }
            public bool MoveNext() { return _mEnumerator.MoveNext(); }
            public ValueEnumerator GetEnumerator() { return this; }
        }
        public struct KeyEnumerator
        {
            private Enumerator _mEnumerator;
            public KeyEnumerator(List<JsonNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public KeyEnumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public KeyEnumerator(Enumerator aEnumerator) { _mEnumerator = aEnumerator; }
            public string Current { get { return _mEnumerator.Current.Key; } }
            public bool MoveNext() { return _mEnumerator.MoveNext(); }
            public KeyEnumerator GetEnumerator() { return this; }
        }

        public class LinqEnumerator : IEnumerator<KeyValuePair<string, JsonNode>>, IEnumerable<KeyValuePair<string, JsonNode>>
        {
            private JsonNode _mNode;
            private Enumerator _mEnumerator;
            internal LinqEnumerator(JsonNode aNode)
            {
                _mNode = aNode;
                if (_mNode != null)
                    _mEnumerator = _mNode.GetEnumerator();
            }
            public KeyValuePair<string, JsonNode> Current { get { return _mEnumerator.Current; } }
            object IEnumerator.Current { get { return _mEnumerator.Current; } }
            public bool MoveNext() { return _mEnumerator.MoveNext(); }

            public void Dispose()
            {
                _mNode = null;
                _mEnumerator = new Enumerator();
            }

            public IEnumerator<KeyValuePair<string, JsonNode>> GetEnumerator()
            {
                return new LinqEnumerator(_mNode);
            }

            public void Reset()
            {
                if (_mNode != null)
                    _mEnumerator = _mNode.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new LinqEnumerator(_mNode);
            }
        }

        #endregion Enumerators

        #region common interface

        public static bool ForceAscii = false; // Use Unicode by default
        public static bool LongAsString = false; // lazy creator creates a JSONString instead of JSONNumber
        public static bool AllowLineComments = true; // allow "//"-style comments at the end of a line

        public abstract JsonNodeType Tag { get; }

        public virtual JsonNode this[int aIndex] { get { return null; } set { } }

        public virtual JsonNode this[string aKey] { get { return null; } set { } }

        public virtual string Value { get { return ""; } set { } }

        public virtual int Count { get { return 0; } }

        public virtual bool IsNumber { get { return false; } }
        public virtual bool IsString { get { return false; } }
        public virtual bool IsBoolean { get { return false; } }
        public virtual bool IsNull { get { return false; } }
        public virtual bool IsArray { get { return false; } }
        public virtual bool IsObject { get { return false; } }

        public virtual bool Inline { get { return false; } set { } }

        public virtual void Add(string aKey, JsonNode aItem)
        {
        }
        public virtual void Add(JsonNode aItem)
        {
            Add("", aItem);
        }

        public virtual JsonNode Remove(string aKey)
        {
            return null;
        }

        public virtual JsonNode Remove(int aIndex)
        {
            return null;
        }

        public virtual JsonNode Remove(JsonNode aNode)
        {
            return aNode;
        }

        public virtual IEnumerable<JsonNode> Children
        {
            get
            {
                yield break;
            }
        }

        public IEnumerable<JsonNode> DeepChildren
        {
            get
            {
                foreach (var c in Children)
                foreach (var d in c.DeepChildren)
                    yield return d;
            }
        }

        public virtual bool HasKey(string aKey)
        {
            return false;
        }

        public virtual JsonNode GetValueOrDefault(string aKey, JsonNode aDefault)
        {
            return aDefault;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, 0, JsonTextMode.Compact);
            return sb.ToString();
        }

        public virtual string ToString(int aIndent)
        {
            StringBuilder sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, aIndent, JsonTextMode.Indent);
            return sb.ToString();
        }
        internal abstract void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode);

        public abstract Enumerator GetEnumerator();
        public IEnumerable<KeyValuePair<string, JsonNode>> Linq { get { return new LinqEnumerator(this); } }
        public KeyEnumerator Keys { get { return new KeyEnumerator(GetEnumerator()); } }
        public ValueEnumerator Values { get { return new ValueEnumerator(GetEnumerator()); } }

        #endregion common interface

        #region typecasting properties


        public virtual double AsDouble
        {
            get
            {
                double v = 0.0;
                if (double.TryParse(Value,NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                    return v;
                return 0.0;
            }
            set
            {
                Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public virtual int AsInt
        {
            get { return (int)AsDouble; }
            set { AsDouble = value; }
        }

        public virtual float AsFloat
        {
            get { return (float)AsDouble; }
            set { AsDouble = value; }
        }

        public virtual bool AsBool
        {
            get
            {
                bool v = false;
                if (bool.TryParse(Value, out v))
                    return v;
                return !string.IsNullOrEmpty(Value);
            }
            set
            {
                Value = (value) ? "true" : "false";
            }
        }

        public virtual long AsLong
        {
            get
            {
                long val = 0;
                if (long.TryParse(Value, out val))
                    return val;
                return 0L;
            }
            set
            {
                Value = value.ToString();
            }
        }

        public virtual JsonArray AsArray
        {
            get
            {
                return this as JsonArray;
            }
        }

        public virtual JsonObject AsObject
        {
            get
            {
                return this as JsonObject;
            }
        }


        #endregion typecasting properties

        #region operators

        public static implicit operator JsonNode(string s)
        {
            return new JsonString(s);
        }
        public static implicit operator string(JsonNode d)
        {
            return (d == null) ? null : d.Value;
        }

        public static implicit operator JsonNode(double n)
        {
            return new JsonNumber(n);
        }
        public static implicit operator double(JsonNode d)
        {
            return (d == null) ? 0 : d.AsDouble;
        }

        public static implicit operator JsonNode(float n)
        {
            return new JsonNumber(n);
        }
        public static implicit operator float(JsonNode d)
        {
            return (d == null) ? 0 : d.AsFloat;
        }

        public static implicit operator JsonNode(int n)
        {
            return new JsonNumber(n);
        }
        public static implicit operator int(JsonNode d)
        {
            return (d == null) ? 0 : d.AsInt;
        }

        public static implicit operator JsonNode(long n)
        {
            if (LongAsString)
                return new JsonString(n.ToString());
            return new JsonNumber(n);
        }
        public static implicit operator long(JsonNode d)
        {
            return (d == null) ? 0L : d.AsLong;
        }

        public static implicit operator JsonNode(bool b)
        {
            return new JsonBool(b);
        }
        public static implicit operator bool(JsonNode d)
        {
            return (d == null) ? false : d.AsBool;
        }

        public static implicit operator JsonNode(KeyValuePair<string, JsonNode> aKeyValue)
        {
            return aKeyValue.Value;
        }

        public static bool operator ==(JsonNode a, object b)
        {
            if (ReferenceEquals(a, b))
                return true;
            bool aIsNull = a is JsonNull || ReferenceEquals(a, null) || a is JsonLazyCreator;
            bool bIsNull = b is JsonNull || ReferenceEquals(b, null) || b is JsonLazyCreator;
            if (aIsNull && bIsNull)
                return true;
            return !aIsNull && a.Equals(b);
        }

        public static bool operator !=(JsonNode a, object b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion operators

        [ThreadStatic]
        private static StringBuilder _mEscapeBuilder;
        internal static StringBuilder EscapeBuilder
        {
            get {
                if (_mEscapeBuilder == null)
                    _mEscapeBuilder = new StringBuilder();
                return _mEscapeBuilder;
            }
        }
        internal static string Escape(string aText)
        {
            var sb = EscapeBuilder;
            sb.Length = 0;
            if (sb.Capacity < aText.Length + aText.Length / 10)
                sb.Capacity = aText.Length + aText.Length / 10;
            foreach (char c in aText)
            {
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    default:
                        if (c < ' ' || (ForceAscii && c > 127))
                        {
                            ushort val = c;
                            sb.Append("\\u").Append(val.ToString("X4"));
                        }
                        else
                            sb.Append(c);
                        break;
                }
            }
            string result = sb.ToString();
            sb.Length = 0;
            return result;
        }

        private static JsonNode ParseElement(string token, bool quoted)
        {
            if (quoted)
                return token;
            string tmp = token.ToLower();
            if (tmp == "false" || tmp == "true")
                return tmp == "true";
            if (tmp == "null")
                return JsonNull.CreateOrGet();
            double val;
            if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                return val;
            else
                return token;
        }

        public static JsonNode Parse(string aJson)
        {
            Stack<JsonNode> stack = new Stack<JsonNode>();
            JsonNode ctx = null;
            int i = 0;
            StringBuilder token = new StringBuilder();
            string tokenName = "";
            bool quoteMode = false;
            bool tokenIsQuoted = false;
            while (i < aJson.Length)
            {
                switch (aJson[i])
                {
                    case '{':
                        if (quoteMode)
                        {
                            token.Append(aJson[i]);
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
                            token.Append(aJson[i]);
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

                            token.Append(aJson[i]);
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
                            token.Append(aJson[i]);
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
                            token.Append(aJson[i]);
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
                            token.Append(aJson[i]);
                        break;

                    case '\\':
                        ++i;
                        if (quoteMode)
                        {
                            char c = aJson[i];
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
                                    string s = aJson.Substring(i + 1, 4);
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
                    case '/':
                        if (AllowLineComments && !quoteMode && i + 1 < aJson.Length && aJson[i+1] == '/')
                        {
                            while (++i < aJson.Length && aJson[i] != '\n' && aJson[i] != '\r') ;
                            break;
                        }
                        token.Append(aJson[i]);
                        break;
                    case '\uFEFF': // remove / ignore BOM (Byte Order Mark)
                        break;

                    default:
                        token.Append(aJson[i]);
                        break;
                }
                ++i;
            }
            if (quoteMode)
            {
                throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
            }
            if (ctx == null)
                return ParseElement(token.ToString(), tokenIsQuoted);
            return ctx;
        }

    }
}