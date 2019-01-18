namespace EtAlii.Ubigia.Api.Functional
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorSubProcessor
    {
        Task Assign(OperatorParameters parameters);
    }
}
