namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}