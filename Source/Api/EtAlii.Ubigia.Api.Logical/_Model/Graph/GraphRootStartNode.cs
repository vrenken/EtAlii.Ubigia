// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(Root) + "}")]
    public sealed class GraphRootStartNode : GraphPathPart
    {
        public string Root { get; }

        public GraphRootStartNode(string root)
        {
            Root = root;
        }

        public override string ToString()
        {
            return Root;
        }
    }
}
