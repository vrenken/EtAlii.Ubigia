namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}