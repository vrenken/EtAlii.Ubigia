namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class RootSubjectProcessor : IRootSubjectProcessor
    {
        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootSubject = (RootSubject)subject;
            output.OnNext(rootSubject);
            output.OnCompleted();

            await Task.CompletedTask;
        }
    }
}
