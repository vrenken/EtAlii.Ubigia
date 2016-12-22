namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Logical;

    internal interface INodeReloadCommand
    {
        void Execute(INode node)//, bool updateToLatest = false)
            ;
    }
}