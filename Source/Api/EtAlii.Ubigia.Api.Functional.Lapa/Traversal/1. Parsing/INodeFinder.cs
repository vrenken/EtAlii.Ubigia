// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal interface INodeFinder
    {
        IEnumerable<LpNode> FindAll(IEnumerable<LpNode> nodes, string id);
        IEnumerable<LpNode> FindAll(LpNode node, string id);

        //LpNode FindFirst(LpNode node)
        LpNode FindFirst(IEnumerable<LpNode> nodes);

        LpNode FindFirst(LpNode node, string id);
        LpNode FindFirst(IEnumerable<LpNode> nodes, string id);
    }
}
