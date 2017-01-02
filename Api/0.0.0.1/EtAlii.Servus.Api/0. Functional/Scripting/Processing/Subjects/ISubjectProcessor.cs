namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal interface ISubjectProcessor
    {
        void Process(Subject subject, ExecutionScope scope, IObserver<object> output);
    }
}
