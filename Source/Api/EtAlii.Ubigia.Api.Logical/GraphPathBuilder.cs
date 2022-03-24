// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerStepThrough]
    public sealed class GraphPathBuilder : IGraphPathBuilder
    {
        private readonly List<GraphPathPart> _parts = new();

        public IGraphPathBuilder Add(in Identifier startIdentifier)
        {
            if (_parts.Any())
            {
                throw new InvalidOperationException("An identifier can only be used at the start of a graph path.");
            }
            _parts.Add(new GraphIdentifiersStartNode(startIdentifier));
            return this;
        }

        public IGraphPathBuilder Add(string nodeName)
        {
            _parts.Add(new GraphNode(nodeName));
            return this;
        }

        public IGraphPathBuilder Add(GraphRelation relation)
        {
            _parts.Add(relation);
            return this;
        }

        public IGraphPathBuilder Add(GraphPathPart part)
        {
            _parts.Add(part);
            return this;
        }

        public IGraphPathBuilder AddRange(IEnumerable<GraphPathPart> parts)
        {
            _parts.AddRange(parts);
            return this;
        }

        public GraphPath ToPath()
        {
            return new(_parts.ToArray());
        }

        public void Clear()
        {
            _parts.Clear();
        }
    }
}
