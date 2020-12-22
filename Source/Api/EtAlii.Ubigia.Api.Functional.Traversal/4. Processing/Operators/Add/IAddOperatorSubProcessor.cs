namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    public interface IAddOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}
