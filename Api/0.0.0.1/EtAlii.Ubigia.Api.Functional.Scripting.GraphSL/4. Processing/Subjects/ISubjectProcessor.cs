namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface ISubjectProcessor
    {
        void Process(Subject subject, ExecutionScope scope, IObserver<object> output);
    }
}
