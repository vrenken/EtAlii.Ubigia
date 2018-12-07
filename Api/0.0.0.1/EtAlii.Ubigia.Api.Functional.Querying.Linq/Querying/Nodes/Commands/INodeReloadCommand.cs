namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Logical;

    internal interface INodeReloadCommand
    {
        void Execute(INode node)//, bool updateToLatest = false)
            ;
    }
}