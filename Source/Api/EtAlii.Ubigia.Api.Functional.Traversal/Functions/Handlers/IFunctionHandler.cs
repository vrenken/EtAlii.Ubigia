namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    public interface IFunctionHandler
    {
        ParameterSet[] ParameterSets { get; }
        string Name { get; }

        Task Process(
            IFunctionContext context,
            ParameterSet parameterSet,
            ArgumentSet argumentSet,
            IObservable<object> input,
            ExecutionScope scope,
            IObserver<object> output,
            bool processAsSubject);
    }
}
