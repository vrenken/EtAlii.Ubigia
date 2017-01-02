namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    internal interface INodeSaveCommand
    {
        void Execute(INode node);
    }
}