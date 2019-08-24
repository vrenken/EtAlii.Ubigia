namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorSubProcessor
    {
        Task Assign(OperatorParameters parameters);
    }
}
