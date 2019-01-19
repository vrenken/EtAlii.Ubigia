namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface ISubjectProcessor
    {
        Task Process(Subject subject, ExecutionScope scope, IObserver<object> output);
    }
}
