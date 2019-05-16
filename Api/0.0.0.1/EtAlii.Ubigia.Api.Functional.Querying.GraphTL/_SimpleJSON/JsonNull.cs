namespace SimpleJson
{
    using System.Text;

    public partial class JsonNull : JsonNode
    {
        static JsonNull _mStaticInstance = new JsonNull();
        public static bool ReuseSameInstance = true;
        public static JsonNull CreateOrGet()
        {
            if (ReuseSameInstance)
                return _mStaticInstance;
            return new JsonNull();
        }
        private JsonNull() { }

        public override JsonNodeType Tag { get { return JsonNodeType.NullValue; } }
        public override bool IsNull { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get { return "null"; }
            set { }
        }
        public override bool AsBool
        {
            get { return false; }
            set { }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;
            return (obj is JsonNull);
        }
        public override int GetHashCode()
        {
            return 0;
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append("null");
        }
    }
}