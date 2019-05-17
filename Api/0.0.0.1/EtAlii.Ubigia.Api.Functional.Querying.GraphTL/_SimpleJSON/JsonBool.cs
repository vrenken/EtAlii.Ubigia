namespace SimpleJson
{
    using System.Text;

    public partial class JsonBool : JsonNode
    {
        private bool _mData;

        public override JsonNodeType Tag => JsonNodeType.Boolean;
        public override bool IsBoolean => true;
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public override string Value
        {
            get => _mData.ToString();
            set
            {
                bool v;
                if (bool.TryParse(value, out v))
                    _mData = v;
            }
        }
        public override bool AsBool
        {
            get => _mData;
            set => _mData = value;
        }

        public JsonBool(bool aData)
        {
            _mData = aData;
        }

        public JsonBool(string aData)
        {
            Value = aData;
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append((_mData) ? "true" : "false");
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool)
                return _mData == (bool)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return _mData.GetHashCode();
        }
    }
}