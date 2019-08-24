namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal interface INodeSaveCommand
    {
        Task Execute(INode node);
    }
}