namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using Moppet.Lapa;

    internal interface INodeValidator
    {
        void EnsureSuccess(LpNode node, string requiredId, bool restIsAllowed = true);
    }
}