namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    internal interface INodeSaveCommand
    {
        void Execute(INode node);
    }
}