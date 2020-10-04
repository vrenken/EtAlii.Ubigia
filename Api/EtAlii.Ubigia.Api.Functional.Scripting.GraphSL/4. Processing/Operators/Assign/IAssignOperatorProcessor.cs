namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IAssignOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}