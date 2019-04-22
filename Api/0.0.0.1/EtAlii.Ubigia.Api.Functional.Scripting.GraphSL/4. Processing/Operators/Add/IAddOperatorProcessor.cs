namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IAddOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}