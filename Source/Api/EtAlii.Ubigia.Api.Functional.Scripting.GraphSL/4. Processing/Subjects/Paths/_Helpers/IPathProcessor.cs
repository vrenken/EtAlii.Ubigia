namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

    public interface IPathProcessor
    {
        IScriptProcessingContext Context { get; }
        Task Process(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output);
    }
}