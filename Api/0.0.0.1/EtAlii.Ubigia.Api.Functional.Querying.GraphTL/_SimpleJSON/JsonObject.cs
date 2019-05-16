namespace SimpleJson
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class JsonObject : JsonNode
    {
        private Dictionary<string, JsonNode> _mDict = new Dictionary<string, JsonNode>();

        private bool _inline = false;
        public override bool Inline
        {
            get { return _inline; }
            set { _inline = value; }
        }

        public override JsonNodeType Tag { get { return JsonNodeType.Object; } }
        public override bool IsObject { get { return true; } }

        public override Enumerator GetEnumerator() { return new Enumerator(_mDict.GetEnumerator()); }


        public override JsonNode this[string aKey]
        {
            get
            {
                if (_mDict.ContainsKey(aKey))
                    return _mDict[aKey];
                else
                    return new JsonLazyCreator(this, aKey);
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (_mDict.ContainsKey(aKey))
                    _mDict[aKey] = value;
                else
                    _mDict.Add(aKey, value);
            }
        }

        public override JsonNode this[int aIndex]
        {
            get
            {
                if (aIndex < 0 || aIndex >= _mDict.Count)
                    return null;
                return _mDict.ElementAt(aIndex).Value;
            }
            set
            {
                if (value == null)
                    value = JsonNull.CreateOrGet();
                if (aIndex < 0 || aIndex >= _mDict.Count)
                    return;
                string key = _mDict.ElementAt(aIndex).Key;
                _mDict[key] = value;
            }
        }

        public override int Count
        {
            get { return _mDict.Count; }
        }

        public override void Add(string aKey, JsonNode aItem)
        {
            if (aItem == null)
                aItem = JsonNull.CreateOrGet();

            if (aKey != null)
            {
                if (_mDict.ContainsKey(aKey))
                    _mDict[aKey] = aItem;
                else
                    _mDict.Add(aKey, aItem);
            }
            else
                _mDict.Add(Guid.NewGuid().ToString(), aItem);
        }

        public override JsonNode Remove(string aKey)
        {
            if (!_mDict.ContainsKey(aKey))
                return null;
            JsonNode tmp = _mDict[aKey];
            _mDict.Remove(aKey);
            return tmp;
        }

        public override JsonNode Remove(int aIndex)
        {
            if (aIndex < 0 || aIndex >= _mDict.Count)
                return null;
            var item = _mDict.ElementAt(aIndex);
            _mDict.Remove(item.Key);
            return item.Value;
        }

        public override JsonNode Remove(JsonNode aNode)
        {
            try
            {
                var item = _mDict.Where(k => k.Value == aNode).First();
                _mDict.Remove(item.Key);
                return aNode;
            }
            catch
            {
                return null;
            }
        }

        public override bool HasKey(string aKey)
        {
            return _mDict.ContainsKey(aKey);
        }

        public override JsonNode GetValueOrDefault(string aKey, JsonNode aDefault)
        {
            JsonNode res;
            if (_mDict.TryGetValue(aKey, out res))
                return res;
            return aDefault;
        }

        public override IEnumerable<JsonNode> Children
        {
            get
            {
                foreach (KeyValuePair<string, JsonNode> n in _mDict)
                    yield return n.Value;
            }
        }

        internal override void WriteToStringBuilder(StringBuilder aSb, int aIndent, int aIndentInc, JsonTextMode aMode)
        {
            aSb.Append('{');
            bool first = true;
            if (_inline)
                aMode = JsonTextMode.Compact;
            foreach (var k in _mDict)
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