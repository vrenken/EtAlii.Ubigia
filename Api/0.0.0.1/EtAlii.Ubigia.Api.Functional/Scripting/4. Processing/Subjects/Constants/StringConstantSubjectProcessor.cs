namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class StringConstantSubjectProcessor : IStringConstantSubjectProcessor
    {
        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var value = ((StringConstantSubject)subject).Value;
            output.OnNext(value);
            output.OnCompleted();
        }
    }
}
