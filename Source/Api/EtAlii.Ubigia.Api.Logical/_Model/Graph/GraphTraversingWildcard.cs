// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Diagnostics;

[DebuggerStepThrough]
[DebuggerDisplay("{" + nameof(Limit) + "}")]
public sealed class GraphTraversingWildcard : GraphPathPart
{
    public readonly int Limit;

    public GraphTraversingWildcard(int limit)
    {
        Limit = limit;
    }

    public override string ToString()
    {
        return Limit == 0 ? "**" : $"*{Limit}*";
    }
}
