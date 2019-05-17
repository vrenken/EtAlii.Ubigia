namespace SimpleJson
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class JsonObject : JsonNode
    {
        private readonly Dictionary<string, JsonNode> _dictionary = new Dictionary<string, JsonNode>();

        public override bool Inline { get => _inline; set => _inline = value; }
        private bool _inline = false;

        public override JsonNodeType Tag => JsonNodeType.Object;
        public override bool IsObject => true;

        public override Enumerator GetEnumerator() { return new Enumerator(_dictionary.GetEnumerator()); }


        public override JsonNode this[string aKey]
        {
            get
            {
                if (_dictionary.ContainsKey(aKey))
                    return _dictionary[aKey];
                else
                    return new JsonLazyCreator(this, aKey);
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (_dictionary.ContainsKey(aKey))
                    _dictionary[aKey] = value;
                else
                    _dictionary.Add(aKey, value);
            }
        }

        public override JsonNode this[int index]
        {
            get
            {
                if (index < 0 || index >= _dictionary.Count)
                    return null;
                return _dictionary.ElementAt(index).Value;
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (index < 0 || index >= _dictionary.Count)
                    return;
                string key = _dictionary.ElementAt(index).Key;
                _dictionary[key] = value;
            }
        }

        public override int Count => _dictionary.Count;

        public override void Add(string key, JsonNode item)
        {
            if (item == null)
                item = JsonNull.CreateOrGet();

            if (key != null)
            {
                if (_dictionary.ContainsKey(key))
                    _dictionary[key] = item;
                else
                    _dictionary.Add(key, item);
            }
            else
                _dictionary.Add(Guid.NewGuid().ToString(), item);
        }

        public override JsonNode Remove(string aKey)
        {
            if (!_dictionary.ContainsKey(aKey))
                return null;
            JsonNode tmp = _dictionary[aKey];
            _dictionary.Remove(aKey);
            return tmp;
        }

        public override JsonNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= _dictionary.Count)
                return null;
            var item = _dictionary.ElementAt(aIndex);
            _dictionary.Remove(item.Key);
            return item.Value;
        }

        public override JsonNode Remove(JsonNode aNode)
        {
            try
            {
                var item = _dictionary.Where(k => k.Value == aNode).First();
                _dictionary.Remove(item.Key);
                return aNode;
            }
            catch
            {
                return null;
            }
        }

        public override bool HasKey(string aKey)
        {
            return _dictionary.ContainsKey(aKey);
        }

        public override JsonNode GetValueOrDefault(string aKey, JsonNode aDefault)
        {
            JsonNode res;
            if (_dictionary.TryGetValue(aKey, out res))
                return res;
            return aDefault;
        }

        public override IEnumerable<JsonNode> Children
        {
            get
            {
                foreach (KeyValuePair<string, JsonNode> n in _dictionary)
                    yield return n.Value;
            }
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('{');
            bool first = true;
            if (_inline)
                aMode = JsonTextMode.Compact;
            foreach (var k in _dictionary)
            {
                if (!first)
                    aSb.Append(',');
                first = false;
                if (aMode == JsonTextMode.Indent)
                    aSb.AppendLine();
                if (aMode == JsonTextMode.Indent)
                    aSb.Append(' ', aIndent + aIndentInc);
                aSb.Append('\"').Append(Escape(k.Key)).Append('\"');
                if (aMode == JsonTextMode.Compact)
                    aSb.Append(':');
                else
                    aSb.Append(" : ");
                k.Value.WriteToStringBuilder(aSb, aIndent + aIndentInc, aIndentInc, aMode);
            }
            if (aMode == JsonTextMode.Indent)
                aSb.AppendLine().Append(' ', aIndent);
            aSb.Append('}');
        }

    }
}