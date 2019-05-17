namespace SimpleJson
{
    using System;
    using System.Text;

    public abstract partial class JsonNode
    {
        #region common interface

        public abstract JsonNodeType Tag { get; }

        public virtual JsonNode this[int index] { get => null; set { } }

        public virtual JsonNode this[string aKey] { get => null; set { } }

        public virtual string Value { get => ""; set { } }

        public virtual int Count => 0;

        public virtual bool IsNumber => false;
        public virtual bool IsString => false;
        public virtual bool IsBoolean => false;
        public virtual bool IsNull => false;
        public virtual bool IsArray => false;
        public virtual bool IsObject => false;

        public virtual bool Inline { get => false; set { } }

        public virtual void Add(string key, JsonNode item)
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
            var sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, 0, JsonTextMode.Compact);
            return sb.ToString();
        }

        public virtual string ToString(int aIndent)
        {
            var sb = new StringBuilder();
            WriteToStringBuilder(sb, 0, aIndent, JsonTextMode.Indent);
            return sb.ToString();
        }
        internal abstract void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode);


        #endregion common interface

        private static StringBuilder EscapeBuilder => _mEscapeBuilder ?? (_mEscapeBuilder = new StringBuilder());
        [ThreadStatic]
        private static StringBuilder _mEscapeBuilder;

        internal static string Escape(string aText)
        {
            var sb = EscapeBuilder;
            sb.Length = 0;
            if (sb.Capacity < aText.Length + aText.Length / 10)
                sb.Capacity = aText.Length + aText.Length / 10;
            foreach (var c in aText)
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
            var result = sb.ToString();
            sb.Length = 0;
            return result;
        }

    }
}