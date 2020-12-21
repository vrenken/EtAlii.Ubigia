namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    public interface IRootPathProcessor
    {
        Task Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output, IScriptScope scriptScope);
    }
}
