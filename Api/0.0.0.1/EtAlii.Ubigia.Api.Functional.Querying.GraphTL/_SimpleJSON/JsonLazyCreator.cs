namespace SimpleJson
{
    using System.Text;

    internal partial class JsonLazyCreator : JsonNode
    {
        private JsonNode _mNode = null;
        private string _mKey = null;
        public override JsonNodeType Tag { get { return JsonNodeType.None; } }
        public override Enumerator GetEnumerator() { return new Enumerator(); }

        public JsonLazyCreator(JsonNode aNode)
        {
            _mNode = aNode;
            _mKey = null;
        }

        public JsonLazyCreator(JsonNode aNode, string aKey)
        {
            _mNode = aNode;
            _mKey = aKey;
        }

        private T Set<T>(T aVal) where T : JsonNode
        {
            if (_mKey == null)
                _mNode.Add(aVal);
            else
                _mNode.Add(_mKey, aVal);
            _mNode = null; // Be GC friendly.
            return aVal;
        }

        public override JsonNode this[int aIndex]
        {
            get { return new JsonLazyCreator(this); }
            set { Set(new JsonArray()).Add(value); }
        }

        public override JsonNode this[string aKey]
        {
            get { return new JsonLazyCreator(this, aKey); }
            set { Set(new JsonObject()).Add(aKey, value); }
        }

        public override void Add(JsonNode aItem)
        {
            Set(new JsonArray()).Add(aItem);
        }

        public override void Add(string aKey, JsonNode aItem)
        {
            Set(new JsonObject()).Add(aKey, aItem);
        }

        public static bool operator ==(JsonLazyCreator a, object b)
        {
            if (b == null)
                return true;
            return System.Object.ReferenceEquals(a, b);
        }

        public static bool operator !=(JsonLazyCreator a, object b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return true;
            return System.Object.ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override int AsInt
        {
            get { Set(new JsonNumber(0)); return 0; }
            set { Set(new JsonNumber(value)); }
        }

        public override float AsFloat
        {
            get { Set(new JsonNumber(0.0f)); return 0.0f; }
            set { Set(new JsonNumber(value)); }
        }

        public override double AsDouble
        {
            get { Set(new JsonNumber(0.0)); return 0.0; }
            set { Set(new JsonNumber(value)); }
        }

        public override long AsLong
        {
            get
            {
                if (LongAsString)
                    Set(new JsonString("0"));
                else
                    Set(new JsonNumber(0.0));
                return 0L;
            }
            set
            {
                if (LongAsString)
                    Set(new JsonString(value.ToString()));
                else
                    Set(new JsonNumber(value));
            }
        }

        public override bool AsBool
        {
            get { Set(new JsonBool(false)); return false; }
            set { Set(new JsonBool(value)); }
        }

        public override JsonArray AsArray
        {
            get { return Set(new JsonArray()); }
        }

        public override JsonObject AsObject
        {
            get { return Set(new JsonObject()); }
        }
        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append("null");
        }
    }
}