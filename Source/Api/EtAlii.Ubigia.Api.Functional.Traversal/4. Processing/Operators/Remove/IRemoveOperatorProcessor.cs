namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}
