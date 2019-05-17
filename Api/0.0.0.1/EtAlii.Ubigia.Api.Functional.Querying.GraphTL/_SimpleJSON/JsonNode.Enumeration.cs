namespace SimpleJson
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract partial class JsonNode
    {
        
        public virtual IEnumerable<JsonNode> Children { get { yield break; } }
        public abstract Enumerator GetEnumerator();
        public IEnumerable<KeyValuePair<string, JsonNode>> Linq => new LinqEnumerator(this);
        public KeyEnumerator Keys => new KeyEnumerator(GetEnumerator());
        public ValueEnumerator Values => new ValueEnumerator(GetEnumerator());

        public IEnumerable<JsonNode> DeepChildren
        {
            get
            {
                foreach (var c in Children)
                foreach (var d in c.DeepChildren)
                    yield return d;
            }
        }
         public struct Enumerator
        {
            private enum Type { None, Array, Object }
            private readonly Type _type;
            private Dictionary<string, JsonNode>.Enumerator _mObject;
            private List<JsonNode>.Enumerator _mArray;
            public bool IsValid => _type != Type.None;

            public Enumerator(List<JsonNode>.Enumerator aArrayEnum)
            {
                _type = Type.Array;
                _mObject = default(Dictionary<string, JsonNode>.Enumerator);
                _mArray = aArrayEnum;
            }
            public Enumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum)
            {
                _type = Type.Object;
                _mObject = aDictEnum;
                _mArray = default(List<JsonNode>.Enumerator);
            }
            public KeyValuePair<string, JsonNode> Current
            {
                get {
                    if (_type == Type.Array)
                        return new KeyValuePair<string, JsonNode>(string.Empty, _mArray.Current);
                    else if (_type == Type.Object)
                        return _mObject.Current;
                    return new KeyValuePair<string, JsonNode>(string.Empty, null);
                }
            }
            public bool MoveNext()
            {
                if (_type == Type.Array)
                    return _mArray.MoveNext();
                else if (_type == Type.Object)
                    return _mObject.MoveNext();
                return false;
            }
        }
        public struct ValueEnumerator
        {
            private Enumerator _mEnumerator;
            public ValueEnumerator(List<JsonNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public ValueEnumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public ValueEnumerator(Enumerator aEnumerator) { _mEnumerator = aEnumerator; }
            public JsonNode Current => _mEnumerator.Current.Value;
            public bool MoveNext() { return _mEnumerator.MoveNext(); }
            public ValueEnumerator GetEnumerator() { return this; }
        }
        public struct KeyEnumerator
        {
            private Enumerator _mEnumerator;
            public KeyEnumerator(List<JsonNode>.Enumerator aArrayEnum) : this(new Enumerator(aArrayEnum)) { }
            public KeyEnumerator(Dictionary<string, JsonNode>.Enumerator aDictEnum) : this(new Enumerator(aDictEnum)) { }
            public KeyEnumerator(Enumerator aEnumerator) { _mEnumerator = aEnumerator; }
            public string Current => _mEnumerator.Current.Key;
            public bool MoveNext() { return _mEnumerator.MoveNext(); }
            public KeyEnumerator GetEnumerator() { return this; }
        }

        public class LinqEnumerator : IEnumerator<KeyValuePair<string, JsonNode>>, IEnumerable<KeyValuePair<string, JsonNode>>
        {
            private JsonNode _mNode;
            private Enumerator _mEnumerator;
            internal LinqEnumerator(JsonNode aNode)
            {
                _mNode = aNode;
                if (_mNode != null)
                    _mEnumerator = _mNode.GetEnumerator();
            }
            public KeyValuePair<string, JsonNode> Current => _mEnumerator.Current;
            object IEnumerator.Current => _mEnumerator.Current;
            public bool MoveNext() { return _mEnumerator.MoveNext(); }

            public void Dispose()
            {
                _mNode = null;
                _mEnumerator = new Enumerator();
            }

            public IEnumerator<KeyValuePair<string, JsonNode>> GetEnumerator()
            {
                return new LinqEnumerator(_mNode);
            }

            public void Reset()
            {
                if (_mNode != null)
                    _mEnumerator = _mNode.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new LinqEnumerator(_mNode);
            }
        }

    }
}