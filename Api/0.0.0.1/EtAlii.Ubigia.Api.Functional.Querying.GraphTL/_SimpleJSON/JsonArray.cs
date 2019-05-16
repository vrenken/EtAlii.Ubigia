namespace SimpleJson
{
    using System.Collections.Generic;
    using System.Text;

    public partial class JsonArray : JsonNode
    {
        private List<JsonNode> _mList = new List<JsonNode>();
        private bool _inline = false;
        public override bool Inline
        {
            get { return _inline; }
            set { _inline = value; }
        }

        public override JsonNodeType Tag { get { return JsonNodeType.Array; } }
        public override bool IsArray { get { return true; } }
        public override Enumerator GetEnumerator() { return new Enumerator(_mList.GetEnumerator()); }

        public override JsonNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= _mList.Count)
                    return new JsonLazyCreator(this);
                return _mList[aIndex];
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (aIndex < 0 || aIndex >= _mList.Count)
                    _mList.Add(value);
                else
                    _mList[aIndex] = value;
            }
        }

        public override JsonNode this[string aKey]
        {
            get { return new JsonLazyCreator(this); }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                _mList.Add(value);
            }
        }

        public override int Count
        {
            get { return _mList.Count; }
        }

        public override void Add(string aKey, JsonNode aItem)
        {
            if (aItem == null)
                aItem = JsonNull.CreateOrGet();
            _mList.Add(aItem);
        }

        public override JsonNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= _mList.Count)
                return null;
            JsonNode tmp = _mList[aIndex];
            _mList.RemoveAt(aIndex);
            return tmp;
        }

        public override JsonNode Remove(JsonNode aNode)
        {
            _mList.Remove(aNode);
            return aNode;
        }

        public override IEnumerable<JsonNode> Children
        {
            get
            {
                foreach (JsonNode n in _mList)
                    yield return n;
            }
        }


        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('[');
            int count = _mList.Count;
            if (_inline)
                aMode = JsonTextMode.Compact;
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    aSb.Append(',');
                if (aMode == JsonTextMode.Indent)
                    aSb.AppendLine();

                if (aMode == JsonTextMode.Indent)
                    aSb.Append(' ', aIndent + aIndentInc);
                _mList[i].WriteToStringBuilder(aSb, aIndent + aIndentInc, aIndentInc, aMode);
            }
            if (aMode == JsonTextMode.Indent)
                aSb.AppendLine().Append(' ', aIndent);
            aSb.Append(']');
        }
    }
}