// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

/// <summary>
/// Use this class to build GraphPath instances of identifiers, nodes and relations.
/// </summary>
public interface IGraphPathBuilder
{
    /// <summary>
    /// Add a start identifier to the GraphPath to be build.
    /// </summary>
    /// <param name="startIdentifier"></param>
    /// <returns></returns>
    IGraphPathBuilder Add(in Identifier startIdentifier);

    /// <summary>
    /// Add a node to the GraphPath to be build.
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns></returns>
    IGraphPathBuilder Add(string nodeName);

    /// <summary>
    /// Add a relation to the GraphPath to be build.
    /// </summary>
    /// <param name="relation"></param>
    /// <returns></returns>
    IGraphPathBuilder Add(GraphRelation relation);

    /// <summary>
    /// Build the GraphPath and return it.
    /// </summary>
    /// <returns></returns>
    GraphPath ToPath();
}
