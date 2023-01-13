// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

public sealed class PathTraversalContext : IPathTraversalContext
{
    public ITraversalContextEntrySet Entries { get; }

    public ITraversalContextRootSet Roots { get; }

    public ITraversalContextPropertySet Properties { get; }

    public PathTraversalContext(
        ITraversalContextEntrySet entries,
        ITraversalContextRootSet roots,
        ITraversalContextPropertySet properties)
    {
        Entries = entries;
        Roots = roots;
        Properties = properties;
    }
}
