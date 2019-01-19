namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class StringConstantSubjectProcessor : IStringConstantSubjectProcessor
    {
        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var value = ((StringConstantSubject)subject).Value;
            output.OnNext(value);
            output.OnCompleted();

            await Task.CompletedTask;
        }
    }
}
