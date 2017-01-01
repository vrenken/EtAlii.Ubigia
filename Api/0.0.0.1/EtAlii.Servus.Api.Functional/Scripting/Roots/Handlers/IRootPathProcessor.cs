namespace EtAlii.Servus.Api.Functional
{
    using System;

    public interface IRootPathProcessor
    {
        void Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output, IScriptScope scriptScope);
    }
}