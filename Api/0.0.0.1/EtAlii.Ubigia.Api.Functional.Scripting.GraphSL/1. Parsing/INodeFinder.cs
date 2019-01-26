namespace EtAlii.Ubigia.Api.Functional
{
    using System.Collections.Generic;
    using Moppet.Lapa;

    internal interface INodeFinder
    {
        IEnumerable<LpNode> FindAll(IEnumerable<LpNode> nodes, string id);
        IEnumerable<LpNode> FindAll(LpNode node, string id);

        LpNode FindFirst(IEnumerable<LpNode> nodes);

        LpNode FindFirst(LpNode node, string id);
        LpNode FindFirst(IEnumerable<LpNode> nodes, string id);
    }
}