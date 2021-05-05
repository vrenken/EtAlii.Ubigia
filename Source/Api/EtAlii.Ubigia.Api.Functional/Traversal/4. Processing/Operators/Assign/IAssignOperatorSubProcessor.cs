namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorSubProcessor
    {
        Task Assign(OperatorParameters parameters);
    }
}
