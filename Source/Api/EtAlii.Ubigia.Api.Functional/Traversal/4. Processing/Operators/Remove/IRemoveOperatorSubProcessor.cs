namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}
