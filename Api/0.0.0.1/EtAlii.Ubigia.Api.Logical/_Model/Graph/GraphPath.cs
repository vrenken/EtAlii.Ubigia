namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public struct GraphPath : IEnumerable<GraphPathPart>
    {
        public static readonly GraphPath Empty = new GraphPath();

        private readonly GraphPathPart[] _parts;

        public GraphPath(params GraphPathPart[] parts)
        {
            _parts = parts;
        }

        public IEnumerator<GraphPathPart> GetEnumerator()
        {
            foreach (var part in _parts)
            {
                yield return part;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var part in _parts)
            {
                yield return part;
            }
        }

        public override string ToString()
        {
            return String.Join("-", _parts.Select(p => "["+ p.ToString() +"]"));
        }

        public static GraphPath Create(Identifier startIdentifier)
        {
            var graphStartNode = new GraphIdentifiersStartNode(startIdentifier);
            return new GraphPath(graphStartNode);
        }

        public static GraphPath Create(Identifier startIdentifier, params GraphPathPart[] parts)
        {
            return Create(new[] { startIdentifier }, parts);
        }

        public static GraphPath Create(IEnumerable<Identifier> startIdentifiers, params GraphPathPart[] parts)
        {
            var list = new List<GraphPathPart> {new GraphIdentifiersStartNode(startIdentifiers)};
            list.AddRange(parts);
            return new GraphPath(list.ToArray());
        }

        public GraphPathPart this[int index] => _parts[index];

        public int Length => _parts.Length;
    }
}