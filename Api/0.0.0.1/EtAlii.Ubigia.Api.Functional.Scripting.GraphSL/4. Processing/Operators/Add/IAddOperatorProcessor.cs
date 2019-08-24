namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System.Threading.Tasks;

    internal interface IAddOperatorProcessor
    {
        Task Process(OperatorParameters parameters);
    }
}