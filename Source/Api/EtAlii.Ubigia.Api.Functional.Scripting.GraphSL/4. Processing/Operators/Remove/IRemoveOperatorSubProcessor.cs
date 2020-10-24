namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorSubProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}