// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Collections.Generic;
using Moppet.Lapa;

internal sealed class NodeFinder : INodeFinder
{
    public LpNode FindFirst(LpNode node, string id)
    {
        return FindFirst(new[] { node }, id);
    }

    public LpNode FindFirst(IEnumerable<LpNode> nodes)
    {
        LpNode result = null;

        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                if (node.Id != null)
                {
                    result = node;
                }
                else
                {
                    result = FindFirst(node.Children);
                }
                if (result != null)
                {
                    break;
                }
            }
        }
        return result;
    }

    public LpNode FindFirst(IEnumerable<LpNode> nodes, string id)
    {
        if (nodes == null) return null;

        LpNode result = null;

        foreach(var node in nodes)
        {
            result = node.Id == id
                ? node
                : FindFirst(node.Children, id);

            if (result != null)
            {
                break;
            }
        }
        return result;
    }

    public IEnumerable<LpNode> FindAll(LpNode node, string id)
    {
        return FindAll(new[] { node }, id);
    }

    public IEnumerable<LpNode> FindAll(IEnumerable<LpNode> nodes, string id)
    {
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                if (node.Id == id)
                {
                    yield return node;
                }
                else
                {
                    var subResults = FindAll(node.Children, id);
                    foreach (var subResult in subResults)
                    {
                        yield return subResult;
                    }
                }
            }
        }
    }
}
