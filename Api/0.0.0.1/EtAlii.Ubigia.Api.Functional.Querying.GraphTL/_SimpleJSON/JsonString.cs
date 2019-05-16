namespace SimpleJson
{
    using System.Text;

    public partial class JsonString : JsonNode
    {
        private string _mData;

        public override JsonNodeType Tag { get { return JsonNodeType.String; } }
        public override bool IsString { get { return true; } }

        public override Enumerator GetEnumerator() { return new Enumerator(); }


        public override string Value
        {
            get { return _mData; }
            set
            {
                _mData = value;
            }
        }

        public JsonString(string aData)
        {
            _mData = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('\"').Append(Escape(_mData)).Append('\"');
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;
            string s = obj as string;
            if (s != null)
                return _mData == s;
            JsonString s2 = obj as JsonString;
            if (s2 != null)
                return _mData == s2._mData;
            return false;
        }
        public override int GetHashCode()
        {
            return _mData.GetHashCode();
        }
    }
}