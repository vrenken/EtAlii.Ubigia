namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Collections.Generic;

    internal interface IParserHelper
    {
        void EnsureSuccess2(LpNode node, string requiredId, bool restIsAllowed = true);
        void EnsureSuccess(LpNode node, string requiredId, bool noRestExpected = false, string text = null);

        IEnumerable<LpNode> FindAll(IEnumerable<LpNode> nodes, string id);
        IEnumerable<LpNode> FindAll(LpNode node, string id);

        //LpNode FindFirst(LpNode node);
        LpNode FindFirst(IEnumerable<LpNode> nodes);

        LpNode FindFirst(LpNode node, string id);
        LpNode FindFirst(IEnumerable<LpNode> nodes, string id);
    }
}