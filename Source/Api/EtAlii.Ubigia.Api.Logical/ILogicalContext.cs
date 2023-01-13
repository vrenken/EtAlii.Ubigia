// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;

public interface ILogicalContext : IAsyncDisposable
{
    /// <summary>
    /// The Options used to instantiate this Context.
    /// </summary>
    LogicalOptions Options { get; }

    /// <summary>
    /// The Nodes property provides logical access to graph nodes.
    /// </summary>
    ILogicalNodeSet Nodes { get; }

    /// <summary>
    /// The Roots property provides logical access to the roots of the graph.
    /// Use it to start a traversal.
    /// </summary>
    ILogicalRootSet Roots { get; }

    /// <summary>
    /// The Content property provides access non-value content stored in graph nodes.
    /// For example media or other more-binary oriented materials.
    /// </summary>
    IContentManager Content { get; }

    /// <summary>
    /// The Properties property provides access to properties stored in graph nodes.
    /// </summary>
    IPropertiesManager Properties { get; }
}
