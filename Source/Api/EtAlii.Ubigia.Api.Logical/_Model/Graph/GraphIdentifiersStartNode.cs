// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Linq;

    public class GraphIdentifiersStartNode : GraphPathPart
    {
        public readonly IEnumerable<Identifier> Identifiers;

        public GraphIdentifiersStartNode(IEnumerable<Identifier> identifiers)
        {
            Identifiers = identifiers;
        }

        public GraphIdentifiersStartNode(in Identifier identifier)
        {
            Identifiers = new[] { identifier };
        }

        public override string ToString()
        {
            return Identifiers.Count() == 1 ? Identifiers.Single().ToTimeString() : "Multiple starts";
        }
    }
}
