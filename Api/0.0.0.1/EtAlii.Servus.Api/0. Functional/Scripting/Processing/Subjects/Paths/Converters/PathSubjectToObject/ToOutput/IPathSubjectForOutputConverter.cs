namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface IPathSubjectForOutputConverter
    {
        void Convert(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output);
    }
}