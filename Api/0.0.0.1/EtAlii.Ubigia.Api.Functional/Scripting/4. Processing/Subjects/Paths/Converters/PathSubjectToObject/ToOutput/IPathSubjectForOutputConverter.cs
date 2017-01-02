namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface IPathSubjectForOutputConverter
    {
        void Convert(PathSubject pathSubject, ExecutionScope scope, IObserver<object> output);
    }
}