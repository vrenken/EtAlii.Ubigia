namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IRemoveOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}