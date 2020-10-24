namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal interface INodeReloadCommand
    {
        Task Execute(INode node); //, bool updateToLatest = false)
    }
}