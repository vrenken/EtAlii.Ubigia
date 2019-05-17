namespace SimpleJson
{
    using System.Text;

    public partial class JsonAnnotation : JsonNode
    {
        private string _annotation;

        public override JsonNodeType Tag => JsonNodeType.String;
        public override bool IsString => true;

        public override Enumerator GetEnumerator() { return new Enumerator(); }


        public override string Value { get => _annotation; set => _annotation = value; }

        public JsonAnnotation(string annotation)
        {
            _annotation = annotation;
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('@').Append(Escape(_annotation)).Append('');
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;
            if (obj is string s)
                return _annotation == s;
            var s2 = obj as JsonString;
            if (s2 != null)
                return _annotation == s2._mData;
            return false;
        }
        public override int GetHashCode()
        {
            return _annotation.GetHashCode();
        }
    }
}