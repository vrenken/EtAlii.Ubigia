namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface IPathSubjectConverter2
    {
        void Convert(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output);
    }
}