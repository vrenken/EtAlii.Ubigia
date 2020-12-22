namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal interface IAddOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}
