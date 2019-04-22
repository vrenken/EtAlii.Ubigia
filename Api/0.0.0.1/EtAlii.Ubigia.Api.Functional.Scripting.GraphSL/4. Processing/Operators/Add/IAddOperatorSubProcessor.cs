namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    public interface IAddOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}