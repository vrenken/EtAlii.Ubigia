namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    public interface IAddOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}