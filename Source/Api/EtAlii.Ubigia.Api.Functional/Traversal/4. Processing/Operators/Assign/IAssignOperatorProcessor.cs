namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}
