// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Diagnostics;

[DebuggerStepThrough]
[DebuggerDisplay("{" + nameof(Name) + "}#{" + nameof(Tag)  +"}")]
public sealed class GraphTaggedNode : GraphPathPart
{
    public readonly string Name;
    public readonly string Tag;

    public GraphTaggedNode(string name, string tag)
    {
        Name = name;
        Tag = tag;
    }

    public override string ToString()
    {
        return $"{Name}#{Tag}";
    }
}
