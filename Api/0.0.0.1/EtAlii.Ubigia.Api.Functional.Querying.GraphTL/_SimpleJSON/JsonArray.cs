namespace SimpleJson
{
    using System.Collections.Generic;
    using System.Text;

    public partial class JsonArray : JsonNode
    {
        private readonly List<JsonNode> _list = new List<JsonNode>();
        private bool _inline = false;
        public override bool Inline
        {
            get => _inline;
            set => _inline = value;
        }

        public override JsonNodeType Tag => JsonNodeType.Array;
        public override bool IsArray => true;
        public override Enumerator GetEnumerator() { return new Enumerator(_list.GetEnumerator()); }

        public override JsonNode this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                    return new JsonLazyCreator(this);
                return _list[index];
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (index < 0 || index >= _list.Count)
                    _list.Add(value);
                else
                    _list[index] = value;
            }
        }

        public override JsonNode this[string aKey]
        {
            get => new JsonLazyCreator(this);
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                _list.Add(value);
            }
        }

        public override int Count => _list.Count;

        public override void Add(string key, JsonNode item)
        {
            if (item == null)
                item = JsonNull.CreateOrGet();
            _list.Add(item);
        }

        public override JsonNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= _list.Count)
                return null;
            var tmp = _list[aIndex];
            _list.RemoveAt(aIndex);
            return tmp;
        }

        public override JsonNode Remove(JsonNode aNode)
        {
            _list.Remove(aNode);
            return aNode;
        }

        public override IEnumerable<JsonNode> Children
        {
            get
            {
                foreach (var n in _list)
                    yield return n;
            }
        }


        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('[');
            var count = _list.Count;
            if (_inline)
                aMode = JsonTextMode.Compact;
            for (var i = 0; i < count; i++)
            {
                if (i > 0)
                    aSb.Append(',');
                if (aMode == JsonTextMode.Indent)
                    aSb.AppendLine();

                if (aMode == JsonTextMode.Indent)
                    aSb.Append(' ', aIndent + aIndentInc);
                _list[i].WriteToStringBuilder(aSb, aIndent + aIndentInc, aIndentInc, aMode);
            }
            if (aMode == JsonTextMode.Indent)
                aSb.AppendLine().Append(' ', aIndent);
            aSb.Append(']');
        }
    }
}