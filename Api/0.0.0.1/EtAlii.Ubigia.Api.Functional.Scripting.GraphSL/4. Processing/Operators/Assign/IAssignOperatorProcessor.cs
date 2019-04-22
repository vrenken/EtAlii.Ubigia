namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}