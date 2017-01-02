namespace EtAlii.Ubigia.Api.Functional
{
    using Moppet.Lapa;

    internal interface INodeValidator
    {
        void EnsureSuccess(LpNode node, string requiredId, bool restIsAllowed = true);
    }
}